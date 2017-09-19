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
        <asp:Label ID="name_lbl" runat="server" Text="Product Name"></asp:Label>
        <asp:TextBox ID="txt_PName" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Product Id"></asp:Label>
            <asp:TextBox ID="txt_PId" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        </p>
        <asp:Label ID="Label2" runat="server" Text="Price of the product"></asp:Label>
        <asp:TextBox ID="txt_Price" runat="server" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
        <p style="margin-left: 80px">
            <asp:Button ID="btn_add" runat="server" OnClick="Button1_Click" Text="Add" />
            <asp:Button ID="btn_cancel" runat="server" OnClick="Button2_Click" Text="Cancel" />
        </p>
    </form>
</body>
</html>
