using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace assigment4
{
    public class RecipeManager
    {
        private Recipe[] recipesList;
        private int maxListNumber=200;
        /// <summary>
        /// this method is construction
        /// </summary>
        public RecipeManager()
        {
            recipesList = new Recipe[maxListNumber];
            for (int i = 0; i < maxListNumber; i++)
            {
                recipesList[i] = new Recipe();
                recipesList[i].SetName("No name");
            }
        }
        /// <summary>
        /// these methods are for reading and writing the fields
        /// </summary>
        #region Set and Get methods
        public int GetMaxList()
        { return maxListNumber; }
        public void SetMaxList(int maxListNum)
        { this.maxListNumber = maxListNum;  }
        #endregion
        public int SearchElement(string searchingName)
        {
            int foundIndex = -1;
            for (int i = 0; i < recipesList.Length; i++)
            {
                if (recipesList[i].GetName().Equals(searchingName))
                {
                    foundIndex = i;
                    break;
                }
            }
            return foundIndex;
        }

        // Add a new Recipe to the list
        public bool AddReciep(Recipe recipe)
        {
            int emptySlotIndex = -1;
            for (int i = 0; i < recipesList.Length; i++)
            {
                
                if (recipesList[i].GetName() == "No name")
                {
                    emptySlotIndex = i;
                    break;
                }
            }

            if (emptySlotIndex >= 0)
            {
                SaveNewRecipe(emptySlotIndex, recipe);
                return true;
            }
            return false;

        }
        public bool CheckIndex(int index)
        {
            bool ok = (index >= 0) && (index < recipesList.Length);//checking the index is not out of range because in this case program will stop
            return ok;
        }
        public bool EditValues(int index, Recipe recipe)
        {
            //check the validity of index
            bool ok = CheckIndex(index);
            if (ok)
            {
                SaveNewRecipe(index, recipe);
            }
           
            return ok;
        }
        public bool DeleteRecipe(Recipe recipe)
        {
            int index = 0;
            while ((recipesList[index].GetName() != recipe.GetName()) && (index<50))//finding the recipe which is aimed to delet
                    index++;
            bool ok = CheckIndex(index) ;
            string[] emptyIngredients = new string[0];
            if (ok)
            {
                //deleiting the recipe
                recipesList[index].SetName(string.Empty) ;
                recipesList[index].SetDescription(string.Empty) ;
                recipesList[index].SetCategory(FoodCategory.Other);
                recipesList[index].SetIngredients(emptyIngredients);
                MoveElementsToDeletedElement(index);// this method is written for deleting the emrty fields in the middle of array
            }
            return ok;
        }
        //this method will shift all the fields to the empty field
        private void MoveElementsToDeletedElement(int index)
        {
            for (int i = index+1; i < recipesList.Length; i++)
            {
                SaveNewRecipe(i-1, recipesList[i]);
            }
        }
        // this method is for saving every fields in a recipe to the definit index
        public void SaveNewRecipe(int pos, Recipe recipe)
        {
            recipesList[pos].SetName(recipe.GetName());
            recipesList[pos].SetDescription(recipe.GetDescription());
            recipesList[pos].SetCategory(recipe.GetCategory());
            recipesList[pos].SetIngredients(recipe.GetIngredients());
        }
        // this method will return all the recipes in a list of recipes
        public Recipe[] GetListOfAllRecipes()
        {
            int numOfElementsUsed = GetNumberOfElementUsed();
            if (numOfElementsUsed <= 0) 
            {
                return null;
            }
            Recipe[] listOfAllRecipes = new Recipe[numOfElementsUsed];//making an empty list of recipes for filling it and returining it.
            int position = 0;
            for (int i = 0; i < numOfElementsUsed; i++)
            {
                if (!recipesList[i].GetName().Equals("No name"))
                {
                    listOfAllRecipes[i] = new Recipe();
                    listOfAllRecipes[i].SetName(recipesList[i].GetName());
                    listOfAllRecipes[i].SetDescription(recipesList[i].GetDescription());
                    listOfAllRecipes[i].SetIngredients(recipesList[i].GetIngredients());
                    listOfAllRecipes[i].SetCategory(recipesList[i].GetCategory());
                    position++;
                }
            }
            return listOfAllRecipes;
        }
        // this method search all the fields of a list until the max limit 
        // count the elements which are used and return the numbers
        public int GetNumberOfElementUsed()
        {
            int numOfElements = 0;
            for (int i = 0;i < recipesList.Length;i++) 
            {
                if (!recipesList[i].GetName().Equals("No Name"))
                {  numOfElements++; }
            }
            return numOfElements;
        }
        // this method will get the name of a recipe and return the position
        public int FindRecipeIndexByName(string  name)
        {
            int numberOfRecipes = GetNumberOfElementUsed();
            for (int i = 0;i < numberOfRecipes; i++)
            {
                if (recipesList[i].GetName().Equals(name))
                { return i; }
            }
            return -1;// if the position is -1 it means that the method can not find the name
        }


    }
}
