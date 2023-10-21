using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace assigment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int maxNumberOfIngredients = 50;
        const int maxNumberOfRecipes = 200;
        RecipeManager recipeControler;
        Recipe currRecipe;
        /// <summary>
        /// Default constructor 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBox(categoryComboBox);
            currRecipe = new Recipe();
            recipeControler = new RecipeManager();
            currRecipe.SetMax(maxNumberOfIngredients);
            recipeControler.SetMaxList(maxNumberOfRecipes);

        }
        /// <summary>
        /// initializing the conbo box to show the fodd category elements
        /// </summary>
        private void InitializeComboBox(ComboBox comboBox)
        {
            Array values = Enum.GetValues(typeof(FoodCategory));// this code will make an array of values of food category
            // this array is for amking the an Enum itterable
            for (int i = 0; i < values.Length; i++)
            {
                FoodCategory category = (FoodCategory)values.GetValue(i);
                comboBox.Items.Add(category); // Add the individual enum value
            }
        }
        private void recipeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }
        /// <summary>
        /// This method is for showing the form of adding ingeredients
        /// </summary>
        private void addIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new AddIngredientForm
            AddIngredientForm dlg = new AddIngredientForm(currRecipe);
 
            // Show the form as a dialog
            dlg.ShowDialog();

            // Check the DialogResult
            if (dlg.DialogResult == true)
            {
                // User has finished editing
                string[] updatedIngredients = dlg.GetIngredientsList();

                // Update currRecipe with the updated ingredients
                currRecipe.SetIngredients(updatedIngredients);
            }
            else
            {
                MessageBox.Show("Editing was canceled or no ingredients added.");
            }

        }


        private void recipesListTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void categoryComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void categoryComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// this method is reading category from the combo box. it returns false if something is wrong in the method
        /// </summary>
        private bool ReadCategory()
        {
            if (categoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("You should select a category for your recipe!");//if user forgot to choose category, this message box will inro, they
                return false;
            }
            else
            {
                currRecipe.SetCategory((FoodCategory) categoryComboBox.SelectedItem);
                return true; //returning true if the user choose correctly.
                
            }
        }
        /// <summary>
        /// this method is reading name from the text box. it returns false if something is wrong in the method
        /// </summary>
        private bool ReadName()
        {
            if (newRecipeTextBox.Text == "")
            {
                MessageBox.Show("You should select a name for your recipe!");
                return false; // if the user forgot to enter the name, this message box will inform they.
            }
            else
            {
                  currRecipe.SetName(newRecipeTextBox.Text);
                  return true;// returning true when the name is filled correctly
                
            }
        }
        /// <summary>
        /// this method is reading description from the text box. it returns false if something is wrong in the method
        /// </summary>
        private bool ReadDescription()
        {
            if (recipeDescriptionTextBox.Text == "")
            {
                MessageBox.Show("You should select a name for your recipe!");
                return false;// if the user forgot to enter the name, this message box will inform they.
            }
            else
            {
                currRecipe.SetDescription(recipeDescriptionTextBox.Text);
                return true;// returning true when the description is filled correctly

            }
        }
        /// <summary>
        /// this method is for adding a new recipe to the listbox
        /// </summary>
        private void addRecipeBotton_Click(object sender, RoutedEventArgs e)
        {
            if (ReadCategory() && ReadName() && ReadDescription())
            {
                bool ok = recipeControler.AddReciep(currRecipe);
                string name = currRecipe.GetName();
                name = name.PadRight(90 - name.Length);
                string foodtype = currRecipe.GetCategory().ToString();
                foodtype = foodtype.PadRight(60-foodtype.Length);
                string ingredientsNumber = currRecipe.currentNumberOfIngredientd().ToString();
                 ingredientsNumber = ingredientsNumber.PadRight(10-ingredientsNumber.Length);

                // Combine the formatted fields into a single string
                string newRow = name + foodtype + ingredientsNumber;

                allRecipeListBox.Items.Add(newRow);
                newRecipeTextBox.Text = string.Empty;
                categoryComboBox.SelectedIndex = -1;
                recipeDescriptionTextBox.Text = string.Empty;
                currRecipe = new Recipe();
            }
            else
            {
                MessageBox.Show("Something is null", "Error");
            }

        }
        /// <summary>
        /// this method is for editing a recipe to the listbox
        /// </summary>
        private void startEditButton_Click(object sender, RoutedEventArgs e)
        {

            if (allRecipeListBox.SelectedItem != null)
            {
                string selectedItemText = allRecipeListBox.SelectedItem.ToString();
                allRecipeListBox.Items.Remove(selectedItemText);
                string name = selectedItemText.Substring(0, 90).Trim();// finding the name of recipe which the user wants to find
                int recipesIndex = recipeControler.FindRecipeIndexByName(name);
                currRecipe = recipeControler.GetListOfAllRecipes()[recipesIndex];
                newRecipeTextBox.Text = currRecipe.GetName();
                categoryComboBox.SelectedIndex = currRecipe.GetCategoryIndex(currRecipe.GetCategory());
                recipeDescriptionTextBox.Text = currRecipe.GetDescription();
                AddIngredientForm dlg = new AddIngredientForm(currRecipe);//when user press edit, first the ingeridients will be shown
                dlg.numberOfIngredientsLabel.Content = currRecipe.GetNumberOfElement();
                dlg.SetingredientsNum(currRecipe.GetNumberOfElement());
                for (int i = 0; i < currRecipe.GetIngredients().Length; i++)
                {
                    dlg.ingeridentsListBox.Items.Add(currRecipe.GetIngredients()[i]);
                }
                dlg.ShowDialog();
                if (dlg.DialogResult == true)
                {
                    // User has finished editing
                    string[] updatedIngredients = dlg.GetIngredientsList();

                    // Update currRecipe with the updated ingredients
                    currRecipe.SetIngredients(updatedIngredients);
                }
                addRecipeBotton.IsEnabled = false;
                recipeControler.DeleteRecipe(currRecipe);
            }
            else
            {
                MessageBox.Show("You must choose a recipe first then edit it!!", "Error");
            }

        }
        /// <summary>
        /// this method is for finishing editing a recipe to the listbox
        /// </summary>

        private void editFinishButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReadCategory() && ReadName() && ReadDescription())
            {
                bool ok = recipeControler.AddReciep(currRecipe);
                string name = currRecipe.GetName();
                name = name.PadRight(90 - name.Length);
                string foodtype = currRecipe.GetCategory().ToString();
                foodtype = foodtype.PadRight(60 - foodtype.Length);
                string ingredientsNumber = currRecipe.currentNumberOfIngredientd().ToString();
                ingredientsNumber = ingredientsNumber.PadRight(10 - ingredientsNumber.Length);

                // Combine the formatted fields into a single string
                string newRow = name + foodtype + ingredientsNumber;
                allRecipeListBox.Items.Add(newRow);
                newRecipeTextBox.Text = string.Empty;
                categoryComboBox.SelectedIndex = -1;
                recipeDescriptionTextBox.Text = string.Empty;
                addRecipeBotton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Somthing is null", "Error");

            }
        }
        /// <summary>
        /// this method is for deleting a recipe to the listbox
        /// </summary>

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedItemText = allRecipeListBox.SelectedItem.ToString();
            allRecipeListBox.Items.Remove(selectedItemText);
            string name = selectedItemText.Substring(0, 90).Trim(); 
            int recipesIndex = recipeControler.FindRecipeIndexByName(name);
            currRecipe = recipeControler.GetListOfAllRecipes()[recipesIndex];
            recipeControler.DeleteRecipe(currRecipe);
        }
        /// <summary>
        /// this method is for clearing any selecting recipe
        /// </summary>
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            allRecipeListBox.SelectedItem = null;
        }
        /// <summary>
        /// this method is for showing the details of a recipe on lst box
        /// </summary>
        private void itemListBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (allRecipeListBox.SelectedItem != null)
            {
                string selectedItemText = allRecipeListBox.SelectedItem.ToString();
                string name = selectedItemText.Substring(0, 90).Trim();
                int recipesIndex = recipeControler.FindRecipeIndexByName(name);
                currRecipe = recipeControler.GetListOfAllRecipes()[recipesIndex];
                string message = "INGREDIENTS\n";//this is for producing a message to show on message box
                for (int i = 0; i < currRecipe.currentNumberOfIngredientd() - 1; i++)
                    message = message + currRecipe.GetIngredients()[i].ToString() + ", ";
                message = message + currRecipe.GetIngredients()[currRecipe.currentNumberOfIngredientd() - 1].ToString() + "\n";
                message = message + currRecipe.GetDescription();
                MessageBox.Show(message, "COOKING INSTRUCTIONs");
            }
            else 
            {
                MessageBox.Show("You should choose an item!!");
            }
        }


        private void allRecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
