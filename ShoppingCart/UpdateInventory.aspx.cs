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
        Label[] lbl_productName;
        Label[] lbl_productPrice;
        Label[] lbl_productId;
        TextBox[] txt_product;
        TextBox[] txt_price;
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
                txt_product = new TextBox[i];
                txt_price = new TextBox[i];
                Label []pId = new Label[i];
                for (int j = 0; j < i; j++)
                {
                    myReader1.Read();
                    lbl_productName[j] = new Label();
                    lbl_productName[j].Text = "Product Name: ";
                    txt_product[j] = new TextBox();
                    txt_product[j].Text= myReader1["PName"].ToString();
                    lbl_productPrice[j] = new Label();
                    lbl_productPrice[j].Text = "Price: ";
                    txt_price[j] = new TextBox();
                    txt_price[j].Text=myReader1["Price"].ToString();
                    pId[j] = new Label();
                    pId[j].Text = "ProductId: ";
                    lbl_productId[j] = new Label();
                    lbl_productId[j].Text =myReader1["PId"].ToString();
                    txt_product[j].ID = lbl_productId[j].Text + "_name";
                    txt_price[j].ID = lbl_productId[j].Text + "_price";
                    this.Form.Controls.Add(pId[j]);
                    this.Form.Controls.Add(lbl_productId[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(lbl_productName[j]);
                    this.Form.Controls.Add(txt_product[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    this.Form.Controls.Add(lbl_productPrice[j]);
                    this.Form.Controls.Add(txt_price[j]);
                    this.Form.Controls.Add(new LiteralControl("<br>"));
                    Button edit_btn = new Button();
                    edit_btn.Text = "Edit";
                    edit_btn.ID = j.ToString()+"_"+lbl_productId[j].Text;
                    edit_btn.Font.Size = FontUnit.Point(10);
                    AddEvent(edit_btn);
                    this.Form.Controls.Add(edit_btn);
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
        protected void AddEvent(Button b)
        {
            b.Click += new EventHandler(Button1_Click);
        }
        protected void Edit(object sender,EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string str = btn.ID.ToString();
            string[] str1 = str.Split('_');
            int i = int.Parse(str1[0]);
            SqlConnection myConnection = new SqlConnection("Data Source=TAVDESKRENT014;User Id=sa;Password=test123!@#;" +
                                       "Initial Catalog=ShoppingSite;");
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("Update Products SET PName='" + txt_product[i].Text + "',Price='" + txt_price[i].Text + "' where PId='" + str1[1]+"'", myConnection);
                myCommand.ExecuteNonQuery();
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
    }
}