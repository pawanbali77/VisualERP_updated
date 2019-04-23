<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

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
            width: 510px;
            height: 170px;
        }

        .auto-style2 {
            width: 481px;
            margin-top: 12;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>Reset Password</h1>
    </div>
    <div align="center">
        <asp:Panel ID="ResetPwdPanel" runat="server" Visible="false">
            <fieldset class="auto-style1">
                <legend style="font-size: 16px">Reset Password</legend>
                <br />
                <table class="auto-style2">
                    <tr>
                        <td style="font-size: small">New password: </td>
                        <td>
                            <asp:TextBox ID="txtNewPwd" runat="server" Height="22px" TextMode="Password"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvNewPwd" runat="server"
                                ControlToValidate="txtNewPwd" Display="Dynamic"
                                ErrorMessage="Please enter new password" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small">Confirm Passsword: </td>
                        <td>
                            <asp:TextBox ID="txtConfirmPwd" runat="server" Height="22px" TextMode="Password"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvConfirmPwd" runat="server"
                                ControlToValidate="txtConfirmPwd" Display="Dynamic"
                                ErrorMessage="Please re-enter password to confirm" ForeColor="Red"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmvConfirmPwd" runat="server"
                                ControlToCompare="txtNewPwd" ControlToValidate="txtConfirmPwd"
                                Display="Dynamic" ErrorMessage="Password didn't match" ForeColor="Red"
                                SetFocusOnError="True"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <br />
                            <asp:Button ID="btnChangePwd" runat="server" ToolTip="Change Password" Text="Change Password" OnClick="btnChangePwd_Click" Style="height: 26px; width: 120px !important;" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:Label ID="lblStatus" runat="server" Visible="false" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Label ID="lblExpired" runat="server" Text="" Style="color: #FF0000" Font-Size="Small"></asp:Label>
        <br />
        <br />
        <asp:HyperLink ID="hyperlink1" NavigateUrl="Login.aspx" Style="white-space: nowrap;" Font-Size="Medium" Text="Click here to Login" runat="server" />
    </div>
</asp:Content>

