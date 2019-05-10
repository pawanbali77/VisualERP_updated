<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessManager.aspx.cs" Inherits="ProcessManager"
    MasterPageFile="~/MainMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/ModelPopupBOMUC.ascx" TagName="ModelPopupBOMUC" TagPrefix="uc1" %>
<%@ Register Src="UserControls/ModelPopupAttributeUC.ascx" TagName="ModelPopupAttributeUC" TagPrefix="uc2" %>
<%@ Register Src="UserControls/ModelPopupInputUC.ascx" TagName="ModelPopupInputUC" TagPrefix="uc3" %>
<%@ Register Src="UserControls/ModelPopupMchUC.ascx" TagName="ModelPopupMchUC" TagPrefix="uc4" %>
<%@ Register Src="UserControls/ModelPopupTFGUC.ascx" TagName="ModelPopupTFGUC" TagPrefix="uc5" %>
<%@ Register Src="UserControls/ProcessObject.ascx" TagName="ProcessObject" TagPrefix="uc6" %>
<%@ Register Src="UserControls/InventoryUC.ascx" TagName="InventoryUC" TagPrefix="uc7" %>
<%@ Register Src="UserControls/InventeryObject.ascx" TagName="InventeryObject" TagPrefix="uc8" %>
<%@ Register Src="UserControls/ModelPopupActivity.ascx" TagName="ModelPopupActivity" TagPrefix="uc9" %>
<%@ Register Src="UserControls/ArrowControl.ascx" TagName="ArrowControl" TagPrefix="uc11" %>
<%@ Register Src="UserControls/ImageControl.ascx" TagName="ImageControl" TagPrefix="uc10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" language="javascript">
        var queryString = new Array();
        $(function () {
            if (queryString.length == 0) {
                if (window.location.search.split('?').length > 1) {
                    var params = window.location.search.split('?')[1].split('&');
                    for (var i = 0; i < params.length; i++) {
                        var key = params[i].split('=')[0];
                        var value = decodeURIComponent(params[i].split('=')[1]);
                        queryString[key] = value;
                    }
                }
            }
            if (queryString["data"] != null) {
              
                var data = queryString["data"];
                console.log(data);
                //$("#ContentPlaceHolder1_MainDiv1").html(data);
            }

        });

        function test() {

            var contentHeight = $(window).height();
            var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 20; // + "px";

            //$("#ContentPlaceHolder1_MainDiv").css("height", newHeight + "px");
            var maxheight = $("input[id=ContentPlaceHolder1_hdnheight]").val(); //get max height of process from hidden field value
            $("#ContentPlaceHolder1_MainDiv").css("height", maxheight);

            var maxwidth = $("input[id=ContentPlaceHolder1_hdnWidth]").val();
            $("#ContentPlaceHolder1_MainDiv").css("width", maxwidth);

            $("#ContentPlaceHolder1_MainDivOuter").css("height", newHeight + "px");
            var remainCnt = $("#header").height() + $("#footer").height() + 50;
            var newHeight1 = contentHeight - remainCnt;
            $(".TreeView1_0").css("height", newHeight1 + "px");
            currFFZoom = 1;
            currIEZoom = 1;
            currOtherZoom = 1;
            //            $("input[id=ContentPlaceHolder1_hdnLastZoom]").val('');
            //            alert($("input[id=ContentPlaceHolder1_hdnLastZoom]").val());


            window.setTimeout(function () {
                $(".isa_info").fadeOut('slow', function () { $('.isa_info').remove() });
                $(".isa_success").fadeOut('slow', function () { $('.isa_success').remove() });
            }, 7000);

            //             if (msieversion()) {                
            ////                  alert("ie");
            ////                 jQuery('#ContentPlaceHolder1_MainDiv1').height('-=1');
            //             }
        }

        function loadProcessManager() {
            location.reload();
        }

        //        function minimizeSummaryTable() {

        //            $("#imgMinimize").click(function () {
        //                $("#ContentPlaceHolder1_divSummary").slideDown("slow");
        //            });
        //        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container">
                <%--<div class="right_top"></div>--%>
                <div class="right_container_top" id="Title">
                    <h1 style="font: 22px Arial, Helvetica, sans-serif !important;">Process View</h1>
                    <div id="divErrorMsg" runat="server" style="font: bold 12px Arial, Helvetica, sans-serif; color: #555; padding: 7px 0px 0px 10px; height: 30px; float: left; min-width: 450px; max-width: 500px;">
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <%-- <div style="float: left; margin-left: 638px; margin-top: 8px;">
                 <asp:Button ID="btnDesignView" runat="server" Text="Design View" class="BlackBtnLe" 
                         onclick="btnDesignView_Click" />
                 </div>--%>
                    <div class="right_nav">
                        <ul>
                            <li runat="server" id="liDesignViewBtn"><a href="Production.aspx?src=process" class="DesignBtn" style="font: bold 20px Arial; line-height: 44px !important;">Design View</a></li>
                            <li onclick="return ZoomIn();" id="liZoomIn" runat="server" style="cursor: pointer; margin-top: 3px;"><a title="Zoom-in" class="ZommIn"></a></li>
                            <li onclick="return ZoomOut();" id="liZoomOut" runat="server" style="cursor: pointer; margin-top: 3px;"><a title="Zoom-out" class="ZommOut"></a></li>
                            <li onclick="return Zoomorg();" id="li2" runat="server" style="height: 50px; cursor: pointer">
                                <a title="Zoom-reset" class="ZommSet"></a></li>
                            <li id="liSummary" runat="server" style="height: 44px; margin-top: 6px;"><a title="Summary functions"
                                class="SummaryIcon" href="SummaryTable.aspx"></a></li>
                            <li onclick="return CopyPageCode();" id="li" runat="server" style="cursor: pointer; margin-top: 3px;"><a title="Zoom-out" class="ZommOut"></a></li>
                        </ul>
                        <asp:HiddenField ID="hdnWidth" runat="server" />
                        <asp:HiddenField ID="hdnheight" runat="server" />
                        <asp:HiddenField ID="hdnLastZoom" runat="server" />
                    </div>
                </div>
                <div class="grey_strip">
                    <div class="filter_strip" id="Bpmn" style="display: none">
                        <ul id="nav">
                            <li class="bdr_none"><a href="#" class="col_grey">BPMN icon</a>
                                <ul style="z-index: 2;">
                                    <li id="liProcess" runat="server">
                                        <asp:LinkButton ID="lnkBtnProces" runat="server" OnClick="lnkBtnProces_Click"><span class="NaviMidIcon1"></span>Process</asp:LinkButton></li>
                                    <li><a href="#"><span class="NaviMidIcon2"></span>Customer Supplier</a></li>
                                    <li id="liInventory" runat="server">
                                        <asp:LinkButton ID="lnkBtnInventory" runat="server" OnClick="lnkBtnInventory_Click"><span class="NaviMidIcon3"></span>Inventory</asp:LinkButton></li>
                                    <li><a href="#"><span class="NaviMidIcon4"></span>External Shipment</a></li>
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
                        </ul>
                    </div>
                </div>
                <%--   <div class="maindiv" id="MainDiv" runat="server" style="position: relative; background-color: #fffff;
                        z-index: 50; height: 390px">
                    </div>--%>
                <%--  <div class="bottom_container" id="MainDiv" runat="server" style="position: relative;
                    background-color: #fffff; z-index: 50; zoom: 100%">
                </div>--%>
                <div id="MainDivOuter" class="bottom_containerZ1" runat="server" style="zoom: 100%">
                    <div id="MainDiv" class="bottom_containerZ" runat="server" style="zoom: 100%">
                    </div>
                </div>
                <%--<div id="divContent" style="zoom: 100%"> 
