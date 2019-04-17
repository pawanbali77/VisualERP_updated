<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster2.master" AutoEventWireup="true" CodeFile="First_Dashboard.aspx.cs" Inherits="First_Dashboard" %>

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
    <style type="text/css">
        .gvItemCenter {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>All Users</h1>
    </div>
    <div align="center">
        <asp:GridView ID="gv_alluser" runat="server" AutoGenerateColumns="false" DataKeyNames="RegisterID" Height="140px" Width="60%"
            ShowHeaderWhenEmpty="true"
            OnRowDataBound="gv_alluser_RowDataBound" OnRowEditing="gv_alluser_RowEditing"
            OnRowCancelingEdit="gv_alluser_RowCancelingEdit" OnRowUpdating="gv_alluser_RowUpdating"
            OnRowDeleting="gv_alluser_RowDeleting">

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
                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Name">--%>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblusername" Style="padding-left: 10px;" runat="server" Text='<%#Eval("username") %>' />
                    </ItemTemplate>
                    <ItemStyle CssClass="gvItemCenter" />

                    <EditItemTemplate>
                        <asp:TextBox ID="txtusername" runat="server" Width="120px" Text='<%#Eval("username") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtusername" Width="80px" runat="server" />
                        <asp:RequiredFieldValidator ID="vemail" runat="server" ControlToValidate="txtusername" Text="*" ForeColor="Red" ValidationGroup="validaiton" />
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
                        <asp:TextBox ID="txtmobile" Width="80px" runat="server" />
                        <asp:RequiredFieldValidator ID="vmobile" runat="server" ControlToValidate="txtmobile" Text="*" ForeColor="Red" ValidationGroup="validaiton" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Role">
                    <ItemTemplate>
                        <asp:Label ID="lblrole" runat="server" Text='<%#Eval("Userrole") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlrole" runat="server">
                            <asp:ListItem Value="0">Select Role</asp:ListItem>
                            <asp:ListItem Value="1">Site Admins</asp:ListItem>
                            <asp:ListItem Value="2">Process Owner</asp:ListItem>
                            <asp:ListItem Value="3">Enterprise Owner</asp:ListItem>
                            <asp:ListItem Value="4">Viewers</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtrole" Width="80px" runat="server" />
                        <asp:RequiredFieldValidator ID="vrole" runat="server" ControlToValidate="txtrole" Text="*" ForeColor="Red" ValidationGroup="validaiton" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status").ToString() == "True" ? "Active" : "Deactive" %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlstatus" runat="server">
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="0">Deactive</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtstatus" Width="80px" runat="server" />
                        <asp:RequiredFieldValidator ID="vstatus" runat="server" ControlToValidate="txtstatus" Text="*" ForeColor="Red" ValidationGroup="validaiton" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:CommandField HeaderText="Action" ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                    ItemStyle-Width="150" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

