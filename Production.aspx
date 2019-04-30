<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Production.aspx.cs" Inherits="Production"
    MasterPageFile="~/MainMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/ModelPopupBOMUC.ascx" TagName="ModelPopupBOMUC" TagPrefix="uc1" %>
<%@ Register Src="UserControls/ModelPopupAttributeUC.ascx" TagName="ModelPopupAttributeUC"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/ModelPopupInputUC.ascx" TagName="ModelPopupInputUC"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/ModelPopupMchUC.ascx" TagName="ModelPopupMchUC" TagPrefix="uc4" %>
<%@ Register Src="UserControls/ModelPopupTFGUC.ascx" TagName="ModelPopupTFGUC" TagPrefix="uc5" %>
<%@ Register Src="UserControls/ProcessObject.ascx" TagName="ProcessObject" TagPrefix="uc6" %>
<%--<%@ Register Src="UserControls/InventoryUC.ascx" TagName="InventoryUC" TagPrefix="uc7" %>--%>
<%@ Register Src="UserControls/InventeryObject.ascx" TagName="InventeryObject" TagPrefix="uc8" %>
<%--<%@ Register Src="UserControls/ModelPopupActivity.ascx" TagName="ModelPopupActivity"
    TagPrefix="uc9" %>--%>
<%@ Register Src="UserControls/ImageControl.ascx" TagName="ImageControl" TagPrefix="uc15" %>
<%@ Register Src="UserControls/ArrowControl.ascx" TagName="ArrowControl" TagPrefix="ucArrow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<script type="text/javascript" language="javascript">
        function test() {
            window.setTimeout(function () {
                $(".isa_info").fadeOut('slow', function () { $('.isa_info').remove() });
                $(".isa_success").fadeOut('slow', function () { $('.isa_success').remove() });
            }, 7000);

        }
<<<<<<< HEAD
    </script>--%>

 

    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script type='text/javascript'  src="//code.jquery.com/jquery-1.9.1.js"></script>
    <script type='text/javascript'  src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>

    
  <%--    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
   
    <script src="http://code.jquery.com/ui/1.8.23/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.23/themes/base/jquery-ui.css"
        type="text/css" media="all" /> --%>
    
    <link rel="stylesheet" href="/resources/demos/style.css">
=======
    </script>--%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.9.1.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">
