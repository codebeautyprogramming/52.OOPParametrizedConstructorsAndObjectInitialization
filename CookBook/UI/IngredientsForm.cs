using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using DomainModel.Models;

namespace CookBook.UI
{
    public partial class IngredientsForm : Form
    {
        public IngredientsForm()
        {
            InitializeComponent();
        }

        private void AddToFridgeBtn_Click(object sender, EventArgs e)
        {
            Ingredient ingredient = new Ingredient();

            ingredient.Name = NameTxt.Text;
            ingredient.Type = TypeTxt.Text;
            ingredient.Weight = WeightNum.Value;
            ingredient.KcalPer100g = KcalPer100gNum.Value;
            ingredient.PricePer100g = PricePer100gNum.Value;

            IngredientsDataAccess db = new IngredientsDataAccess();
            db.AddIngredient(ingredient);
            ClearAllFields();
            RefreshGridData();
        }

        private void ClearAllFields()
        {
            NameTxt.Text = string.Empty;
            TypeTxt.Text = string.Empty;
            WeightNum.Value = 1;
            KcalPer100gNum.Value = 0;
            PricePer100gNum.Value = 0;
        }

        private void IngredientsForm_Load(object sender, EventArgs e)
        {
            RefreshGridData();
            CustomizeGridAppearance();
        }

        private void RefreshGridData()
        {
            IngredientsDataAccess db = new IngredientsDataAccess();
            List<Ingredient> ingredients = db.GetIngredients();
            IngredientsGrid.DataSource = ingredients;
        }

        private void CustomizeGridAppearance()
        {
            IngredientsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            IngredientsGrid.AutoGenerateColumns = false;

            DataGridViewColumn[] columns = new DataGridViewColumn[6];
            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText="Name" };
            columns[2] = new DataGridViewTextBoxColumn() { DataPropertyName = "Type", HeaderText = "Type" };
            columns[3] = new DataGridViewTextBoxColumn() { DataPropertyName = "Weight", HeaderText = "Weight" };
            columns[4] = new DataGridViewTextBoxColumn() { DataPropertyName = "PricePer100g", HeaderText = "Price (100g)" };
            columns[5] = new DataGridViewTextBoxColumn() { DataPropertyName = "KcalPer100g", HeaderText = "Kcal (100g)" };

            IngredientsGrid.Columns.Clear();
            IngredientsGrid.Columns.AddRange(columns);

        }
    }
}
