using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class Inventory : System.Web.UI.Page
    {
        string productName = "";
        string pId = "";
        int price;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            productName = TextBox1.Text;
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            pId = TextBox2.Text;
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            price = int.Parse(TextBox3.Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UpdateInventory();
            Response.Redirect("HomePage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
        protected void UpdateInventory()
        {
            SqlConnection myConnection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbcn"].ConnectionString);
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("INSERT into Products(PId,PName,Price)Values('" + pId + "','" + productName + "','" + price + "')", myConnection);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}