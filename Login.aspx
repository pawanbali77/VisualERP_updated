<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function HideLabel(lblmsg) {
            setTimeout("HideLabelHelper('" + lblmsg + "');", 5000);
        }
        function HideLabelHelper(lblmsg) {
            document.getElementById(lblmsg).style.display = "none";
        }
    </script>



    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        input {
            width: 200px;
        }

        table {
            border: 1px solid #ccc;
        }

            table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
            }


        .auto-style1 {
            width: 37%;
        }

        .auto-style2 {
            width: 147px;
        }

        .auto-style3 {
            width: 230px;
        }

        .auto-style4 {
            width: 681px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>User Login</h1>
    </div>

    <div align="center">
        <table class="auto-style1" align="center">

            <tr>
                <th colspan="3">Login Form
                </th>
            </tr>

            <tr>
                <td class="auto-style2">Email</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtEmail" runat="server" Height="22px" Width="215px" placeholder="Email"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid email." ControlToValidate="txtEmail" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" ForeColor="Red">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Password</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtPassword" runat="server" Height="22px" Width="215px" TextMode="Password" placeholder="Password"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="auto-style2"></td>
                <td class="auto-style3">
                    <asp:CheckBox ID="rememberme" runat="server" Style="float: left; margin-left: -92px;" /><span style="margin-left: -87px;">Remember Me </span>
                    <asp:Button ID="Button1" runat="server" Text="Login" ToolTip="Login" Style="height: 26px; width: 65px !important; float: right;" OnClick="btnlogin_Click" />
                </td>
                <td class="auto-style4"></td>
            </tr>

       <%--     <tr>
                <td class="auto-style2">
                    <asp:HyperLink ID="hyperlink1" NavigateUrl="#" style="white-space: nowrap;" Text="Forget Password" runat="server" />
                </td>
                <td class="auto-style3"></td>
                <td class="auto-style4">&nbsp;</td>
            </tr>--%>
            <tr>
                <td class="auto-style2"></td>
                <td class="auto-style3">
                    <asp:Label ID="lblmsg" runat="server" Font-Size="Medium" Visible="false"></asp:Label>
                </td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
        </table>
    </div>


</asp:Content>

