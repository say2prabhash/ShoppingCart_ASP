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
        bool flag = true;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
          
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

            SqlConnection myConnection = new SqlConnection("Data Source=TAVDESKRENT014;User Id=sa;Password=test123!@#;" +
                                      "Initial Catalog=ShoppingSite;");
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