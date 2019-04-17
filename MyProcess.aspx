<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster2.master" AutoEventWireup="true" CodeFile="MyProcess.aspx.cs" Inherits="MyProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>Process List</h1>
    </div>

    <div align="center">
        <asp:GridView ID="gv_process" runat="server" AutoGenerateColumns="false" Height="140px" Width="30%"
            ShowHeaderWhenEmpty="true" OnRowCommand="gv_process_RowCommand" OnRowDeleting="gv_process_RowDeleting" OnRowDataBound="gv_process_RowDataBound" OnRowCreated="gv_process_RowCreated">
        
            <EmptyDataRowStyle forecolor="Red" HorizontalAlign="Center" Font-Size="Small" Font-Bold="true"/>
            <EmptyDataTemplate>
                <label>No process list found !</label>
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
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Process Name">
                    <ItemTemplate>
                        <asp:Label ID="lblProcessName" Style="text-align: left" runat="server" Text='<%#Eval("ProcessName") %>' />
                        <asp:Label ID="lblProcessId" Style="text-align: left" runat="server" Visible="false" Text='<%#Eval("ProcessId") %>' />
                        <asp:Label ID="lblCompanyID" Style="text-align: left" runat="server" Visible="false" Text='<%#Eval("CompanyID") %>' />
                        <asp:Label ID="lblUserID" Style="text-align: left" runat="server" Visible="false" Text='<%#Eval("UserID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="View Process">
                    <ItemTemplate>
                        <asp:LinkButton Text="View" runat="server" CommandName="View" CommandArgument="<%# Container.DataItemIndex %>" Height="18px" Width="60px" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnDelete" Text="Delete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" Height="18px" Width="60px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>



        </asp:GridView>

        <asp:LinkButton Text="Click here for create process" ID="lnkCreateProcess" OnClick="lblCreateProcess_Click" Visible="false" runat="server" />



    </div>



</asp:Content>

