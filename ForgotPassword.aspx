<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function HideLabel(lblStatus) {
            setTimeout("HideLabelHelper('" + lblStatus + "');", 5000);
        }
        function HideLabelHelper(lblStatus) {
            document.getElementById(lblStatus).style.display = "none";
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 456px;
            height: 162px;
        }

        .auto-style2 {
            width: 392px;
            height: 121px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>Forgot Password</h1>
    </div>
    <div align="center">
        <fieldset class="auto-style1">
            <legend style="font-size: medium">Recover Password By Email</legend>
            <table class="auto-style2">
                <tr>
                    <td style="font-size: small"> Email Id : </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Height="22px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailId" runat="server"
                            ErrorMessage="Please enter valid email"
                            ControlToValidate="txtEmail" Display="Dynamic"
                            ForeColor="Red" SetFocusOnError="True"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmit_Click" Style="height: 26px; width: 65px !important;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblStatus" runat="server" Font-Size="Small" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="hyperlink1" NavigateUrl="Login.aspx" Style="white-space: nowrap;" Font-Size="Small" Text="Click here to Login" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>