>>>>>>> f0aeb939c7bffcbbd47a69dcf2294111dbd5e9eb
    <script language="javascript" type="text/javascript">
        $(function showpopup() {
            $("#Panel1").dialog();
            $("#Panel1").show();

        });
    </script>
    <script language="javascript" type="text/javascript">

        function unloadPopupBox() {	// TO Unload the Popupbox
            // document.getElementById('popup_box').style.display = "block";
            $('#popup_box').fadeOut("slow");
            $("#container").css({ // this is just for style		
                "opacity": "1"
            });
        }

    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.8.23/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.23/themes/base/jquery-ui.css"
        type="text/css" media="all" />
    <%-- script to resize div on scrolling --%>
    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.1/themes/base/jquery-ui.css" />
    <%-- <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" /> --%>
    <script src="http://code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.1/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
        function callready() {
            $(document).ready(function () {

                var contentHeight = $(window).height();
                var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 52; // + "px";
                //alert(newHeight);
                $("#ContentPlaceHolder1_MainDiv1").css("height", newHeight + "px");
                var remainCnt = $("#header").height() + $("#footer").height() + 50;
                var newHeight1 = contentHeight - remainCnt;
                $(".TreeView1_0").css("height", newHeight1 + "px");

                $('div[name ^= aivCArrow]').resizable({
                    stop: function (event, ui) {
                        var resized = $(this);
                        resized.queue(function () {

                            var width = ui.size.width;
                            var height = ui.size.height;

                            var pane = $(event.target);
                            var pos = pane.position();
                            var nameRe = pane.attr("name");
                            var nameResize = nameRe.replace('aiv', 'div');
                            var id = pane.attr("id");
                            var name = nameResize.replace('ContentPlaceHolder1_', '');
                            var location = $("#ContentPlaceHolder1_" + name + "");
                            var childPos = $("#ContentPlaceHolder1_" + name + "").position();

                            var styleTop = location.css('top');
                            var getTop = styleTop.split("px");
                            var top = getTop[0];
                            var styleleft = location.css('left');
                            var getleft = styleleft.split("px");
                            var left = getleft[0];
                            // alert(width + ',' + height + ',' + name + ',' + top + ',' + left + ',' + id);

                            var hiddenval = name + "@" + top + "~" + left + "[" + width + "*" + height + "]" + ",";
                            var oldhdn = $("input[id=ContentPlaceHolder1_hdnSupplier]").val();
                            str = oldhdn;
                            var modified = str
                            var modified = (str.substring(0, str.indexOf(name)) + name + '@' + top + "~" + left + "[" + width + "*" + height + "]" + "," + str.substring(str.indexOf(name)).substring(str.substring(str.indexOf(name)).indexOf(","))).replace(",,", ",");
                            $("input[id=ContentPlaceHolder1_hdnSupplier]").val(modified);

                            $(this).dequeue();
                        });
                    }
                });


                // $('div[name^=divSupplier], div[name^=divShipment], div[id^=divForcast], div[id^=divProduction], div[id^=divDSchedule], div[id^=divArrow],div[name^=divElectronic],div[name^=divDataTable],div[name^=divTimelineSegment],div[name^=divTimelinetotal],div[name^=divSupermarket],div[name^=divSafetyStock],div[name^=divSignalKanban],div[name^=divWithdrawalkanban],div[name^=divWithdrawalBatch],div[name^=divProductionKanban],div[name^=divBatchKanban],div[name^=divKanbanPost],div[name^=divFIFOLane],div[name^=divKaizenBurst],div[name^=divPullArrow1],div[name^=divPullArrow2],div[name^=divPullArrow3],div[name^=divPhysicalPull],div[name^=divSequencedPullBall],div[name^=divLoadLeveling]').resizable();
                //  $("#ContentPlaceHolder1_divProcess").draggable();
                $('div[name^=divSupplier], div[id^=ContentPlaceHolder1_divProcess],div[id^=ContentPlaceHolder1_divParallelProcess], div[name^=divShipment], div[name^=divForcast], div[name^=divValueStream], div[name^=divDSchedule], div[name^=divArrow],div[name^=divElectronic],div[name^=divDataTable],div[name^=divTimelineSegment],div[name^=divTimelinetotal],div[name^=divSupermarket],div[name^=divSafetyStock],div[name^=divSignalKanban],div[name^=divWithdrawalkanban],div[name^=divWithdrawalBatch],div[name^=divProductionKanban],div[name^=divBatchKanban],div[name^=divKanbanPost],div[name^=divFIFOLane],div[name^=divKaizenBurst],div[name^=divPullArrow1],div[name^=divPullArrow2],div[name^=divPullArrow3],div[name^=divPhysicalPull],div[name^=divSequencedPullBall],div[name^=divLoadLeveling],div[name^=divCArrow] ').draggable();

                $('div[name^=divSupplier], div[id^=ContentPlaceHolder1_divProcess],div[id^=ContentPlaceHolder1_divParallelProcess], div[name^=divShipment], div[name^=divForcast], div[name^=divValueStream], div[name^=divDSchedule], div[name^=divArrow],div[name^=divElectronic],div[name^=divDataTable],div[name^=divTimelineSegment],div[name^=divTimelinetotal],div[name^=divSupermarket],div[name^=divSafetyStock],div[name^=divSignalKanban],div[name^=divWithdrawalkanban],div[name^=divWithdrawalBatch],div[name^=divProductionKanban],div[name^=divBatchKanban],div[name^=divKanbanPost],div[name^=divFIFOLane],div[name^=divKaizenBurst],div[name^=divPullArrow1],div[name^=divPullArrow2],div[name^=divPullArrow3],div[name^=divPhysicalPull],div[name^=divSequencedPullBall],div[name^=divLoadLeveling],div[name^=divCArrow] ').draggable({ revert: "invalid" });

                $('div[name^=aivCArrow]').resizable();
                // $("#droppable").droppable({
                $("#ContentPlaceHolder1_MainDiv1").droppable({
                    drop: function (event, ui) {
                        //var pos = ui.draggable.position();
                        // var id = ui.draggable.attr("id"); // getting draggble element id  
                        var id = ui.draggable.attr("id"); // getting draggble element id                       
                        var nameContent = ui.draggable.attr("name");
                        var name = nameContent.replace('ContentPlaceHolder1_', '');
                        //  if ($(name).text().match('divCArrow')) {
                        if (name.indexOf('divCArrow') > -1) {
                            var x = $("#" + id + "").position();

                            var childPos = $("#" + id + "").offset();

                            var styleTop = ui.draggable[0].style.top;
                            var styleLeft = ui.draggable[0].style.left;


                            var stylewidth = ui.draggable[0].style.width; ///////////
                            var styleheight = ui.draggable[0].style.height; //////////



                            var parentId = "ContentPlaceHolder1_" + name + "";
                            var newname = name.replace('d', 'a');
                            var childid = $("div[name='" + newname + "']").attr("id");
                            //                        alert(childid);
                            var location = $("#" + childid + "");
                            var stylewidth = location.css('width');
                            var styleheight = location.css('height');
                            var getwidth = stylewidth.split("px");
                            var getheight = styleheight.split("px");
                            var width = getwidth[0];
                            var height = getheight[0];
                            //  alert(width + ',' + height);

                            var ArrTop = styleTop.split("px");
                            var ArrLeft = styleLeft.split("px");


                            var Arrwidth = stylewidth.split("px"); ////////////
                            var Arrheight = styleheight.split("px"); ///////


                            var top = ArrTop[0];
                            var left = ArrLeft[0];

                            //                        var width = Arrwidth[0]; //////
                            //                        var height = Arrheight[0]; ////


                            //alert(top + "," + left);
                            //alert("(x,y): (" + (childPos.left) + "," + (childPos.top + ")");
                            //var offset = $("#" + id + "").offset();
                            //var posX = $("#" + id + "").offset().top - $("#ContentPlaceHolder1_MainDiv1").offset().top;
                            //var posY = $("#" + id + "").offset().left - $("#ContentPlaceHolder1_MainDiv1").offset().left;
                            //var posZ = $(window).scrollTop();

                            var parentPos = $("#ContentPlaceHolder1_MainDiv1").offset();
                            var childOffset = {
                                top: childPos.top - parentPos.top,
                                left: childPos.left - parentPos.left
                            }

                            //alert(x.top + ',' + x.left);
                            var xPos = childPos.top;
                            var yPos = childPos.left;

                        }
                        else {

                            var x = $("#" + id + "").position();

                            var childPos = $("#" + id + "").offset();

                            var styleTop = ui.draggable[0].style.top;
                            var styleLeft = ui.draggable[0].style.left;


                            var stylewidth = ui.draggable[0].style.width; ///////////
                            var styleheight = ui.draggable[0].style.height; //////////




                            var ArrTop = styleTop.split("px");
                            var ArrLeft = styleLeft.split("px");


                            var Arrwidth = stylewidth.split("px"); ////////////
                            var Arrheight = styleheight.split("px"); ///////


                            var top = ArrTop[0];
                            var left = ArrLeft[0];

                            var width = Arrwidth[0]; //////
                            var height = Arrheight[0]; ////


                            //alert(top + "," + left);
                            //alert("(x,y): (" + (childPos.left) + "," + (childPos.top + ")");
                            //var offset = $("#" + id + "").offset();
                            //var posX = $("#" + id + "").offset().top - $("#ContentPlaceHolder1_MainDiv1").offset().top;
                            //var posY = $("#" + id + "").offset().left - $("#ContentPlaceHolder1_MainDiv1").offset().left;
                            //var posZ = $(window).scrollTop();

                            var parentPos = $("#ContentPlaceHolder1_MainDiv1").offset();
                            var childOffset = {
                                top: childPos.top - parentPos.top,
                                left: childPos.left - parentPos.left
                            }

                            //alert(x.top + ',' + x.left);
                            var xPos = childPos.top;
                            var yPos = childPos.left;


                        }


                        //  var hiddenval = name + "@" + top + "~" + left + ","; 
                        var hiddenval = name + "@" + top + "~" + left + "[" + width + "*" + height + "]" + ","; /////////////
                        // alert(top + ',' + left);
                        // alert($("input[id=ContentPlaceHolder1_hdnSupplier]").val());
                        var oldhdn = $("input[id=ContentPlaceHolder1_hdnSupplier]").val();

                        str = oldhdn;
                        var modified = str
                        // var modified = (str.substring(0, str.indexOf(name)) + name + '@' + xPos + "~" + yPos + "," + str.substring(str.indexOf(name)).substring(str.indexOf(",") + 1)).replace(",,", ",");
                        var modified = (str.substring(0, str.indexOf(name)) + name + '@' + top + "~" + left + "[" + width + "*" + height + "]" + "," + str.substring(str.indexOf(name)).substring(str.substring(str.indexOf(name)).indexOf(","))).replace(",,", ",");
                        // alert('Prefix ' + str.substring(0, str.indexOf(name)));
                        // alert('Postfix ' + str.substring(str.substring(str.indexOf(name)).substring(str.indexOf(","))).replace(",,", ","));
                        //alert('Postfix ' + str.substring(str.indexOf(name)).substring(str.substring(str.indexOf(name)).indexOf(",")));

                        // alert('modified value' + modified);


                        $("input[id=ContentPlaceHolder1_hdnSupplier]").val(modified);
                    }
                });
            });

            window.setTimeout(function () {
                $(".isa_info").fadeOut('slow', function () { $('.isa_info').remove() });
                $(".isa_success").fadeOut('slow', function () { $('.isa_success').remove() });
            }, 7000);
        }
