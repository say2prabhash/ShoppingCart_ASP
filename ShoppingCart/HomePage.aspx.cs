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
        Dictionary<string, string> productId;
        Label[] productName;
        Label[] productPrice;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbcn"].ConnectionString);
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
                productId = new Dictionary<string, string>();
                for (int j=0;j<i;j++)
                {
                    myReader1.Read();
                    productName[j] = new Label();
                    productName[j].Text = myReader1["PName"].ToString();
                    productPrice[j] = new Label();
                    productPrice[j].Text = myReader1["Price"].ToString();
                    productId[productName[j].Text] = myReader1["PId"].ToString();
                    this.Form.Controls.Add(productName[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(productPrice[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    Button addToCart = new Button();
                    addToCart.Text = "Add "+ productName[j].Text+ " to cart";
                    addToCart.ID = productName[j].Text;
                    addToCart.Font.Size = FontUnit.Point(10);
                    addToCart.Click += new EventHandler(AddToCart);
                    this.Form.Controls.Add(addToCart);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                }
                myReader1.Close();
                myConnection.Close();
                HttpContext.Current.Session["productId"] = productId;
            }
            catch(Exception exception)
            {

            }
        }
        protected void AddToCart(object sender,EventArgs e)
        {
            Button b = (Button)sender;
            if (ViewState["cart"]!=null)
            {
                cartItems = new Dictionary<string, int>();
                cartItems = (Dictionary<string,int>)ViewState["cart"];
                for (int i = 0; i < productName.Length; i++)
                {
                    if (b.ID.Equals(productName[i].Text))
                    {
                        cartItems[productName[i].Text] = int.Parse(productPrice[i].Text);
                        break;
                    }
                }
                ViewState["cart"] = cartItems;
                HttpContext.Current.Session["itemList"] = cartItems;
            }
            else
            {
                cartItems = new Dictionary<string, int>();
                
                for (int i = 0; i < productName.Length; i++)
                {
                    if (b.ID.Equals(productName[i].Text))
                    {
                        cartItems[productName[i].Text] = int.Parse(productPrice[i].Text);
                        ViewState["cart"] = cartItems;
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