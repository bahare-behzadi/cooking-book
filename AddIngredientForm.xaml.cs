using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace assigment4
{
    /// <summary>
    /// Interaction logic for AddIngredientForm.xaml
    /// </summary>
    public partial class AddIngredientForm : Window
    {
        private int ingredientsNum;
        string[] temp;
        /// <summary>
        /// construction
        /// </summary>
        public AddIngredientForm(Recipe newrecip)
        {
            InitializeComponent();
            temp = new string[newrecip.GetMax()];
            ingredientsNum = newrecip.currentNumberOfIngredientd();
            ingeridentsListBox.Items.Clear();
            for(int i = 0; i < newrecip.GetIngredients().Length; i++)
            {
                temp[i] = newrecip.GetIngredients()[i];
            }
            UpdateIngeridientsListBox(newrecip.GetIngredients());
            numberOfIngredientsLabel.Content = newrecip.currentNumberOfIngredientd();
             
        }
        /// reading and writing the number of ingredients
        public void SetingredientsNum(int ingredientsNum)
        {
            this.ingredientsNum = ingredientsNum;
        }
        public int GetingredientsNum()
        {
            return ingredientsNum;
        }
        public void SetNewStringList(string[] newlist, int max)
        {
            // defining a temprary list for ingredients
            for (int i = 0; i < max; i++)
            {
                newlist[i] = string.Empty;
            }
        }
        /// <summary>
        /// adding a new ingredient
        /// </summary>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if ((ingredientTextBox == null) || (ingredientTextBox.Text == string.Empty))
                MessageBox.Show("Ingredients should not be empty", "Error");
            else
            {
                temp[FindFreePlace(temp)] = ingredientTextBox.Text;
                ingredientsNum++;//adding the number of ingreidients
                UpdateIngeridientsListBox(temp);
                ingredientTextBox.Clear();
                numberOfIngredientsLabel.Content = ingredientsNum.ToString();
            }
        }
        /// <summary>
        /// editing an ingredient
        /// </summary>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (ingeridentsListBox.SelectedItem != null)
            {
                string selectedItemText = ingeridentsListBox.SelectedItem.ToString();
                ingredientTextBox.Text = selectedItemText;
                ///<editing>
                /// in this code when the user edit an ingridients, first the code delet it from the list
                /// then put it in the text box because the user be able to edit it
                /// then subtract 1 from the number of ingeridients
                /// for adding edited ingridients user must add it it again
                int index = FindingIndex(selectedItemText);
                ShifingCells(index);
                UpdateIngeridientsListBox(temp);
                ingredientsNum--;
                ///</editing>
                numberOfIngredientsLabel.Content = ingredientsNum.ToString();
            }
            else
            {
                MessageBox.Show("You should select an item first!", "Error");

            }

        }
        /// <summary>
        /// deleting an ingridients
        /// </summary>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ingeridentsListBox.SelectedItem != null)
            {
                //this code is for finding the ingeridirnt
                string selectedItemText = ingeridentsListBox.SelectedItem.ToString();
                int index = FindingIndex(selectedItemText);
                ShifingCells(index);
                ingeridentsListBox.Items.Remove(selectedItemText);
                UpdateIngeridientsListBox(temp);//this method will update the list box with new list, in which the ingredient is deleted
                ingredientsNum--;//subtracting one from all ingredients because one is deleted
                numberOfIngredientsLabel.Content = ingredientsNum.ToString();
            }
            else
            {
                MessageBox.Show("You should select an item to delete.", "Error");
            }
        }

        private void okIngredientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ingredientsNum == 0)
            {
                MessageBox.Show("No ingridients is added!");
            }
            else
            {
                DialogResult = true;//if DialogResult is true it means that everything is ok and the code can add listbox items to recipe ingridients
            }
        }
        /// <summary>
        /// this method returns the ingeridients list
        /// </summary>

        public string[] GetIngredientsList()
        {
            string[] ingredients = new string[ingredientsNum];
            for (int i = 0; i < ingredientsNum; i++)
            {
                ingredients[i] = ingeridentsListBox.Items[i].ToString();
            }
            return ingredients;
        }
        /// <summary>
        /// this method update the listbox items with new ones which is saved in a list with the name temp.
        /// </summary>
  
        private void UpdateIngeridientsListBox(string[] ingeridients)
        {
            if (ingeridentsListBox != null)
            {
                ingeridentsListBox.Items.Clear(); // Clear the ListBox

                if (ingeridients != null)
                {
                    for (int i = 0; i < ingeridients.Length; i++)
                    {
                        ingeridentsListBox.Items.Add(ingeridients[i]);
                    }
                }
            }
        }
        /// <summary>
        /// this method searches all the list and find the first free place and return the position(index).
        /// </summary>
        private int FindFreePlace(string[] ingeridients)
        {
            int i = 0;
            for (;i < temp.Length; i++)
                if (temp[i] == string.Empty)
                    return i;
            return i;
        }
        /// <summary>
        /// this method do nothing and just close the form.
        /// </summary>
        private void cancelIngredientBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// this method find the position of a recipe.
        /// </summary>
        private int FindingIndex(string searchingtext)
        {
            int index = 0;
            while (temp[index] != searchingtext)
            {
                index++;
            }
            return index; 
        }
        /// <summary>
        /// this method shift the cells to the empty cell.
        /// </summary>
        private void ShifingCells(int startPsition)
        {
            for (int i = startPsition + 1; i < temp.Length; i++)
            {
                temp[startPsition] = temp[i];
                startPsition++;
            }
        }
    }
}