<<<<<<< HEAD

            .chkk input {
                float: left;
                margin-left: 5px;
                margin-top: 4px;
            }

            .chkk label {
                float: left !important;
                margin-top: -17px;
                margin-left: -16px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container">
                <%--<div class="right_top"></div>--%>
                <div class="right_container_top" id="Title">
                    <h1>Design View</h1>
                    <div id="divErrorMsg" runat="server" style="font: bold 12px Arial, Helvetica, sans-serif; color: #555; padding: 7px 0px 0px 10px; height: 30px; float: left; min-width: 450px; max-width: 500px;">
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="right_nav">
                        <ul>
                             <li onclick="return ZoomIn();" id="liZoomIn" runat="server" style="cursor: pointer;
                                margin-top: 3px;"><a title="Zoom-in" class="ZommIn"></a></li>
                            <li onclick="return ZoomOut();" id="liZoomOut" runat="server" style="cursor: pointer;
                                margin-top: 3px;"><a title="Zoom-out" class="ZommOut"></a></li>
                            <li onclick="return Zoomorg();" id="li5" runat="server" style="height: 50px; cursor: pointer">
                                <a title="Zoom-reset" class="ZommSet"></a></li>

                            <li runat="server" id="liSavedesign">
                                <asp:LinkButton ID="lnkbtnSavedesign" runat="server" Text="Save Design" CssClass="DesignBtn"
                                    OnClick="lnkbtnSavedesign_Click" /></li>
=======
    </script>

    <style type="text/css">
        .maindiv {
            min-height: 390px !important;
            min-width: 80% !important;
        }
    </style>
    <style type="text/css">
        .chkk {
            border: solid 1px gray;
        }

            .chkk input {
                float: left;
                margin-left: 5px;
                margin-top: 4px;
            }

            .chkk label {
                float: left !important;
                margin-top: -17px;
                margin-left: -16px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container">
                <%--<div class="right_top"></div>--%>
                <div class="right_container_top" id="Title">
                    <h1 style="font: 20px Arial, Helvetica, sans-serif;">Design View</h1>
                    <div id="divErrorMsg" runat="server" style="font: bold 12px Arial, Helvetica, sans-serif; color: #555; padding: 7px 0px 0px 10px; height: 30px; float: left; min-width: 450px; max-width: 500px;">
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="right_nav">
                        <ul>
                            <li runat="server" id="liSavedesign">
                                <asp:LinkButton ID="lnkbtnSavedesign" runat="server" Text="Save Design"
                                    Style="font: bold 20px Arial; line-height: 44px !important;" CssClass="DesignBtn"
                                    OnClick="lnkbtnSavedesign_Click" /></li>
>>>>>>> f0aeb939c7bffcbbd47a69dcf2294111dbd5e9eb
                            <%-- <li style="display:none"><a href="#">
                                <img src="images/zoom-in.png" /></a></li>
                            <li style="display:none"><a href="#">
                                <img src="images/zoom-out.png" /></a></li>--%>

                            <%--                                  <li onclick="return callready();" id="liZoomIn" runat="server" style="cursor: pointer; margin-top: 3px;"><a title="Zoom-in" class="ZommIn"></a></li>
                            <li onclick="return callready();" id="liZoomOut" runat="server" style="cursor: pointer; margin-top: 3px;"><a title="Zoom-out" class="ZommOut"></a></li>
                            <li onclick="return callready();" id="li5" runat="server" style="height: 50px; cursor: pointer">
                                <a title="Zoom-reset" class="ZommSet"></a></li>--%>
                        </ul>
                    </div>
                </div>
                <div class="grey_strip" id="controls" runat="server">
                    <div class="filter_strip" id="Bpmn" runat="server">
                        <ul id="nav">
                            <li class="bdr_none"><a href="#" class="col_grey" style="font: 20px Arial, Helvetica, sans-serif;line-height: 33px;">BPMN icon</a>
                                <ul style="z-index: 2; overflow-y: scroll!important;">
                                    <li id="liProcess" runat="server">
                                        <asp:LinkButton ID="lnkBtnProces" runat="server" OnClick="lnkBtnProces_Click"><span class="NaviMidIcon1"></span>Process</asp:LinkButton></li>
                                    <li id="li1" runat="server">
                                        <asp:LinkButton ID="lnkBtnparallelProcess" runat="server" OnClick="lnkBtnparallelProcess_Click"><span class="NaviMidIcon1"></span>Process(Parallel)</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnSupplier" runat="server" OnClick="lnkBtnSupplier_Click"><span class="NaviMidIcon2"></span>Customer Supplier</asp:LinkButton></li>
                                    <li id="liInventory" runat="server">
                                        <asp:LinkButton ID="lnkBtnInventory" runat="server" OnClick="lnkBtnInventory_Click"><span class="NaviMidIcon3"></span>Inventory</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnShipment" runat="server" OnClick="lnkBtnShipment_Click"><span class="NaviMidIcon4"></span>External Shipment</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnForcast" runat="server" OnClick="lnkBtnForcast_Click"><span class="NaviMidIcon5"></span>Market Forcast</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnArrow" runat="server" OnClick="lnkBtnArrow_Click"><span class="NaviMidIcon6"></span>Annual Production Plan</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnDSchedule" runat="server" OnClick="lnkBtnDSchedule_Click"><span class="NaviMidIcon7"></span>Delivery Schedule</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnProduction" runat="server" OnClick="lnkBtnProduction_Click"><span class="NaviMidIcon8"></span>Production Control</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnElectronic" runat="server" OnClick="lnkBtnElectronic_Click"><span class="NaviMidIcon9"></span>Electronic Information</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnDataTable" runat="server" OnClick="lnkBtnDataTable_Click"><span class="NaviMidIcon10"></span>Data Table</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnTimelineSegment" runat="server" OnClick="lnkBtnTimelineSegment_Click"><span class="NaviMidIcon11"></span>Timeline Segment</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnTimelinetotal" runat="server" OnClick="lnkBtnTimelinetotal_Click"><span class="NaviMidIcon12"></span>Timeline total</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnSupermarket" runat="server" OnClick="lnkBtnSupermarket_Click"><span class="NaviMidIcon13"></span>Supermarket</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnSafetyStock" runat="server" OnClick="lnkbtnSafetyStock_Click"><span class="NaviMidIcon14"></span>Safety Stock</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnSignalKanban" runat="server" OnClick="lnkbtnSignalKanban_Click"><span class="NaviMidIcon15"></span>Signal Kanban</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnWithdrawalkanban" runat="server" OnClick="lnkbtnWithdrawalkanban_Click"><span class="NaviMidIcon16"></span>Withdrawal Kanban</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnWithdrawalBatch" runat="server" OnClick="lnkbtnWithdrawalBatch_Click"><span class="NaviMidIcon17"></span>Withdrawal Batch</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnProductionKanban" runat="server" OnClick="lnkbtnProductionKanban_Click"><span class="NaviMidIcon18"></span>Production Kanban</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnBatchKanban" runat="server" OnClick="lnkbtnBatchKanban_Click"><span class="NaviMidIcon19"></span>Batch Kanban</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnKanbanPost" runat="server" OnClick="lnkbtnKanbanPost_Click"><span class="NaviMidIcon20"></span>Kanban Post</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnFIFOLane" runat="server" OnClick="lnkbtnFIFOLane_Click"><span class="NaviMidIcon21"></span>FIFO Lane</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnKaizenBurst" runat="server" OnClick="lnkbtnKaizenBurst_Click"><span class="NaviMidIcon22"></span>Kaizen Burst</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnPullArrow1" runat="server" OnClick="lnkbtnPullArrow1_Click"><span class="NaviMidIcon23"></span>Pull Arrow 1</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnPullArrow2" runat="server" OnClick="lnkbtnPullArrow2_Click"><span class="NaviMidIcon24"></span>Pull Arrow 2</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnPullArrow3" runat="server" OnClick="lnkbtnPullArrow3_Click"><span class="NaviMidIcon25"></span>Pull Arrow 3</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnPhysicalPull" runat="server" OnClick="lnkbtnPhysicalPull_Click"><span class="NaviMidIcon26"></span>Physical Pull</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnSequencedPullBall" runat="server" OnClick="lnkbtnSequencedPullBall_Click"><span class="NaviMidIcon27"></span>Sequenced Pull Ball</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnLoadLeveling" runat="server" OnClick="lnkbtnLoadLeveling_Click"><span class="NaviMidIcon28"></span>Load Leveling</asp:LinkButton></li>
                                </ul>
                            </li>
                            <asp:HiddenField ID="hdnSupplier" runat="server" />
                        </ul>
                    </div>
                    <div class="filter_stripArw" id="Div1" runat="server">
                        <ul id="arw">
                            <li class="bdr_none"><a href="#" class="col_grey" style="font: 18px Arial, Helvetica, sans-serif;line-height: 38px !important;padding-left: 8px !important;">Draw Arrows</a>
                                <ul style="z-index: 2; overflow-y: scroll!important; right: 0px">
                                    <li id="li2" runat="server">
                                        <asp:LinkButton ID="lnkbtnArrow1" runat="server" OnClick="lnkbtnArrow1_Click"><span class="ArrowIcon1"></span>Right to Bottom</asp:LinkButton></li>
                                    <li id="li3" runat="server">
                                        <asp:LinkButton ID="lnkbtnArrow2" runat="server" OnClick="lnkbtnArrow2_Click"><span class="ArrowIcon2"></span>Left to Top</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow3" runat="server" OnClick="lnkbtnArrow3_Click"><span class="ArrowIcon3"></span>Top to Right</asp:LinkButton></li>
                                    <li id="li4" runat="server">
                                        <asp:LinkButton ID="lnkbtnArrow4" runat="server" OnClick="lnkbtnArrow4_Click"><span class="ArrowIcon4"></span>Bottom to Left</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow5" runat="server" OnClick="lnkbtnArrow5_Click"><span class="ArrowIcon5"></span>Right to Top</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow6" runat="server" OnClick="lnkbtnArrow6_Click"><span class="ArrowIcon6"></span>Right to Bottom</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow7" runat="server" OnClick="lnkbtnArrow7_Click"><span class="ArrowIcon7"></span>Equal Top</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow8" runat="server" OnClick="lnkbtnArrow8_Click"><span class="ArrowIcon8"></span>Equal Bottom</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow9" runat="server" OnClick="lnkbtnArrow9_Click"><span class="ArrowIcon9"></span>Top to Left</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow10" runat="server" OnClick="lnkbtnArrow10_Click"><span class="ArrowIcon10"></span>Bottom to Right</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow11" runat="server" OnClick="lnkbtnArrow11_Click"><span class="ArrowIcon11"></span>Bottom to Left</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow12" runat="server" OnClick="lnkbtnArrow12_Click"><span class="ArrowIcon12"></span>Top to Right</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow13" runat="server" OnClick="lnkbtnArrow13_Click"><span class="ArrowIcon13"></span>Equal</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow14" runat="server" OnClick="lnkbtnArrow14_Click"><span class="ArrowIcon14"></span>Top bottom to Left</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow15" runat="server" OnClick="lnkbtnArrow15_Click"><span class="ArrowIcon15"></span>Bottom top to Left</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow16" runat="server" OnClick="lnkbtnArrow16_Click"><span class="ArrowIcon16"></span>Bottom top to Right</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow17" runat="server" OnClick="lnkbtnArrow17_Click"><span class="ArrowIcon17"></span>Top botom to Right</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow18" runat="server" OnClick="lnkbtnArrow18_Click"><span class="ArrowIcon18"></span>Top to Bottom</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow19" runat="server" OnClick="lnkbtnArrow19_Click"><span class="ArrowIcon19"></span>Bottom to Top</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow20" runat="server" OnClick="lnkbtnArrow20_Click"><span class="ArrowIcon20"></span>Left to Right</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkbtnArrow21" runat="server" OnClick="lnkbtnArrow21_Click"><span class="ArrowIcon21"></span>Right to Left</asp:LinkButton></li>
                                </ul>
                            </li>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </ul>
                    </div>
                </div>
                <%--  <div class="bottom_container">
                    <div class="Clear">
                    </div>--%>
                <div class="bottom_container" id="MainDiv1" runat="server" style="position: relative; background-color: #fffff; z-index: 50; overflow-y: auto">
                </div>
                <%--</div>--%>
            </div>
            <div style="display: none">
                <div id="divTemp" runat="server">
                    <asp:Label ID="lblStore" runat="server" Visible="false" />
                </div>
            </div>
            <%--<asp:Button ID="Button1" runat="server" Text="Button" style="display:none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="imgClosepr"
                 TargetControlID="Button1" PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader"
                Drag="true" BackgroundCssClass="AjaxLoaderOuter">
            </asp:ModalPopupExtender>--%>
            <asp:Panel ID="Panel1" runat="server" CssClass="AjaxLoaderOuter1">
                <div class="AttribWrPoupProcess">
                    <asp:ImageButton ID="imgCloseTree" runat="server" CssClass="CloseBtnP" OnClick="imgCloseTree_OnClick"
                        ImageUrl="~/images/close_btn.png" />
                    <h2>Add Title</h2>
                    <div class="AttribMid">
                        <div id="LeftAtProcess" style="width: 320px!important">
                            <ul class="LeftFrm" style="width: 320px!important">
                                <li>
                                    <label style="width: 32%!important">
                                        Title Name</label>
                                    <asp:TextBox ID="txtTitleName" MaxLength="150" runat="server" CssClass="AttrTxtFild"
                                        TabIndex="1" Width="185px" Style="margin-left: 3px; margin-top: -5px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtTitleName" runat="server" InitialValue="" ValidationGroup="addValue"
                                        ControlToValidate="txtTitleName" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <asp:Button ID="btnAddTitle" runat="server" CssClass="BlueBtnLe" Text="Add Title"
                                        ValidationGroup="addValue" CausesValidation="true" OnClick="btnAddTitle_Click1" />
                                </li>
                            </ul>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" CssClass="AjaxLoaderOuter1">
                <div class="AttributeWrpPoupInventery" id="popup_box_Inventery" runat="server">
                    <div class="AttribWrPoupInventery" style="width: 330px; float: left; border: 1px solid #ccc; background: #fff; position: relative; margin: 105px 493px 0;">
                        <%-- <a href="#" class="CloseBtnPInventery" id="imgCloseInventory"></a>--%>
                        <asp:ImageButton ID="imgCloseInventoryP" runat="server" CssClass="CloseBtnPInventery"
                            OnClick="imgCloseInventoryP_OnClick" ImageUrl="~/images/close_btn.png" />
                        <h2>Add Inventory</h2>
                        <div class="AttribMid">
                            <div id="LeftAtInventery">
                                <ul class="LeftFrm">
                                    <li>
                                        <label style="width: 28%!important">
                                            CT</label>
                                        <asp:TextBox ID="txtCT" runat="server" MaxLength="50" CssClass="AttrTxtFild" TabIndex="1"
                                            Width="245px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="Numbers" runat="server" TargetControlID="txtCT"
                                            FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtCT" ErrorMessage="Please enter the CT name"
                                            InitialValue="" ValidationGroup="addInventery" ForeColor="Red" EnableClientScript="true"
                                            Text="*">
                         
                                        </asp:RequiredFieldValidator>
                                    </li>
                                    <li>
                                        <label style="width: 28%!important">
                                            Doller $</label>
                                        <asp:TextBox ID="txtdoller" MaxLength="50" runat="server" CssClass="AttrTxtFild"
                                            TabIndex="2" Width="245px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtdoller"
                                            FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="reqtxtdoller" runat="server" InitialValue="" ValidationGroup="addInventery"
                                            ControlToValidate="txtdoller" ErrorMessage="Please enter the doller" ForeColor="Red"
                                            EnableClientScript="true" Text="*">
                                        </asp:RequiredFieldValidator>
                                    </li>
                                    <li>
                                        <label style="width: 28%!important">
                                            Time</label>
                                        <asp:TextBox ID="txttime" MaxLength="50" runat="server" CssClass="AttrTxtFild" TabIndex="3"
                                            Width="245px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txttime"
                                            FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldtxttime" runat="server" InitialValue=""
                                            ValidationGroup="addInventery" ControlToValidate="txttime" ErrorMessage="Please enter the time"
                                            ForeColor="Red" EnableClientScript="true" Text="*">
                                        </asp:RequiredFieldValidator>
                                    </li>
                                    <li runat="server" id="lstInventoryNextTo">
                                        <label style="width: 60%!important">
                                            Next To Activity</label>
                                        <asp:DropDownList ID="ddlInventoryNextTo" runat="server" AutoPostBack="false" CssClass="AttrSeFild"
                                            Style="width: 243px!important;">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0"
                                        ValidationGroup="addParallel" ControlToValidate="ddlNexttoActivity" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    </li>
                                    <li>
                                        <asp:Button ID="addInventeryBtn" ValidationGroup="addInventery" runat="server" CssClass="BlueBtnLe"
                                            Text="Add" TabIndex="4" OnClick="addInventeryBtn_Click" />
                                    </li>
                                </ul>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:"
                                ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" ValidationGroup="addInventery"
                                ForeColor="Red" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlParallelProcess" runat="server" CssClass="AjaxLoaderOuter1">
                <%--<div class="AttributeWrpPoupInventery" id="Div1" runat="server">--%>
                <div class="AttribWrPoupInventery" style="width: 500px; float: left; border: 1px solid #ccc; background: #fff; position: relative; margin: 140px 415px;">
                    <asp:ImageButton ID="imgCloseParallelP" runat="server" CssClass="CloseBtnPInventery"
                        OnClick="imgCloseParallelP_OnClick" ImageUrl="~/images/close_btn.png" />
                    <h2>Add Parallel Process</h2>
                    <div class="AttribMid">
                        <div id="Div2">
                            <ul class="LeftFrm" style="width: 500px">
                                <li>
                                    <label style="width: 28%!important; padding-top: 5px;">
                                        Process Name</label>
                                    <asp:TextBox ID="txtParallelProcessName" runat="server" MaxLength="50" CssClass="AttrTxtFild"
                                        TabIndex="1" Width="245px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtParallelProcessName"
                                        ErrorMessage="Please enter Process name" InitialValue="" ValidationGroup="addParallel"
                                        ForeColor="Red" EnableClientScript="true" Text="*">                         
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label style="width: 28%!important; padding-top: 5px;">
                                        Parallel Activity</label>
                                    <asp:DropDownList ID="ddlParallelActivity" runat="server" AutoPostBack="true" CssClass="AttrSeFild"
                                        Style="width: 243px!important;" OnSelectedIndexChanged="ddlParallelActivity_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                        ValidationGroup="addParallel" ControlToValidate="ddlParallelActivity" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li id="lstRelational" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div style="width: 100%; float: left;">
                                                <div style="width: 49%; float: left; max-height: 170px; overflow-y: scroll; margin-right: 5px;">
                                                    <label style="width: 90%!important">
                                                        Relation From</label>
                                                    <asp:CheckBoxList ID="chklstActivitiesFrom" runat="server" Width="200px" SelectionMode="Multiple"
                                                        CssClass="chkk">
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div style="width: 49%; float: left; max-height: 170px; overflow-y: scroll">
                                                    <label style="width: 81%!important">
                                                        Relation To</label>
                                                    <asp:CheckBoxList ID="chklstActivitiesTo" runat="server" Width="200px" SelectionMode="Multiple"
                                                        CssClass="chkk">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ddlParallelActivity" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </li>
                                <li>
                                    <asp:Button ID="btnAddParallelProcess" ValidationGroup="addParallel" runat="server"
                                        CssClass="BlueBtnLe" Text="Add" TabIndex="4" OnClick="btnAddParallelProcess_Click" />
                                </li>
                            </ul>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
                <div class="Clear">
                </div>
                <%--  </div>--%>
            </asp:Panel>
            <asp:Panel ID="pnlAddProcess" runat="server" CssClass="AjaxLoaderOuter1">
                <%--<div class="AttributeWrpPoupInventery" id="Div1" runat="server">--%>
                <div class="AttribWrPoupInventery" style="width: 420px; float: left; border: 1px solid #ccc; background: #fff; position: relative; margin: 140px 415px;">
                    <asp:ImageButton ID="imgCloseaddProcess" runat="server" CssClass="CloseBtnPInventery"
                        ImageUrl="~/images/close_btn.png" />
                    <h2>Add Process</h2>
                    <div class="AttribMid">
                        <div id="Div3">
                            <ul class="LeftFrm" style="width: 420px">
                                <li>
                                    <label style="width: 30%!important; padding-top: 5px;">
                                        Activity Name</label>
                                    <asp:TextBox ID="txtAddActivity" runat="server" MaxLength="50" CssClass="AttrTxtFild"
                                        TabIndex="1" Width="245px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddActivity"
                                        ErrorMessage="Please enter Activity name" InitialValue="" ValidationGroup="addParallel"
                                        ForeColor="Red" EnableClientScript="true" Text="*">                         
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li runat="server" id="lstAddProcess">
                                    <label style="width: 30%!important; padding-top: 5px;">
                                        Next To Activity</label>
                                    <asp:DropDownList ID="ddlNexttoActivity" runat="server" AutoPostBack="false" CssClass="AttrSeFild"
                                        Style="width: 243px!important;">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0"
                                        ValidationGroup="addParallel" ControlToValidate="ddlNexttoActivity" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
<<<<<<< HEAD
                                </li>
                                <li>
                                    <asp:Button ID="btnAddProcessActivity" ValidationGroup="addParallel" runat="server" CssClass="BlueBtnLe"
                                        Text="Add" TabIndex="4" OnClick="btnAddProcessActivity_Click" />
                                </li>
                            </ul>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
                <div class="Clear">
                </div>
                <%--  </div>--%>
            </asp:Panel>
 

        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="http://code.jquery.com/jquery-migrate-1.2.1.js"></script>
</asp:Content>
=======
                                </li>
                                <li>
                                    <asp:Button ID="btnAddProcessActivity" ValidationGroup="addParallel" runat="server" CssClass="BlueBtnLe"
                                        Text="Add" TabIndex="4" OnClick="btnAddProcessActivity_Click" />
                                </li>
                            </ul>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
                <div class="Clear">
                </div>
                <%--  </div>--%>
            </asp:Panel>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
>>>>>>> f0aeb939c7bffcbbd47a69dcf2294111dbd5e9eb
