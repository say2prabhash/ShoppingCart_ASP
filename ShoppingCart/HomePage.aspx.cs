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
        Label[] lbl_productName;
        Label[] lbl_productPrice;
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
                lbl_productName = new Label[i];
                lbl_productPrice = new Label[i];
                productId = new Dictionary<string, string>();
                for (int j=0;j<i;j++)
                {
                    myReader1.Read();
                    lbl_productName[j] = new Label();
                    lbl_productName[j].Text = myReader1["PName"].ToString();
                    lbl_productPrice[j] = new Label();
                    lbl_productPrice[j].Text = myReader1["Price"].ToString();
                    productId[lbl_productName[j].Text] = myReader1["PId"].ToString();
                    this.Form.Controls.Add(lbl_productName[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(lbl_productPrice[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    Button btn_addToCart = new Button();
                    btn_addToCart.Text = "Add "+ lbl_productName[j].Text+ " to cart";
                    btn_addToCart.ID = lbl_productName[j].Text;
                    btn_addToCart.Font.Size = FontUnit.Point(10);
                    btn_addToCart.Click += new EventHandler(AddToCart);
                    this.Form.Controls.Add(btn_addToCart);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                }
                myReader1.Close();
                myConnection.Close();
                HttpContext.Current.Session["productId"] = productId;
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
        protected void AddToCart(object sender,EventArgs e)
        {
            Button b = (Button)sender;
            if (ViewState["cart"]!=null)
            {
                cartItems = new Dictionary<string, int>();
                cartItems = (Dictionary<string,int>)ViewState["cart"];
                for (int i = 0; i < lbl_productName.Length; i++)
                {
                    if (b.ID.Equals(lbl_productName[i].Text))
                    {
                        cartItems[lbl_productName[i].Text] = int.Parse(lbl_productPrice[i].Text);
                        break;
                    }
                }
                ViewState["cart"] = cartItems;
                HttpContext.Current.Session["itemList"] = cartItems;
            }
            else
            {
                cartItems = new Dictionary<string, int>();
                
                for (int i = 0; i < lbl_productName.Length; i++)
                {
                    if (b.ID.Equals(lbl_productName[i].Text))
                    {
                        cartItems[lbl_productName[i].Text] = int.Parse(lbl_productPrice[i].Text);
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