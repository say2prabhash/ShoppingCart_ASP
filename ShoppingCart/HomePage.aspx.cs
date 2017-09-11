using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class UserChoice : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection("Data Source=TAVDESKRENT014;User Id=sa;Password=test123!@#;" +
                                       "Initial Catalog=ShoppingSite;");
            try
            {
                myConnection.Open();
                SqlDataReader myReader = null;
                //SqlCommand count = new SqlCommand("select count(Pid) from Products", myConnection);
                SqlCommand myCommand = new SqlCommand("select * from Products",
                                                         myConnection);
                //myReader = count.ExecuteReader();
                //int i = myReader.Cast<object>().Count();
                myReader = myCommand.ExecuteReader();
                int i = 0;
                while(myReader.Read())
                {
                    i++;
                }
                myReader.Close();
                SqlDataReader myReader1 = null;
                myReader1 = myCommand.ExecuteReader();
                Label[] productName = new Label[i];
                Label[] productPrice = new Label[i];
                for (int j=0;j<i;j++)
                {
                    productName[j] = new Label();
                    productName[j].Text = myReader1["PName"].ToString();
                    productPrice[j] = new Label();
                    productPrice[j].Text = myReader1["Price"].ToString();
                    this.Controls.Add(productName[j]);
                    this.Controls.Add(new LiteralControl("<br>"));
                    this.Controls.Add(productPrice[j]);
                    this.Controls.Add(new LiteralControl("<br>"));
                    this.Controls.Add(new LiteralControl("<br>"));
                }
            }
            catch(Exception exception)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}