<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="TrainingVideo.aspx.cs" Inherits="TrainingVideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <h1>Upload Training Material Videos</h1>
    </div>
    <div align="center">
        <asp:FileUpload ID="FileUpload1" runat="server" Height="45px" Width="348px" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" Height="31px" Width="91px" />
        <hr />
        <br />
        <asp:DataList ID="DataList1" runat="server" AutoGenerateColumns="false"
            RepeatColumns="4" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" RepeatDirection="Horizontal">
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <ItemTemplate>
                <u><b>
                    <%# Eval("Name") %></b></u>
                
                <hr />
                <br />
                <a class="player" style="height: 300px; width: 300px; display: block" href='<%# Eval("Id", "FileCS.ashx?Id={0}") %>'></a>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        </asp:DataList>

        <script src="FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            flowplayer("a.player", "FlowPlayer/flowplayer-3.2.16.swf", {
                plugins: {
                    pseudo: { url: "FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
                },
                clip: { provider: 'pseudo', autoPlay: false },
            });
        </script>
    </div>
</asp:Content>

