<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster2.master" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="UserRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }
    </script>
    <script type="text/javascript">
        function HideLabel(lblmsg) {
            setTimeout("HideLabelHelper('" + lblmsg + "');", 5000);
        }
        function HideLabelHelper(lblmsg) {
            document.getElementById(lblmsg).style.display = "none";
        }
    </script>
     <style type="text/css">
        /*body {
            font-family: Arial;
            font-size: 10pt;
        }

        input {
            width: 200px;
        }*/

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
            width: 40%;
        }

        .auto-style2 {
            width: 147px;
        }

        .auto-style3 {
            width: 238px;
        }

        .auto-style4 {
            width: 681px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>Add New User Registration</h1>
    </div>
    <div align="center">
        <table class="auto-style1" align="center" border="0" cellpadding="0" cellspacing="0">

            <tr>
                <th colspan="3">User Registration Form
                </th>
            </tr>
            <tr>
                <td class="auto-style2">UserName</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtUsername" runat="server" Height="22px" Width="215px" Placeholder="UserName"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtUsername" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">Email</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtEmail" runat="server" Height="22px" Width="215px" Placeholder="Email"></asp:TextBox>
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
                    <asp:TextBox ID="txtPassword" runat="server" Height="22px" Width="215px" TextMode="Password" Placeholder="Password"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">ConfirmPassword</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Height="22px" Width="215px" Placeholder="Confirm Password"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtConfirmPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password not match." ForeColor="Red" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">MobileNo.</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtMobile" onkeypress="return IsNumeric(event);" MaxLength="10" runat="server" Height="22px" Width="215px" Placeholder="Mobile Number"></asp:TextBox>
                </td>
                <td class="auto-style4"></td>
            </tr>

            <tr>
                <td class="auto-style2">Role</td>
                <td class="auto-style3">
                    <asp:DropDownList ID="ddlRole" runat="server" Height="22px" Width="215px"></asp:DropDownList>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlRole" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Button ID="btnsave" ToolTip="Save" runat="server" Height="34px" Text="Save" Width="107px" OnClick="btnsave_Click" />
                    <asp:Button ID="txtclear" ToolTip="Clear" runat="server" Height="34px" Text="Clear" Width="107px" CausesValidation="false" OnClick="txtclear_Click" />
                </td>
                <td class="auto-style4"></td>
            </tr>

            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Label ID="lblmsg" runat="server" Font-Size="Medium" Visible="false"></asp:Label>
                </td>
                <td class="auto-style4">&nbsp;</td>
            </tr>

            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <%--<asp:HyperLink ID="hplinklogin" Visible="false" runat="server" NavigateUrl="~/Login.aspx" Font-Underline="true" > <h3>Click here to Login</h3></asp:HyperLink>--%>
                </td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
        </table>
    </div>

   
</asp:Content>

