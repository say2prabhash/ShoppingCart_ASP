using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class UserChoice : System.Web.UI.Page
    {
        Dictionary<string, int> cartItems;
        Label[] productName;
        Label[] productPrice;
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
                while(myReader.Read())
                {
                    i++;
                }
                myReader.Close();
                SqlDataReader myReader1 = null;
                myReader1 = myCommand.ExecuteReader();
                productName = new Label[i];
                productPrice = new Label[i];
                for (int j=0;j<i;j++)
                {
                    myReader1.Read();
                    productName[j] = new Label();
                    productName[j].Text = myReader1["PName"].ToString();
                    productPrice[j] = new Label();
                    productPrice[j].Text = myReader1["Price"].ToString();
                    this.Controls.Add(productName[j]);
                    this.Controls.Add(new LiteralControl("<br>"));
                    this.Controls.Add(productPrice[j]);
                    Button addToCart = new Button();
                    addToCart.Text = "Add "+ productName[j].Text+ " to cart";
                    addToCart.ID = productName[j].Text;
                    addToCart.Font.Size = FontUnit.Point(10);
                    addToCart.Click += new EventHandler(AddToCart);
                    this.Form.Controls.Add(addToCart);
                    this.Controls.Add(new LiteralControl("<br>"));
                    this.Controls.Add(new LiteralControl("<br>"));
                }
                myReader1.Close();
                myConnection.Close();
            }
            catch(Exception exception)
            {

            }
        }
        protected void AddToCart(object sender,EventArgs e)
        {
            if (ViewState["productName"] != null && ViewState["productPrice"] != null)
            {
                cartItems = new Dictionary<string, int>();
                cartItems[ViewState["productName"].ToString()] = int.Parse(ViewState["productPrice"].ToString());
                Button b = (Button)sender;
                for (int i = 0; i < productName.Length; i++)
                {
                    if (b.ID.Equals(productName[i].Text))
                    {
                        cartItems[productName[i].Text] = int.Parse(productPrice[i].Text);
                        break;
                    }
                }
                HttpContext.Current.Session["itemList"] = cartItems;
            }
            else
            {
                cartItems = new Dictionary<string, int>();
                Button b = (Button)sender;
                for (int i = 0; i < productName.Length; i++)
                {
                    if (b.ID.Equals(productName[i].Text))
                    {
                        cartItems[productName[i].Text] = int.Parse(productPrice[i].Text);
                        ViewState["productName"] = productName[i].Text;
                        ViewState["productPrice"] = productPrice[i].Text;
                        HttpContext.Current.Session["itemList"] = cartItems;
                        break;
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShoppingCart.aspx");
        }
    }
}