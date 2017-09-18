using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class ProductRemoval : System.Web.UI.Page
    {
        Label[] productName;
        Label[] productPrice;
        Label[] productId;
        bool flag = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection("Data Source=TAVDESKRENT014;User Id=sa;Password=test123!@#;" +
                                       "Initial Catalog=ShoppingSite;");
            try
            {
                myConnection.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from Products",
                                                         myConnection);
                myReader = myCommand.ExecuteReader();
                int i = 0;
                while (myReader.Read())
                {
                    i++;
                }
                myReader.Close();
                SqlDataReader myReader1 = null;
                myReader1 = myCommand.ExecuteReader();
                productName = new Label[i];
                productPrice = new Label[i];
                productId = new Label[i];
                for (int j = 0; j < i; j++)
                {
                    myReader1.Read();
                    productName[j] = new Label();
                    productName[j].Text ="Product Name: "+ myReader1["PName"].ToString();
                    productPrice[j] = new Label();
                    productPrice[j].Text = "Price: "+myReader1["Price"].ToString();
                    productId[j] = new Label();
                    productId[j].Text = "Product ID: "+myReader1["PId"].ToString();
                    this.Form.Controls.Add(productId[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(productName[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(productPrice[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    //Button addToCart = new Button();
                    //addToCart.Text = "Add " + productName[j].Text + " to cart";
                    //addToCart.ID = productName[j].Text;
                    //addToCart.Font.Size = FontUnit.Point(10);
                    //this.Form.Controls.Add(addToCart);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                }
                myReader1.Close();
                myConnection.Close();
            }
            catch (Exception exception)
            {

            }
        }
        protected bool RemoveProductFromInventory()
        {
            SqlConnection myConnection = new SqlConnection("Data Source=TAVDESKRENT014;User Id=sa;Password=test123!@#;" +
                                      "Initial Catalog=ShoppingSite;");
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select PId from OrderDetails",myConnection);
                SqlDataReader reader = null;
                string str = "";
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    str+= reader["PId"].ToString();
                }
                string[] pId = str.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var id in pId)
                {
                    if(id.Equals(TextBox1.Text))
                    {
                        flag = false;
                        break;
                    }
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {

            }
            return flag;
        }
        protected void UpdateInventory()
        {

            SqlConnection myConnection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbcn"].ConnectionString);
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("DELETE FROM Products WHERE PId ='"+TextBox1.Text+"'", myConnection);
                SqlDataReader reader = null;
                string str = "";
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    str += reader["PId"].ToString();
                }
                string[] pId = str.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in pId)
                {
                    if (id.Equals(TextBox1.Text))
                    {
                        flag = false;
                        break;
                    }
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {

            }
        }

        protected void TextBox1_TextChanged1(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (RemoveProductFromInventory() == true)
            {
                UpdateInventory();
                Response.Redirect("UpdateSuccessfull.aspx");
            }
            else
            {
                Response.Redirect("UpdationUnsuccesfull.aspx");
            }
        }
    }
}