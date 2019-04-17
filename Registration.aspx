<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

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

    <script type="text/javascript">

        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);

            return ret;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>Company Registration</h1>
    </div>
    <div align="center">


        <table class="auto-style1" align="center" border="0" cellpadding="0" cellspacing="0">

            <tr>
                <th colspan="3">Registration Form
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
                <td class="auto-style2">ConfirmPassword</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Height="22px" Width="215px" placeholder="Confirm Password"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtConfirmPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password not match." ForeColor="Red" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">MobileNo.</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtMobile" onkeypress="return IsNumeric(event);" MaxLength="10" runat="server" Height="22px" Width="215px" placeholder="Mobile Number"></asp:TextBox>
                </td>
                <td class="auto-style4"></td>
            </tr>
            <tr>
                <td class="auto-style2">Company Name</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtCompanyname" runat="server" Height="22px" Width="215px" placeholder="Company Name"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtCompanyname" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="auto-style2"><label>Upload Photo</label></td>
                <td class="auto-style3">
                    <a target="_blank" runat="server" id="hyview" style="cursor: pointer" visible="false">View Image </a>
                    <%-- <asp:HyperLink ID="hyview" CssClass="lightbox" runat="server" Text="View Image" />--%>
                    <asp:FileUpload ID="fileUpComp" runat="server" />
                </td>
                <td class="auto-style4">
                      <asp:RequiredFieldValidator runat="server" ID="imagerequiredField" Display="Dynamic"
                        ErrorMessage="*" ControlToValidate="fileUpComp" Style="margin: 5px; color: Red" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^.*([^\.][\.](([gG][iI][fF])|([Jj][pP][Gg])|[Jj][Pp][Ee]|([Jj][pP][Ee][Gg])|([Bb][mM][pP])|([Pp][nN][Gg])))"
                         Display="Dynamic" ErrorMessage="Invalid File" SetFocusOnError="true"
                        ControlToValidate="fileUpComp" Style="color: Red;"></asp:RegularExpressionValidator>
                </td>


            </tr>



            <tr>
                <td class="auto-style2">Select Industry</td>
                <td class="auto-style3">
                    <asp:DropDownList ID="ddlIndustries" runat="server" Height="22px" Width="215px"></asp:DropDownList>
                </td>
                <td class="auto-style4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlIndustries" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Button ID="btnsave" ToolTip="Save" runat="server" Height="34px" Text="Save" Width="105px" OnClick="btnsave_Click" />
                    <asp:Button ID="txtclear" ToolTip="Clear" runat="server" Height="34px" Text="Clear" Width="105px" OnClick="txtclear_Click" CausesValidation="false" />
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
                    <asp:HyperLink ID="hplinklogin" Visible="false" runat="server" NavigateUrl="~/Login.aspx" Font-Underline="true"> <h3>Click here to Login</h3></asp:HyperLink>
                </td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

