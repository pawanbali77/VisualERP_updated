<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterPriseManager.aspx.cs"
    Inherits="EnterPriseManager" MasterPageFile="~/EnterPriseMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/ModelPopupBOMUC.ascx" TagName="ModelPopupBOMUC" TagPrefix="uc1" %>
<%@ Register Src="UserControls/ModelPopupAttributeUC.ascx" TagName="ModelPopupAttributeUC"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/ModelPopupInputUC.ascx" TagName="ModelPopupInputUC"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/ModelPopupMchUC.ascx" TagName="ModelPopupMchUC" TagPrefix="uc4" %>
<%@ Register Src="UserControls/ModelPopupTFGUC.ascx" TagName="ModelPopupTFGUC" TagPrefix="uc5" %>
<%@ Register Src="UserControls/ProcessObject.ascx" TagName="ProcessObject" TagPrefix="uc6" %>
<%@ Register Src="UserControls/InventoryUC.ascx" TagName="InventoryUC" TagPrefix="uc7" %>
<%@ Register Src="UserControls/InventeryObject.ascx" TagName="InventeryObject" TagPrefix="uc8" %>
<%@ Register Src="UserControls/AerrowUpDown.ascx" TagName="AerrowUpDown" TagPrefix="uc9" %>
<%@ Register Src="UserControls/AerrowDownUC.ascx" TagName="AerrowDownUC" TagPrefix="uc10" %>
<%@ Register Src="UserControls/AerrowUpUc.ascx" TagName="AerrowUpUc" TagPrefix="uc11" %>
<%@ Register Src="UserControls/HandOffUC.ascx" TagName="HandOffUC" TagPrefix="uc12" %>
<%@ Register Src="UserControls/ModelPopupActivity.ascx" TagName="ModelPopupActivity" TagPrefix="ucA" %>
<%@ Register Src="UserControls/ModelPopupErrorReportUC.ascx" TagName="ModelPopupErrorReportUC" TagPrefix="uc12" %>
<%@ Register Src="UserControls/ImageControl.ascx" TagName="ImageControl" TagPrefix="uc13" %>
<%@ Register Src="UserControls/ArrowControl.ascx" TagName="ArrowControl" TagPrefix="uc14" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function test() {
            var contentHeight = $(window).height();
            var header = $("#header").height();
            var footer = $("#footer").height();
            var title = $("#Title").height();
            var bpmn = $("#Bpmn").height();

            var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 20// + "px";
            // $("#ContentPlaceHolder1_MainDiv").css("height", newHeight + "px");

            var maxheight = $("input[id=ContentPlaceHolder1_hdnheight]").val(); //get max height of process from hidden field value
            $("#ContentPlaceHolder1_MainDiv").css("height", maxheight + "px");
            var maxwidth = $("input[id=ContentPlaceHolder1_hdnWidth]").val();
            $("#ContentPlaceHolder1_MainDiv").css("width", maxwidth + "px");
            $("#ContentPlaceHolder1_MainDivOuter").css("height", newHeight + "px");
            // alert($("#ContentPlaceHolder1_MainDiv").height());
            var remainCnt = $("#header").height() + $("#footer").height() + 50;
            var newHeight1 = contentHeight - remainCnt;
            $(".TreeView1_000").css("height", newHeight1 + "px");
            $("#ContentPlaceHolder1_MainDiv").css("overflow", "hidden");
            currFFZoom = 1;
            currIEZoom = 100;
            currOtherZoom = 1;
            //$("input[id=ContentPlaceHolder1_hdnLastZoomE]").val('');            

            window.setTimeout(function () {
                $(".isa_info").fadeOut('slow', function () { $('.isa_info').remove() });
                $(".isa_success").fadeOut('slow', function () { $('.isa_success').remove() });
            }, 6000);

            $(window).resize(function () {
                var contentHeight = $(window).height();
                var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 20 // + "px"; -75
                // $("#ContentPlaceHolder1_MainDiv").css("height", newHeight + "px");
                // $("#ContentPlaceHolder1_MainDivOuter").css("height", newHeight + "px");
                var contentHeight = $(window).height();
                var remainCnt = $("#header").height() + $("#footer").height();
                var newHeight1 = contentHeight - remainCnt;
                $(".TreeView1_000").css("height", newHeight1 + "px");
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container">
                <div class="right_container_top TopMarg" id="Title">
                    <h1>Enterprise View</h1>
                    <div id="divErrorMsg" runat="server" style="font: bold 12px Arial, Helvetica, sans-serif; color: #555; padding: 7px 0px 0px 10px; height: 30px; float: left; min-width: 450px; max-width: 500px;">
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="right_nav">
                        <ul>
                            <li id="liarrowDown" runat="server" style="height: 50px;">
                                <asp:LinkButton ID="lnkbtnDown" runat="server" CssClass="ArrowDown" OnClick="lnkbtnDown_Click" />
                            </li>
                            <li id="liarrowUp" runat="server" style="height: 50px;">
                                <asp:LinkButton ID="lnkbtnUp" runat="server" CssClass="ArrowUp" OnClick="lnkbtnUp_Click" />
                            </li>
                            <%-- <li onclick="return ZoomIn();" id="liZoomIn" runat="server" style="cursor: crosshair">
                                <a title="Zoom-in">
                                    <img src="images/zoom-in.png" alt="ZoomIn" /></a></li>
                            <li onclick="return ZoomOut();" id="liZoomOut" runat="server" style="cursor: crosshair">
                                <a title="Zoom-out">
                                    <img src="images/zoom-out.png" alt="ZoomOut" /></a></li>
                            <li onclick="return Zoomorg();" id="li2" runat="server" style="height: 50px; cursor: pointer">
                                <a title="Zoom-reset">
                                    <img src="images/zoom-org.png" alt="ZoomOrg" style="margin-top: 3px; height: 36px" /></a></li>--%>
                            <li onclick="return ZoomIn();" id="liZoomIn" runat="server" style="cursor: pointer; margin-top: 3px;">
                                <a title="Zoom-in" class="ZommIn"></a></li>
                            <li onclick="return ZoomOut();" id="liZoomOut" runat="server" style="cursor: pointer; margin-top: 3px;">
                                <a title="Zoom-out" class="ZommOut"></a></li>
                            <li onclick="return Zoomorg();" id="li2" runat="server" style="height: 50px; cursor: pointer">
                                <a title="Zoom-reset" class="ZommSet"></a></li>
                        </ul>
                        <asp:HiddenField ID="hdnWidth" runat="server" />
                        <asp:HiddenField ID="hdnheight" runat="server" />
                        <asp:HiddenField ID="hdnLastZoomE" runat="server" />
                    </div>
                </div>
                <div class="grey_strip">
                    <div class="filter_strip Top2Marg" id="Bpmn">
                        <asp:Label ID="lblSystemName" runat="server" Style="font-size: 18px; padding-left: 12px;"></asp:Label>
                    </div>
                </div>
                <%-- <div class="bottom_container TopMargin115" id="MainDivRole">--%>
                <%--<div class="Clear">
                    </div>--%>
                <%--  <div class="block_2 margin_top1" id="MainDiv" runat="server">
                        <asp:Table Width="100%" border="0" CellSpacing="0" CellPadding="0" ID="tb1" runat="server"
                            BackColor="Aqua">
                            <asp:TableRow ID="trfirst" runat="server">
                            </asp:TableRow>
                        </asp:Table>
                    </div>--%>
                <div class="bottom_containerZ1" id="MainDivOuter" runat="server" style="zoom: 100%">
                    <div class="bottom_containerZ" id="MainDiv" runat="server" style="zoom: 100%">
                    </div>
                </div>
                <div class="SummryListRi" style="border: 1px solid #cccccc; display: block; opacity: 0.8;"
                    id="divSummary" runat="server">
                    <div class="summry_table_th">
                        <span id="">Summary Table</span>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Style="height: 200px" ScrollBars="Vertical">
                        <asp:GridView ID="gridProcessSummary" runat="server" AlternatingRowStyle-CssClass="field_row bg_white"
                            CssClass="summry_table" AutoGenerateColumns="false" OnSorting="gridProcessSummary_Sorting"
                            AllowSorting="true" OnRowCreated="gridProcessSummary_RowCreated" CellSpacing="0"
                            CellPadding="0" RowStyle-CssClass="field_row" GridLines="None" HeaderStyle-CssClass="block_1_top"
                            Style="overflow-y: auto; overflow-x: hidden; height: 200px; width: 300px;">
                            <Columns>
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
                                        <%#DataBinder.Eval(Container.DataItem, "AttributeValueResult")%>
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
                            <RowStyle CssClass="field_row" />
                            <%--    <HeaderStyle CssClass="block_1_top" />--%>
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <%-- </div>--%>
                <div class="Clear">
                    <%-- <uc10:AerrowDownUC ID="AerrowDownUC1" runat="server" />
                    <uc11:AerrowUpUc ID="AerrowUpUc1" runat="server" />--%>
                </div>
            </div>
            <%--<uc6:ProcessObject ID="ProcessObject1" runat="server" />--%>
            <uc7:InventoryUC ID="InventoryUC1" runat="server" />
            <%--Attribute--%>
            <uc2:ModelPopupAttributeUC ID="ModelPopupAttributeUC1" runat="server" />
            <%--Attribute end--%>
            <%--Input Attribute--%>
            <uc3:ModelPopupInputUC ID="ModelPopupInputUC1" runat="server" />
            <%--Input Attribute--%>
            <%--BOM Manger---%>
            <uc1:ModelPopupBOMUC ID="ModelPopupBOMUC1" runat="server" />
            <%--End BOM Manger--%>
            <%----TFG----%>
            <uc5:ModelPopupTFGUC ID="ModelPopupTFGUC1" runat="server" />
            <uc12:ModelPopupErrorReportUC ID="ModelPopupErrorReportUC1" runat="server" />
            <%----End TFG--%>
            <%---Machine--%>
            <uc12:HandOffUC ID="HandOffUC1" runat="server" />
            <uc4:ModelPopupMchUC ID="ModelPopupMchUC1" runat="server" />
            <ucA:ModelPopupActivity ID="ModelPopupActivityucA" runat="server" />
            <%--Activity--%>
            <uc9:AerrowUpDown ID="AerrowUpDown1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Uppnl1"  DisplayAfter="10">
                <ProgressTemplate>
                    <div class="AjaxLoaderOuter123">
                        <div class="AjaxLoaderInner123">
                            <img src='<%= ResolveUrl("~/images/ajax-loading11.gif") %>' alt="Wait..." style="background-color: Transparent;" />
                        </div>
                    </div>
              </ProgressTemplate>
            </asp:UpdateProgress>--%>
    <script type="text/javascript" src="http://code.jquery.com/jquery-migrate-1.2.1.js"></script>
</asp:Content>
