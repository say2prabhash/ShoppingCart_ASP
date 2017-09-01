<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="ShoppingCart.Inventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Quantity of item in inventory"></asp:Label>
        <p>
            <asp:Label ID="lbl_item1" runat="server" Text="Item1"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lbl_quantityItem1" runat="server">100</asp:Label>
        </p>
        <p>
            <asp:Label ID="lbl_item2" runat="server" Text="Item2"></asp:Label>
        </p>
        <asp:Label ID="lbl_quantityItem2" runat="server" Text="100"></asp:Label>
    </form>
</body>
</html>
