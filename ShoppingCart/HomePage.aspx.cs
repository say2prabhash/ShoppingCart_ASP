using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class UserChoice : System.Web.UI.Page
    {
        List<string> listOfItems = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btn_item1_Click(object sender, EventArgs e)
        {
            
            if (ViewState["itemsList"] != null)
            {
                listOfItems.Add(ViewState["itemsList"].ToString());
                listOfItems.Add(lbl_item1.Text);
                HttpContext.Current.Session["itemList"] = listOfItems;
            }
            else
            {
                ViewState["itemsList"] = lbl_item1.Text;
               
            }
        }

            protected void Button1_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("ShoppingCart.aspx");
        }


        protected void btn_item2_Click(object sender, EventArgs e)
        {
            if (ViewState["itemsList"] != null)
            {
                listOfItems.Add(ViewState["itemsList"].ToString());
                listOfItems.Add(lbl_item2.Text);
                HttpContext.Current.Session["itemList"] = listOfItems;
            }
            else
            {
                ViewState["itemList"] = lbl_item2.Text;

            }
        }
    }
}