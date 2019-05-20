<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ProcessIntrgrate.aspx.cs" Inherits="ProcessIntrgrate" %>

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
<%@ Register src="UserControls/Shipment.ascx" tagname="Shipment" tagprefix="uc9" %>
<%@ Register Src="UserControls/ModelPopupErrorReportUC.ascx" TagName="ModelPopupErrorReportUC" TagPrefix="uc12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var $ = jQuery.noConflict();
    </script>
     
    <script src="http://code.jquery.com/ui/1.8.23/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.23/themes/base/jquery-ui.css"
        type="text/css" media="all" />--%>
    <script type="text/javascript">
        function callready() {

            $(document).ready(function () {

                // var hv = $("#<%= hdnSupplier.ClientID %>").val();
                // var hv = $("#<%= hdnSupplier.ClientID %>").attr("value");
                // alert(hv);            
                // $("div").resizable();
                // $("div").draggable();
                $('div[id^=divSupplier], div[id^=divShipment], div[id^=divForcast], div[id^=divProduction], div[id^=divDSchedule], div[id^=divArrow] ').resizable();
                // $("#divSupplier1").draggable();
                $('div[id^=divSupplier], #divSupplier1, div[id^=divShipment], div[id^=divForcast], div[id^=divProduction], div[id^=divDSchedule], div[id^=divArrow] ').draggable();


                // $("#droppable").droppable({
                $("#ContentPlaceHolder1_MainDiv1").droppable({
                    drop: function (event, ui) {
                        //var pos = ui.draggable.position();
                        var id = ui.draggable.attr("id"); // getting draggble element id                     
                        var x = $("#" + id + "").position();

                        var childPos = $("#" + id + "").offset();
                        var parentPos = $("#ContentPlaceHolder1_MainDiv1").offset();
                        var childOffset = {
                            top: childPos.top - parentPos.top,
                            left: childPos.left - parentPos.left
                        }

                        //alert(x.top + ',' + x.left);
                        var xPos = childOffset.top;
                        var yPos = childOffset.left;
                        var hiddenval = id + "_" + xPos + "-" + yPos + ",";

                        alert(hiddenval);
                        alert($("input[id=ContentPlaceHolder1_hdnSupplier]").val());
                        var oldhdn = $("input[id=ContentPlaceHolder1_hdnSupplier]").val();
                        var arr = oldhdn.split(id + '_');
                        var arr1 = arr[1].split(',');
                        var z = arr1[0].split('-');
                        z[0] = xPos;
                        z[1] = yPos;
                        var modifiedhdn = arr[0] + id + "_" + z[0] + "-" + z[1] + "," + arr1[1];
                        var lastChar = modifiedhdn.substr(modifiedhdn.length - 1);
                        alert(lastChar);
                        if (lastChar != ",") {
                            modifiedhdn = arr[0] + id + "_" + z[0] + "-" + z[1] + "," + arr1[1] + ",";
                            alert(modifiedhdn);
                        }
                        alert(modifiedhdn);
                        $("input[id=ContentPlaceHolder1_hdnSupplier]").val(modifiedhdn);
                    }
                });
            });
        }
    </script>
    <style type="text/css">
        .Supplier
        {
            width: 100px;
            height: 100px;
            padding: 0.5em;
            background: #FF5555;
            color: #fff;
            position: absolute;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function test() {

            var contentHeight = $(window).height();
            var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 75; // + "px";
            //alert(newHeight);
            $("#ContentPlaceHolder1_MainDiv").css("height", newHeight + "px");
            var remainCnt = $("#header").height() + $("#footer").height() + 50;
            var newHeight1 = contentHeight - remainCnt;
            $(".TreeView1_0").css("height", newHeight1 + "px");
        }
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container">
                <%--<div class="right_top"></div>--%>
                <div class="right_container_top" id="Title">
                    <h1>
                        Process View</h1>
                         <%--<div style="float: left; margin-left: 599px; margin-top: 8px;">
                 <asp:Button ID="btnDesignView" runat="server" Text="Design View" class="BlackBtnLe" 
                         onclick="btnDesignView_Click" />
                 </div>--%>
                    <div class="right_nav">
                        <ul>
                         <li><a href="Production.aspx" class="DesignBtn">
                               Design View</a></li>
                            <li><a href="#">
                                <img src="images/zoom-in.png" /></a></li>
                            <li><a href="#">
                                <img src="images/zoom-out.png" /></a></li>
                        </ul>
                    </div>
                </div>

               <div class="grey_strip"> <div class="filter_strip" id="Bpmn">
                    <ul id="nav">
                        <li class="bdr_none"><a href="#" class="col_grey">BPMN icon</a>
                            <ul style="z-index: 2;">
                                <li id="liProcess" runat="server">
                                    <asp:LinkButton ID="lnkBtnProces" runat="server" OnClick="lnkBtnProces_Click"><span class="NaviMidIcon1"></span>Process</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="lnkBtnSupplier" runat="server" OnClick="lnkBtnSupplier_Click"><span class="NaviMidIcon2"></span>Customer Supplier</asp:LinkButton></li>
                                <li id="liInventory" runat="server">
                                    <asp:LinkButton ID="lnkBtnInventory" runat="server" OnClick="lnkBtnInventory_Click"><span class="NaviMidIcon3"></span>Inventory</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="lnkBtnShipment" runat="server" ><span class="NaviMidIcon4"></span>External Shipment</asp:LinkButton></li>
                                <li><a href="#"><span class="NaviMidIcon5"></span>Push</a></li>
                                <li><a href="#"><span class="NaviMidIcon6"></span>Shipment Arrow</a></li>
                                <li><a href="#"><span class="NaviMidIcon7"></span>Go See Production</a></li>
                                <li><a href="#"><span class="NaviMidIcon8"></span>Electronic Information</a></li>
                                <li><a href="#"><span class="NaviMidIcon9"></span>Production Control</a></li>
                                <li><a href="#"><span class="NaviMidIcon10"></span>Data Table</a></li>
                                <li><a href="#"><span class="NaviMidIcon11"></span>Timeline Segment</a></li>
                                <li><a href="#"><span class="NaviMidIcon12"></span>Timeline total</a></li>
                                <li><a href="#"><span class="NaviMidIcon13"></span>Supermarket</a></li>
                                <li><a href="#"><span class="NaviMidIcon14"></span>Safety Stock</a></li>
                                <li><a href="#"><span class="NaviMidIcon15"></span>Signal Kanban</a></li>
                                <li><a href="#"><span class="NaviMidIcon16"></span>Withdrawal kanban</a></li>
                                <li><a href="#"><span class="NaviMidIcon17"></span>Withdrawal Batch</a></li>
                                <li><a href="#"><span class="NaviMidIcon18"></span>Production Kanban</a></li>
                                <li><a href="#"><span class="NaviMidIcon19"></span>Batch Kanban</a></li>
                                <li><a href="#"><span class="NaviMidIcon20"></span>Kanban Post</a></li>
                                <li><a href="#"><span class="NaviMidIcon21"></span>FIFO Lane</a></li>
                                <li><a href="#"><span class="NaviMidIcon22"></span>Kaizen Burst</a></li>
                                <li><a href="#"><span class="NaviMidIcon23"></span>Pull Arrow 1</a></li>
                                <li><a href="#"><span class="NaviMidIcon24"></span>Pull Arrow 2</a></li>
                                <li><a href="#"><span class="NaviMidIcon25"></span>Pull Arrow 3</a></li>
                                <li><a href="#"><span class="NaviMidIcon26"></span>Physical Pull</a></li>
                                <li><a href="#"><span class="NaviMidIcon27"></span>Sequenced Pull Ball</a></li>
                                <li class="last"><a href="#"><span class="NaviMidIcon28"></span>Load Leveling</a></li>
                            </ul>
                        </li>
                        <asp:HiddenField ID="hdnSupplier" runat="server" />
                    </ul>
                </div></div>
                <div class="bottom_container">
                    <div class="Clear">
                    </div>
                    <div class="" id="MainDiv1" runat="server" style="position: relative; background-color: #fffff;
                        z-index: 50; height: 390px; margin-top: 20px">
                    </div>
                    <div class="SummryListRi" style="border: 1px solid #cccccc;" id="divSummary" runat="server"
                        visible="false">
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
                                <RowStyle CssClass="field_row" />
                                <%--    <HeaderStyle CssClass="block_1_top" />--%>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
                <div class="Clear">
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
            <uc4:ModelPopupMchUC ID="ModelPopupMchUC1" runat="server" />
            <%--MachineEnd--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
