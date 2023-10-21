using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assigment4
{
    public class Recipe
    {
        private string name;
        private string description;
        private FoodCategory ctegory;// a category as listed in the enum FoodCategory
        private string[] ingredients;
        int maxNumber = 50;
        // Defining the constructor
        public Recipe()
        {
            //creating the array and giving valu to other variables
            ingredients = new string[maxNumber];//It has null elements
            name = string.Empty;
            description = string.Empty; 
            ctegory = FoodCategory.Other;
            //Giving initial values to ingredients array
            for (int i = 0; i < maxNumber; i++) 
            {
                ingredients[i] = string.Empty;
            }
        }
        // reading and writing the fields of a recipe
        #region Get and Set methods
        public string GetName()
        {
            return name;
        }
        public void SetName(string name)
        { this.name = name; }
        public void SetDescription(string description) 
        { 
            this.description = description;
        }
        public string GetDescription() 
        { 
            return description; 
        }
        public FoodCategory GetCategory()
        {
            return ctegory;
        }
        public void SetCategory(FoodCategory category)
        { this.ctegory = category; }
        public string[] GetIngredients() 
        { 
            return ingredients; 
        }
        public void SetIngredients(string[] ingredients)
        {

            for(int i = 0;i < ingredients.Length;i++)
            {
                this.ingredients[i] = ingredients[i].ToString();
            }
        }
        public void SetMax(int maxNumber)
        { this.maxNumber = maxNumber; }
        public int GetMax()
        { return maxNumber;  }
        #endregion
        public int currentNumberOfIngredientd()
        {
            int numberOfElements = 0;
            while (ingredients[numberOfElements]!=string.Empty) 
            {
                numberOfElements++;
            }
            return numberOfElements;
        }
        public int GetCategoryIndex(FoodCategory category)
        {
            return (int)category;
        }
        public int GetNumberOfElement()
        { 
            int numberOfElement = 0;
            for(int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i]!=string.Empty)
                {
                    numberOfElement++;
                }
            }
            return numberOfElement;
        }
    }

}
