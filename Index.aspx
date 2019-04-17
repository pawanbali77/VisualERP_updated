<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Welcome to Visual ERP</title>
    <script type='text/javascript' src='css/jquery-1.2.3.min.js'></script>
    <script type='text/javascript' src='css/menu.js'></script>
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var contentHeight = $(window).height();
            var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 73; // + "px";
            $("#MainDiv").css("height", newHeight + "px");
        });
    </script>
     <script type="text/javascript" >
         function adddiv() {

             if ($("#MainDiv #tblMain #trmain")[0].childElementCount == 0) {

                  $("#MainDiv #tblMain #trmain").append("<td><table class='activity_block' cellpadding='0' cellspacing='0'><tr class='activity_block_top'><td class='th_fieldsi left'><a href='#' id='href1' >Attribute</a></td><td class='th_fieldsi'><a href='#'>Inputs</a></td><td class='th_fieldsi'><a href='#'>BOM</a></td><td class='th_fieldsi'><a href='#'>TFG</a></td><td class='th_fieldsi'><a href='#'>Machine</a></td><tr><td colspan='5' class='activity_txt'>Activity 1</td></tr><tr><td colspan='5'><table border='0' cellpadding='0' cellspacing='0' width='100%'> <tr class='block_1_top'> <td class='attributes'> <a class='block_arrow' href='#'>Attribute</a></td> <td class='value'><a class='block_arrow' href='#'>Value</a></td><td class='unit'><a class='block_arrow' href='#'>Unit</a></td></tr><tr class='field_row bg_white'><td class='attributes_td'>VA Time:</td><td class='value_td'>12</td><td class='unit_td'>Sec</td></tr><tr class='field_row'><td class='attributes_td'>NVA Ti</td><td class='value_td'>15</td><td class='unit_td'>Min</td></tr><tr class='field_row bg_white'><td class='attributes_td'>VA Time</td><td class='value_td'>1</td><td class='unit_td'>Min</td></tr><tr class='field_row'><td class='attributes_td'>NVA Time</td><td class='value_td'>15</td><td class='unit_td'>Min</td></tr></table></td> </tr></table></td>");
             }
              else {
                 
                  $("#MainDiv #tblMain #trmain").append("<td><div class='nxt_arrow'><img src='images/nxt_arrow.png'/></div></td><td><table class='small_block' cellpadding='0' cellspacing='0'><tr><td class='small_block_top' colspan='3'><img src='images/small_block_top.png'/></td></tr><tr class=block_1_top'><td class='Small_b3' style='padding-left: 2%; width: 32%; border-left: 1px solid #CCCCCC;><a class='block_arrow' href='#'>CT</a></td><td class='Small_b3' style='padding-left: 2%; width: 29.9%;><a class='block_arrow' href='#'>$</a></td><td class='Small_b3' style='border-right: 1px solid #CCCCCC;'><a class='block_arrow' href='#'>Time</a></td></tr><tr class='bg_white' style='box-shadow: 4px 5px 4px #F3F3F3;'><td class='Small_b3' style='padding-left: 2%; width: 32%; border-left: 1px solid #CCCCCC;border-right: 1px solid #CCCCCC;'>CT</td><td class='Small_b3' style='padding-left: 2%; width: 26.8%; border-right: 1px solid #CCCCCC;'>$</td><td class='Small_b3' style='border-right: 1px solid #CCCCCC; padding-left: 2%;'>Time</td></tr></table> </td><td><div class='nxt_arrow'><img src='images/nxt_arrow.png' /></div></td><td><table class='activity_block' cellpadding='0' cellspacing='0'><tr class='activity_block_top'><td class='th_fieldsi left'><a href='#'>Attribute</a></td><td class='th_fieldsi'><a href='#'>Inputs</a></td><td class='th_fieldsi'><a href='#'>BOM</a></td><td class='th_fieldsi'><a href='#'>TFG</a></td><td class='th_fieldsi'><a href='#'>Machine</a></td><tr><td colspan='5' class='activity_txt'>Activity 1</td></tr><tr><td colspan='5'><table border='0' cellpadding='0' cellspacing='0' width='100%'> <tr class='block_1_top'> <td class='attributes'> <a class='block_arrow' href='#'>Attribute</a></td> <td class='value'><a class='block_arrow' href='#'>Value</a></td><td class='unit'><a class='block_arrow' href='#'>Unit</a></td></tr><tr class='field_row bg_white'><td class='attributes_td'>VA Time:</td><td class='value_td'>12</td><td class='unit_td'>Sec</td></tr><tr class='field_row'><td class='attributes_td'>NVA Ti</td><td class='value_td'>15</td><td class='unit_td'>Min</td></tr><tr class='field_row bg_white'><td class='attributes_td'>VA Time</td><td class='value_td'>1</td><td class='unit_td'>Min</td></tr><tr class='field_row'><td class='attributes_td'>NVA Time</td><td class='value_td'>15</td><td class='unit_td'>Min</td></tr></table></td> </tr></table></td>");              
             }
         }
    </script>
   
