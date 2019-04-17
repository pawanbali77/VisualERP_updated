<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ReportManager.aspx.cs" Inherits="ReportManage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<script type="text/javascript">
    function hideAddNode() {
        $('#divAddNode').hide();
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="right_container_top TopMarg" id="Title">
        <h1>
            Report Manager
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div id="divRecordCount" runat="server" style="font: bold 14px Arial, Helvetica, sans-serif;
                        color: #555; padding: 0px 0px 0px 135px; float: right; width: 300px; margin-top: -19px;">
                        (Total Records: <span style="color: #EF705B">
                            <asp:Literal ID="ltrTotalRecord" runat="server" /></span>)</div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </h1>
        <div class="right_nav">
            <ul>
                <li>
                    <asp:UpdatePanel ID="updExport" runat="server">
                    <ContentTemplate>
                    <asp:LinkButton ID="lnkbtnExporttoExcel" runat="server" Text="Export to Excel" CssClass="DesignBtn"
                        OnClick="lnkbtnExporttoExcel_Click" />                        
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkbtnExporttoExcel" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </li>
                <li><a href="#">
                    <img src="images/zoom-in.png" /></a></li>
                <li><a href="#">
                    <img src="images/zoom-out.png" /></a></li>
            </ul>
        </div>
    </div>
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="RightReportDiv" id="divReport" runat="server" visible="true" style="overflow-y: scroll;
                height: 400px;">
                <asp:GridView ID="gridReport" runat="server" AlternatingRowStyle-CssClass="GrayBg" 
                    AutoGenerateColumns="false" OnSorting="gridReport_Sorting" OnRowCreated="gridReport_RowCreated"
                    CellSpacing="0" CellPadding="0" RowStyle-CssClass="field_row" GridLines="None"
                    HeaderStyle-CssClass="block_1_top" Style="overflow-y: auto; overflow-x: hidden;">
                    <Columns>
                        <asp:TemplateField SortExpression="NodeName" ItemStyle-CssClass="Node_td" HeaderStyle-CssClass="Node">
                            <HeaderTemplate>
                                <a class="block_arrow" href="#">Node</a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "NodeName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="AttributeName" HeaderStyle-CssClass="attributes"
                            ItemStyle-CssClass="attributes_td">
                            <HeaderTemplate>
                                <a class="block_arrow" href="#">Attribute</a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "AttributeName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="AttributeValue" ItemStyle-CssClass="value_td"
                            HeaderStyle-CssClass="value">
                            <HeaderTemplate>
                                <a class="block_arrow" href="#">Value</a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "AttributeValue")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="UnitName" ItemStyle-CssClass="unit_td" HeaderStyle-CssClass="unit">
                            <HeaderTemplate>
                                <a class="block_arrow" href="#">Unit</a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "UnitName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div>
                            &nbsp;</div>
                        <div class="msgSucess12">
                            <p>
                                No records found !
                            </p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
