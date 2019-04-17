<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster2.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>Edit Profile</h1>
    </div>

    <div align="center">

        <asp:GridView ID="gv_EditProfile" Visible="false" runat="server" AutoGenerateColumns="false" DataKeyNames="RegisterID" Height="140px" Width="60%"
            ShowHeaderWhenEmpty="true" OnRowEditing="gv_EditProfile_RowEditing"
            OnRowCancelingEdit="gv_EditProfile_RowCancelingEdit" OnRowUpdating="gv_EditProfile_RowUpdating"
            OnRowDataBound="gv_EditProfile_RowDataBound">

            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Size="Small" Font-Bold="true" />
            <EmptyDataTemplate>
                <label>No user list found !</label>
            </EmptyDataTemplate>
            <RowStyle HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Style="text-align: center;" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Company Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyname" runat="server" Text='<%#Eval("Companyname") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Mobile">
                    <ItemTemplate>
                        <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtmobile" runat="server" Width="120px" MaxLength="10" onkeypress="return IsNumeric(event);" Text='<%#Eval("mobile") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:RequiredFieldValidator ID="vmobile" runat="server" ControlToValidate="txtmobile" Text="*" ForeColor="Red" ValidationGroup="asd" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Company Image">
                    <ItemTemplate>
                        <asp:Image ImageUrl='<%#Eval("Companyimage") %>' runat="server" Style="width: 45px; height: 40px;" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <a target="_blank" runat="server" id="hyview" style="cursor: pointer" visible="false">View Image </a>
                        <asp:FileUpload ID="fileUpComp" runat="server" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:RequiredFieldValidator runat="server" ID="imagerequiredField" Display="Dynamic"
                            ErrorMessage="*" ControlToValidate="fileUpComp" Style="margin: 5px; color: Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^.*([^\.][\.](([gG][iI][fF])|([Jj][pP][Gg])|[Jj][Pp][Ee]|([Jj][pP][Ee][Gg])|([Bb][mM][pP])|([Pp][nN][Gg])))"
                            Display="Dynamic" ErrorMessage="Invalid File" SetFocusOnError="true"
                            ControlToValidate="fileUpComp" Style="color: Red;"></asp:RegularExpressionValidator>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Industries">
                    <ItemTemplate>
                        <asp:Label ID="lblIndustry" runat="server" Text='<%#Eval("Industry") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlIndustry" runat="server">
                            <asp:ListItem Value="1">Service</asp:ListItem>
                            <asp:ListItem Value="2">Manufacturing</asp:ListItem>
                            <asp:ListItem Value="3">Retail</asp:ListItem>
                            <asp:ListItem Value="4">Medical</asp:ListItem>
                            <asp:ListItem Value="5">Consulting</asp:ListItem>
                            <asp:ListItem Value="6">Other</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:RequiredFieldValidator ID="vIndustries" runat="server" ControlToValidate="txtrole" Text="*" ForeColor="Red" ValidationGroup="asd" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status").ToString() == "True" ? "Active" : "Deactive" %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField HeaderText="Action" ButtonType="Link" ShowEditButton="true"
                    ItemStyle-Width="150" />
            </Columns>
        </asp:GridView>




        <asp:GridView ID="gv_EditProfile2" Visible="false" runat="server" AutoGenerateColumns="false" DataKeyNames="RegisterID" Height="140px" Width="60%"
            ShowHeaderWhenEmpty="true" OnRowEditing="gv_EditProfile2_RowEditing"
            OnRowCancelingEdit="gv_EditProfile2_RowCancelingEdit" OnRowUpdating="gv_EditProfile2_RowUpdating"
            OnRowDataBound="gv_EditProfile2_RowDataBound">

            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Size="Small" Font-Bold="true" />
            <EmptyDataTemplate>
                <label>No user list found !</label>
            </EmptyDataTemplate>
            <RowStyle HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Style="text-align: center;" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%#Eval("Username") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUsername" runat="server" Width="120px" Text='<%#Eval("Username") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:RequiredFieldValidator ID="vUsername" runat="server" ControlToValidate="txtUsername" Text="*" ForeColor="Red" ValidationGroup="asd" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Mobile">
                    <ItemTemplate>
                        <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtmobile" runat="server" Width="120px" MaxLength="10" onkeypress="return IsNumeric(event);" Text='<%#Eval("mobile") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:RequiredFieldValidator ID="vmobile" runat="server" ControlToValidate="txtmobile" Text="*" ForeColor="Red" ValidationGroup="asd" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status").ToString() == "True" ? "Active" : "Deactive" %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField HeaderText="Action" ButtonType="Link" ShowEditButton="true"
                    ItemStyle-Width="150" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>