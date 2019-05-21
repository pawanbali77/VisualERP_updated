
<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="UploadFiles.aspx.cs" Inherits="UploadFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>Upload Training Material Videos</h1>
    </div>

     <div align="center">
          <asp:FileUpload ID="FileUploadpost" runat="server" Height="45px" Width="348px" />
            <asp:TextBox ID="TextBixcomment" Placeholder="Enter your filename" runat="server" Height="30px" />
            <asp:Button ID="Button1" Text="Upload" OnClick="LinkBPOST_Click" runat="server" Height="31px" Width="91px"/>
            &nbsp; <asp:Label ID="ErrorMsg" CssClass="ErrorMsg" runat="server" Visible="false" Font-Size="16px"></asp:Label>
            <br />
            <br />
            <hr />

            <div align="center">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" Height="140px" Width="60%"
                    ShowHeaderWhenEmpty="true"
                    OnRowDeleting="GridView1_RowDeleting">
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
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Style="text-align: center;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblfilename" Style="padding-left: 10px;" runat="server" Text='<%#Eval("ContentPost") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblextension" Style="padding-left: 10px;" runat="server" Text='<%#Eval("Extension").ToString() == ".mp4" ? "Play" : "Download" %>' OnClick="lblextension_Click" CommandArgument='<%#Eval("Id") %>'/>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Delete" ButtonType="Link" ShowDeleteButton="true"
                            ItemStyle-Width="150" />
                    </Columns>
                </asp:GridView>
            </div>
     </div>
</asp:Content>
