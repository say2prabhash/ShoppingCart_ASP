using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Generic.Dictionary<string, int>;

namespace ShoppingCart
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        Dictionary<string, int> itemsInCart;
        Label[] lbl_price;
        Label[] lbl_productName;
        int totalPrice = 0;
        Label lbl_orderTotal;
        protected void Page_Load(object sender, EventArgs e)
        {
            itemsInCart = (Dictionary<string,int>) HttpContext.Current.Session["itemList"];
            lbl_price = new Label[itemsInCart.Count];
            lbl_productName = new Label[itemsInCart.Count];
            KeyCollection keys = itemsInCart.Keys;
            List<string> items = keys.ToList<string>();
            for (int i=1;i<=itemsInCart.Count;i++)
            {

                lbl_productName[i-1] = new Label();
                lbl_productName[i-1].ID = "lbl_item" + i;
                lbl_productName[i - 1].Text = items[i-1];
                lbl_productName[i-1].Style["Position"] = "Absolute";
                this.Controls.Add(lbl_productName[i-1]);
                this.Controls.Add(new LiteralControl("<br>"));
                lbl_price[i - 1] = new Label();
                lbl_price[i - 1].ID = "btn_item" + i;
                totalPrice += itemsInCart[items[i - 1]];
                lbl_price[i - 1].Text = itemsInCart[items[i-1]].ToString();
                lbl_price[i - 1].Style["Position"] = "Absolute";
                this.Controls.Add(lbl_price[i - 1]);
                this.Controls.Add(new LiteralControl("<br>"));
                this.Controls.Add(new LiteralControl("<br>"));
            }
            OrderTotalCalculator();
        }
        protected void OrderTotalCalculator()
        {
            lbl_orderTotal = new Label();
            lbl_orderTotal.ID = "orderPrice_lbl";
            lbl_orderTotal.Text = totalPrice.ToString();
            Label total = new Label();
            total.ID = "Total_lbl";
            total.Text = "Total Price: ";
            this.Controls.Add(total);
            this.Controls.Add(lbl_orderTotal);
            ViewState["total"] = lbl_orderTotal.Text;
            ViewState["item"] = itemsInCart;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> items = new Dictionary<string, int>();
            if(ViewState["item"]!=null)
            {
                itemsInCart = (Dictionary<string,int>)ViewState["item"];
            }
            for(int i=0;i<itemsInCart.Count;i++)
            {
                int q;
                int.TryParse(lbl_price[i].Text, out q);
                items[lbl_productName[i].Text] = q;
            }
            HttpContext.Current.Session["itemsOrdered"] = itemsInCart;
            HttpContext.Current.Session["total"] = ViewState["total"].ToString();
            Response.Redirect("OrderGeneration.aspx");
        }
    }
}