Six month Professional Training in Web Desinging from  Rajiv Nanda Design & Photography
     HTML, Dream weaver CS3(CSS, Div Tag, Table less  
     Sites), Flash CS3 with Action script, Anfy Java , 
     Flax, Front Page, VCD Cutter, Jquery, javascript, 
     Sound Forge, Xara 3D, Anim FX, Corel Draw, 
     Photoshop, Illustrator)        

</div>--%>
                <div class="SummryListRi" style="border: 1px solid #cccccc; display: block; opacity: 0.8;"
                    id="divSummary" runat="server" visible="false">
                    <div class="summry_table_th">
                        <%-- <a href="#" class="MinimizeMinusIcon" id="imgMinimize" onclick="return minimizeSummaryTable();"></a>--%>
                        <span id="">Summary Table</span>
                        <div id="Span1" style="font-size: 12px; color: #FF0000">
                            Critical Path Lead Time -
                           
                            <asp:Literal ID="ltrMaxCycleTime" runat="server" />
                        </div>
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
                <%--</div>--%>
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
            <%----End TFG--%>
            <%---Machine--%>
            <uc4:ModelPopupMchUC ID="ModelPopupMchUC1" runat="server" />
            <%--MachineEnd--%>
            <uc9:ModelPopupActivity ID="ModelPopupActivityUC9" runat="server" />
            <%--Activity--%>
            <%-- <uc21:ModelPopupSummaryTable ID="ModelPopupSummaryTableuc21" runat="server" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--   <asp:LinkButton ID="lnkBtnInventory12" runat="server"><span class="NaviMidIcon3"></span>Inventory</asp:LinkButton>--%>
    <%-- <uc8:InventeryObject ID="InventeryObject1" runat="server" />--%>
</asp:Content>
