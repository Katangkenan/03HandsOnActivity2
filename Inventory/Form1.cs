using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;
        private Boolean ProductGoods = false;

        BindingSource showProductList = new BindingSource();

        public frmAddProduct()
        {
            InitializeComponent();
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
                {
                    "Beverages",
                    "Bread/Bakery",
                    "Canned/Jarred Goods",
                    "Dairy",
                    "Frozen Goods",
                    "Meat",
                    "Personal Care",
                    "Other"
                };
            foreach (string item in ListOfProductCategory)
            {
                cbCategory.Items.Add(item);
            }
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new Product();
            }
            return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]+$"))
            {
                throw new Number();
            }
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new Currency();
            }
            return Convert.ToDouble(price);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                ProductGoods = false;

                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy=MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;

                ProductGoods = true;
            }

            catch (Product)
            {
                MessageBox.Show("Error! Letters Only!");
            }
            catch (Number)
            {
                MessageBox.Show("Error! Numbers Only!");
            }
            catch (Currency)
            {
                MessageBox.Show("Error! Numbers Only!");
            }
            finally
            {
                if (ProductGoods == true)
                {
                    MessageBox.Show("Product Goods Added!");
                }
            }

        }
    }


    public class Product : Exception
    {
        public Product(string NameVariable) : base(NameVariable) { }
        public Product() { }
    };

    public class Number : Exception
    {
        public Number(string NameVariable) : base(NameVariable) { }
        public Number() { }
    };

    public class Currency : Exception
    {
        public Currency(string NameVariable) : base(NameVariable) { }
        public Currency() { }
    }
}