</head>
<body>
    <form id="form1" runat="server">
  
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />

    <asp:UpdatePanel ID="updatefirst" runat="server" UpdateMode="Always">
    <ContentTemplate>
    <div class="main_container">
        <div class="header" id="header">
            <div class="Logo">
                <a href="#">
                    <img src="images/logo.png" /></a></div>
            <div class="header_right">
                <ul>
                    <li><a href="#" class="process_manager">Process View</a></li>
                    <li><a href="#" class=" enterprize_manager">Enterprise Manager</a></li>
                </ul>
            </div>
        </div>
        <div class="mid_container">
            <div class="side_bar">
                <asp:TreeView ID="TreeView1" runat="server" CssClass="TreeView1_0" ShowLines="true" OnTreeNodePopulate="MyTreeView_TreeNodePopulate" ForeColor="#555555" HoverNodeStyle-ForeColor="#000">
                </asp:TreeView>
            </div>
            <div class="right_container">
                <%--<div class="right_top"></div>--%>
                <div class="right_container_top" id="Title">
                    <h1>
                        Process View</h1>
                    <div class="right_nav">
                        <ul>
                            <li><a href="#">
                                <img src="images/zoom-in.png" /></a></li>
                            <li><a href="#">
                                <img src="images/zoom-out.png" /></a></li>
                        </ul>
                    </div>
                </div>
                <div class="filter_strip" id="Bpmn">
                    <ul id="nav">
                        <li class="bdr_none"><a href="#" class="col_grey">BPMN icon</a>
                            <ul>
                                <li><a href="#" onclick="adddiv();"><span class="NaviMidIcon2" ></span>Process</a></li>
                                <li><a href="#" ><span class="NaviMidIcon2"></span>Customer Supplier</a></li>
                                <li><a href="#"><span class="NaviMidIcon3"></span>Inventory</a></li>
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
      
                <div class="bottom_container">
                    <div class="Clear">
                    </div>
                    <div class="block_2 margin_top" id="MainDiv">
                        <table id="tblMain" width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr id="trmain" >
                            </tr>
                        </table>
                    </div>
                   
                </div>
                <div class="Clear">
                </div>
            </div>
        </div>
        <asp:ModalPopupExtender ID="mopoExUser" PopupControlID="pnlist" TargetControlID="Button1"
            runat="server"  BackgroundCssClass="AjaxLoaderOuter" 
              BehaviorID="mopoExUser" CancelControlID="imgClose">
        </asp:ModalPopupExtender>
        <asp:Button ID="Button1" runat="server" style="display:none" />
        <asp:Panel ID="pnlist" runat="server" style="display: none">
        <div class="AttributeWrp"  id="divAttr">
            <div class="AttribWrpIn">
                <a href="#" class="CloseBtnP" id="imgClose"></a>
                <h2>
                    Attributes</h2>
                <div class="AttribMid">
                    <div id="LeftAt">
                        <ul class="LeftFrm">
                            <li>
                                <label>
                                    Include on Map?</label>
                                <input name="" type="radio" value="" class="radioBtn" />
                                <span>Yes</span>
                                <input name="" type="radio" value="" class="radioBtn" />
                                <span>No</span></li>
                            <li>
                                <label>
                                    Attribute Name</label>
                                <input name="" type="text" class="AttrTxtFild" />
                            </li>
                            <li>
                                <label>
                                    Attribute Value</label>
                                <input name="" type="text" class="AttrTxtFild" />
                            </li>
                            <li>
                                <label>
                                    Units</label>
                                <select name="" class="AttrSeFild">
                                </select>
                            </li>
                            <li>
                                <input name="" type="button" class="BlueBtnLe" value="Add Attribute" />
                            </li>
                        </ul>
                    </div>
                    <div class="RightAt">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th>
                                    Include on Map
                                </th>
                                <th>
                                    Attribute
                                </th>
                                <th>
                                    Value
                                </th>
                                <th>
                                    Units
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    Yes
                                </td>
                                <td>
                                    No. of Employees
                                </td>
                                <td>
                                    12
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr class="GrayBg">
                                <td>
                                    No
                                </td>
                                <td>
                                    Name of Employee 1
                                </td>
                                <td>
                                    15
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Yes
                                </td>
                                <td>
                                    No. of Employees
                                </td>
                                <td>
                                    12
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr class="GrayBg">
                                <td>
                                    No
                                </td>
                                <td>
                                    Name of Employee 1
                                </td>
                                <td>
                                    15
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Yes
                                </td>
                                <td>
                                    No. of Employees
                                </td>
                                <td>
                                    12
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr class="GrayBg">
                                <td>
                                    No
                                </td>
                                <td>
                                    Name of Employee 1
                                </td>
                                <td>
                                    15
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Yes
                                </td>
                                <td>
                                    No. of Employees
                                </td>
                                <td>
                                    12
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr class="GrayBg">
                                <td>
                                    No
                                </td>
                                <td>
                                    Name of Employee 1
                                </td>
                                <td>
                                    15
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Yes
                                </td>
                                <td>
                                    No. of Employees
                                </td>
                                <td>
                                    12
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr class="GrayBg">
                                <td>
                                    No
                                </td>
                                <td>
                                    Name of Employee 1
                                </td>
                                <td>
                                    15
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Yes
                                </td>
                                <td>
                                    No. of Employees
                                </td>
                                <td>
                                    12
                                </td>
                                <td>
                                    Count
                                </td>
                            </tr>
                           
                        </table>
                    </div>
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
        <div class="footer" id="footer">
            <p>
                © Copyright GroEngine, LLC 2013. All Rights Reserved</p>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
