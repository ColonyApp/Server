<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="ColonyServerWebApplication.Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 55%;
        }
        .auto-style2 {
            width: 463px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="ButtonWant" runat="server" Text="Want!" Width="458px" OnClick="ButtonWant_Click"  />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="ButtonGet" runat="server" Text="Get!" Width="458px" OnClick="ButtonGet_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="ButtonSearch" runat="server" Text="Search!" Width="458px" OnClick="ButtonSearch_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="ButtonGive" runat="server" Text="Give!" Width="458px" OnClick="ButtonGive_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="ButtonConfig" runat="server" Text="Config" Width="458px" OnClick="ButtonConfig_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
