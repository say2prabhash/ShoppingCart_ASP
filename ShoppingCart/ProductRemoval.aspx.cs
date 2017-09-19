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
        Label[] lbl_productName;
        Label[] lbl_productPrice;
        Label[] lbl_productId;
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
                lbl_productName = new Label[i];
                lbl_productPrice = new Label[i];
                lbl_productId = new Label[i];
                for (int j = 0; j < i; j++)
                {
                    myReader1.Read();
                    lbl_productName[j] = new Label();
                    lbl_productName[j].Text ="Product Name: "+ myReader1["PName"].ToString();
                    lbl_productPrice[j] = new Label();
                    lbl_productPrice[j].Text = "Price: "+myReader1["Price"].ToString();
                    lbl_productId[j] = new Label();
                    lbl_productId[j].Text = "Product ID: "+myReader1["PId"].ToString();
                    this.Form.Controls.Add(lbl_productId[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(lbl_productName[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(lbl_productPrice[j]);
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
            catch (SqlException dataBaseException)
            {
                Response.Redirect("DatabaseConnectionProblem.aspx");
            }
            catch (Exception exception)
            {
                Response.Write("PageLoadiingProblem.aspx");
            }
            finally
            {
                myConnection.Close();
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
                    if(id.Equals(txt_PId.Text))
                    {
                        flag = false;
                        break;
                    }
                }
                myConnection.Close();
            }
            catch (SqlException dataBaseException)
            {
                Response.Redirect("DatabaseConnectionProblem.aspx");
            }
            finally
            {
                myConnection.Close();
            }
            return flag;
        }
        protected void UpdateInventory()
        {

            SqlConnection myConnection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbcn"].ConnectionString);
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("DELETE FROM Products WHERE PId ='"+txt_PId.Text+"'", myConnection);
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
                    if (id.Equals(txt_PId.Text))
                    {
                        flag = false;
                        break;
                    }
                }
                myConnection.Close();
            }
            catch (SqlException dataBaseException)
            {
                Response.Redirect("DatabaseConnectionProblem.aspx");
            }
            finally
            {
                myConnection.Close();
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