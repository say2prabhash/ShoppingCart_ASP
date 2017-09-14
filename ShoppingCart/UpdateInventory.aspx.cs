using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class UpdateInventory : System.Web.UI.Page
    {
        Label[] productName;
        Label[] productPrice;
        Label[] productId;
        TextBox[] product;
        TextBox[] price;
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
                product = new TextBox[i];
                price = new TextBox[i];
                Label []pId = new Label[i];
                for (int j = 0; j < i; j++)
                {
                    myReader1.Read();
                    productName[j] = new Label();
                    productName[j].Text = "Product Name: ";
                    product[j] = new TextBox();
                    product[j].Text= myReader1["PName"].ToString();
                    productPrice[j] = new Label();
                    productPrice[j].Text = "Price: ";
                    price[j] = new TextBox();
                    price[j].Text=myReader1["Price"].ToString();
                    pId[j] = new Label();
                    pId[j].Text = "ProductId: ";
                    productId[j] = new Label();
                    productId[j].Text =myReader1["PId"].ToString();
                    product[j].ID = productId[j].Text + "_name";
                    price[j].ID = productId[j].Text + "_price";
                    this.Form.Controls.Add(pId[j]);
                    this.Form.Controls.Add(productId[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(productName[j]);
                    this.Form.Controls.Add(product[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(productPrice[j]);
                    this.Form.Controls.Add(price[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    Button edit = new Button();
                    edit.Text = "Edit";
                    edit.ID = j.ToString()+"_"+productId[j].Text;
                    edit.Font.Size = FontUnit.Point(10);
                    AddEvent(edit);
                    this.Form.Controls.Add(edit);
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
        protected void AddEvent(Button b)
        {
            b.Click += new EventHandler(Button1_Click);
        }
        protected void Edit(object sender,EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string str = b.ID.ToString();
            string[] str1 = str.Split('_');
            int i = int.Parse(str1[0]);
            SqlConnection myConnection = new SqlConnection("Data Source=TAVDESKRENT014;User Id=sa;Password=test123!@#;" +
                                       "Initial Catalog=ShoppingSite;");
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("Update Products SET PName='" + product[i].Text + "',Price='" + price[i].Text + "' where PId='" + str1[1]+"'", myConnection);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}