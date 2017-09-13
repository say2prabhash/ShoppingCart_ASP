<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductRemoval.aspx.cs" Inherits="ShoppingCart.ProductRemoval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Enter the Product Id"></asp:Label>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Remove" />
    </form>
</body>
</html>
