using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Generic.Dictionary<string, int>;

namespace ShoppingCart
{
    public partial class OrderGeneration : System.Web.UI.Page
    {
        Dictionary<string, int> items;
        int total;
        DateTime date = DateTime.Now;
        List<string> productIdList;
        Dictionary<string, string> productId;
        protected void Page_Load(object sender, EventArgs e)
        {
            productId = (Dictionary<string, string>)(HttpContext.Current.Session["productId"]);
            items = (Dictionary<string, int>)(HttpContext.Current.Session["itemsOrdered"]);
            total = int.Parse(HttpContext.Current.Session["total"].ToString());
            SqlConnection myConnection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbcn"].ConnectionString);
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("INSERT into Orders(Date,TotalAmount)Values(Current_TimeStamp,'" + total + "')", myConnection);
                myCommand.ExecuteNonQuery();
                KeyCollection keys = items.Keys;
                List<string> itemsList = keys.ToList<string>();
                productIdList = new List<string>();
                foreach(var item in itemsList)
                {
                    if(productId.ContainsKey(item))
                    {
                        productIdList.Add(productId[item]);
                    }
                }
                string pId = "";
                foreach(var id in productIdList)
                {
                    pId += id+",";
                }
                SqlCommand getOrderId = new SqlCommand("select max(OId) from Orders",myConnection);
                int oId = int.Parse(getOrderId.ExecuteScalar().ToString());
                SqlCommand updateOrderDetails = new SqlCommand("INSERT into OrderDetails(PId,OId,Quantity,Price)Values('" + pId + "','" + oId + "'," + "'1','" + total + "')", myConnection);
                updateOrderDetails.ExecuteNonQuery();
                myConnection.Close();
                Label lbl = new Label();
                lbl.Text = "Thank You for placing order";
                this.Controls.Add(lbl);
                myConnection.Close();
            }
            catch (Exception exception)
            {

            }
        }
    }
}