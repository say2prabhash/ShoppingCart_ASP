using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class OrderGeneration : System.Web.UI.Page
    {
        Dictionary<string, int> items;
        int total;
        DateTime date = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            items = (Dictionary<string, int>)(HttpContext.Current.Session["itemsOrdered"]);
            total = int.Parse(HttpContext.Current.Session["total"].ToString());
            SqlConnection myConnection = new SqlConnection("Data Source=TAVDESKRENT014;User Id=sa;Password=test123!@#;" +
                                       "Initial Catalog=ShoppingSite;");
            try
            {
                myConnection.Open();
   
                SqlCommand myCommand = new SqlCommand("INSERT into Orders(Date,TotalAmount)Values('"+date+"','"+ total + "')",myConnection);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception exception)
            {

            }
        }
    }
}