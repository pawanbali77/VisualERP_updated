<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ManageReport.aspx.cs" Inherits="ManageReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function test1() {

            var contentHeight = $(window).height();
            var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 20; // + "px";        
            var remainCnt = $("#header").height() + $("#footer").height() + 50;
            var newHeight1 = contentHeight - remainCnt;
            $(".TreeView1_0").css("height", newHeight1 + "px");
        }

        function test() {
            window.setTimeout(function () {
                $(".isa_info").fadeOut('slow', function () { $('.isa_info').remove() });
                $(".isa_success").fadeOut('slow', function () { $('.isa_success').remove() });
                $(".isa_warning").fadeOut('slow', function () { $('.isa_warning').remove() });
                $(".isa_error").fadeOut('slow', function () { $('.isa_error').remove() });
            }, 7000);
        }

        function loadProcessManager() {
            location.reload();
        }
      
    </script>   
    <script type="text/javascript">
        function Selectall(chkAllAttributes) {
            if ($(chkAllAttributes).is(":checked")) {
                $("[id*=chkboxAttribute] input").attr("checked", "checked");
            } else {
                $("[id*=chkboxAttribute] input").removeAttr("checked");
            }
        }
    </script>
    <script type="text/javascript">
        function SelectallActivity(chkSelectAllActivity) {
            if ($(chkSelectAllActivity).is(":checked")) {
                $("[id*=chkboxActivity] input").attr("checked", "checked");
            } else {
                $("[id*=chkboxActivity] input").removeAttr("checked");
            }
        }
    </script>
    <script type="text/javascript">
        function SelectAllBomProcess(chkSelectAllBom) {
            if ($(chkSelectAllBom).is(":checked")) {
                $("[id*=chkboxBomProcess] input").attr("checked", "checked");
            } else {
                $("[id*=chkboxBomProcess] input").removeAttr("checked");
            }
        }
    </script>
    <script type="text/javascript">
        function loadProcessManager() {
            location.reload();
        }

    </script>
    <style type="text/css">
        .Hide
        {
            display: none;
        }
    </style>
    <style type="text/css">
        
        
         .RightAtBom table th
        {
            font: bold 12px Arial, Helvetica, sans-serif;
            padding: 0 15px 0 15px;
            text-align: center;
        }
        
        .RightAtBom table td
        {
            font: normal 12px Arial, Helvetica, sans-serif;
            text-align: center;
            padding: 12px 15px 12px 15px;
        }       
        .RightAt table th
        {
            font: bold 12px Arial, Helvetica, sans-serif;
            padding: 0 15px 0 15px;
            text-align: center;
        }
        
        .RightAt table td
        {
            font: normal 12px Arial, Helvetica, sans-serif;
            text-align: center;
            padding: 12px 15px 12px 15px;
        }       
        .itemstyle
        {
           width: 150px;
           overflow: hidden;
           white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container_top TopMarg" id="Title">
                <h1>
                    Report Manager
                    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>                          
                            <div id="divRecordCount" runat="server" style="font: bold 12px Arial, Helvetica, sans-serif;
                                color: #555; padding: 0px 0px 0px 530px; float: right; width: 900px; margin-top: -5px;">
                                <asp:Literal ID="ltrTotalRecord" runat="server" />
                                <asp:Label ID="lblMsg" runat="server" Visible="false" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </h1>
                <div id="divErrorMsg" runat="server" style="font: bold 12px Arial, Helvetica, sans-serif;
                    color: #555; padding: 7px 0px 0px 10px; height: 30px; float: left; min-width: 450px;
                    max-width: 500px;">
                    <asp:Label ID="lblMsg" runat="server" />
                </div>
                <div class="right_nav">
                    <ul>
                        <li id="liSaveReport" runat="server">
                            <%--<asp:UpdatePanel ID="updExport" runat="server">
                        <ContentTemplate>--%>
                            <asp:LinkButton ID="lnkbtnSaveReport" runat="server" Text="Save Report" CssClass="DesignBtn"
                                OnClick="lnkbtnSaveReport_Click" ValidationGroup="addReport" Visible="false"></asp:LinkButton>
                            <%--</ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkbtnExporttoExcel" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                        </li>
                        <li id="liExporttoExcel" runat="server">
                            <asp:LinkButton ID="lnkbtnExporttoExcel" runat="server" Text="Export to Excel" CssClass="DesignBtn"
                                OnClick="lnkbtnExporttoExcel_Click" Visible="false" />
                        </li>
                      <%--  <li style="line-height: 27px; margin-top: 4px; height: 43px">
                            <asp:LinkButton ID="lnkbtnList" runat="server" ToolTip="Saved Report" CssClass="iconList1"
                                OnClick="lnkbtnList_Click"></asp:LinkButton>
                        </li>--%>
                        <li style="line-height: 27px; margin-top: 4px;">
                            <asp:LinkButton ID="lnkbtnAdd" runat="server" ToolTip="New Report" CssClass="iconAdd1"
                                OnClick="lnkbtnAdd_Click"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
            <asp:Panel ID="pnlReportType" runat="server" Visible="false">
                <div class="ActivityPopupTop" id="div1" runat="server">
                    <div class="ActivityPopup">
                        <h2 style="font: bold 15px/32px Arial,Helvetica,sans-serif!important">
                            Select Report Type</h2>
                        <div class="ActivitybMid" style="min-height: 50px!important">
                            <div class="LeftAtActivity">
                                <ul class="ActivtyNew" style="margin-top: 5px;">
                                    <li><span style="background: url(images/report.png) no-repeat left top!important;
                                        padding-left: 27px; height: 29px; float: left; margin-left: 10px;"></span>
                                        <asp:LinkButton ID="lnkBtnAttributeReport" OnClick="lnkBtnAttributeReport_Click"
                                            runat="server" Text="Attribute Report" Style="font: bold 13px/32px Arial,Helvetica,sans-serif;
                                            color: #555555; margin-left: 17px;" /></li>
                                    <li><span style="background: url(images/report.png) no-repeat left top!important;
                                        padding-left: 27px; height: 29px; float: left; margin-left: 10px;"></span>
                                        <asp:LinkButton ID="lnkBtnBOMReport" OnClick="lnkBtnBOMReport_Click" runat="server"
                                            Text="BOM Report" Style="font: bold 13px/32px Arial,Helvetica,sans-serif; color: #555555;
                                            margin-left: 17px;" /></li>
                                    <li><span style="background: url(images/report.png) no-repeat left top!important;
                                        padding-left: 27px; height: 29px; float: left; margin-left: 10px;"></span>
                                        <asp:LinkButton ID="lnkBtnTFGReport" OnClick="lnkBtnTFGReport_Click" runat="server"
                                            Text="TFG Report" Style="font: bold 13px/32px Arial,Helvetica,sans-serif; color: #555555;
                                            margin-left: 17px;" /></li>
                                    <li><span style="background: url(images/report.png) no-repeat left top!important;
                                        padding-left: 27px; height: 29px; float: left; margin-left: 10px;"></span>
                                        <asp:LinkButton ID="lnkBtnMachineReport" OnClick="lnkBtnMachineReport_Click" runat="server"
                                            Text="Machine Report" Style="font: bold 13px/32px Arial,Helvetica,sans-serif;
                                            color: #555555; margin-left: 17px;" /></li>
                                    <li id="liPPESA" runat="server"><span style="background: url(images/report.png) no-repeat left top!important;
                                        padding-left: 27px; height: 29px; float: left; margin-left: 10px;"></span>
                                        <asp:LinkButton ID="lnkBtnPPESAReport" OnClick="lnkBtnPPESAReport_Click" runat="server"
                                            Text="Process Capability Scorecard" Style="font: bold 13px/32px Arial,Helvetica,sans-serif;
                                            color: #555555; margin-left: 17px;" /></li>
                                    <li id="liPDESA" runat="server"><span style="background: url(images/report.png) no-repeat left top!important;
                                        padding-left: 27px; height: 29px; float: left; margin-left: 10px;"></span>
                                        <asp:LinkButton ID="lnkBtnPDESAReport" OnClick="lnkBtnPDESAReport_Click" runat="server"
                                            Text="Design Capability Scorecard" Style="font: bold 13px/32px Arial,Helvetica,sans-serif;
                                            color: #555555; margin-left: 17px;" /></li>


                                     <li id="liTgtValueGap" runat="server"><span style="background: url(images/report.png) no-repeat left top!important;
                                        padding-left: 27px; height: 29px; float: left; margin-left: 10px;"></span>
                                        <asp:LinkButton ID="btnTgtValueGap" OnClick="btnTgtValueGap_Click" runat="server"
                                            Text="Target Value Gap" Style="font: bold 13px/32px Arial,Helvetica,sans-serif;
                                            color: #555555; margin-left: 17px;" /></li>

                                    <%--<li><a href="#"><span class="ReportTag"></span>TFG Report</a></li>
                                    <li><a href="#"><span class="ReportTag"></span>Machine Report</a></li>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlActivity" runat="server" Visible="false">
                <div class="ActivityPopupTop">
                    <div class="ActivityPopup">
                        <h2 style="font: bold 15px/32px Arial,Helvetica,sans-serif!important" id="headerTitle"
                            runat="server">
                            Select Activity</h2>
                        <div class="ActivitybMid" id="divAttribute" runat="server">
                            <div class="LeftAtActivity">
                                <ul class="ActivtyNew">
                                    <li>
                                        <div class="fixheight" style="height: 150px; margin-top: 15px; overflow-y: scroll;
                                            overflow-x: hidden;">
                                            <asp:CheckBox ID="chkSelectAllActivity" Checked="false" Text="Select All" runat="server"
                                                onclick="SelectallActivity(this)" />
                                            <asp:CheckBoxList ID="chkboxActivity" CssClass="checkbox" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </li>
                                    <li>
                                        <asp:Button ID="btnNextToActivity" runat="server" Text="Next" CssClass="btnNextNew"
                                            OnClick="btnNextToActivity_Click" />
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlAttribute" runat="server" Visible="false">
                <div class="ActivityPopupTop">
                    <div class="ActivityPopup">
                        <h2 style="font: bold 15px/32px Arial,Helvetica,sans-serif!important">
                            Select Attribute</h2>
                        <div class="ActivitybMid">
                            <div class="LeftAtActivity">
                                <ul class="ActivtyNew">
                                    <li>
                                        <div class="fixheight" style="height: 150px; margin-top: 15px; overflow-y: scroll;
                                            overflow-x: hidden;">
                                            <asp:CheckBox ID="chkAllAttributes" Checked="false" Text="Select All" runat="server"
                                                onclick="Selectall(this)" />
                                            <asp:CheckBoxList ID="chkboxAttribute" runat="server" Style="margin-left: 10px">
                                            </asp:CheckBoxList>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <asp:Button ID="btnNextToAttribute" Text="Next" runat="server" Style="margin: -38px 90px 0 0!important;"
                            CssClass="btnNextNew" OnClick="btnNextToAttribute_Click" />
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlBomProcess" runat="server" Visible="false">
                <div class="ActivityPopupTop">
                    <div class="ActivityPopup" style="height: 237px;">
                        <h2 style="font: bold 15px/32px Arial,Helvetica,sans-serif!important">
                            Select Bom Process</h2>
                        <div class="ActivitybMid">
                            <div class="LeftAtActivity">
                                <ul class="ActivtyNew">
                                    <li>
                                        <div class="fixheight" style="height: 150px; margin-top: 15px; overflow-y: scroll;
                                            overflow-x: hidden;">
                                            <asp:CheckBox ID="chkSelectAllBom" Checked="false" Text="Select All" runat="server"
                                                onclick="SelectAllBomProcess(this)" />
                                            <asp:CheckBoxList ID="chkboxBomProcess" CssClass="checkbox" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                        <%-- <asp:CheckBoxList ID="chkboxBomProcess" runat="server" Style="margin-left: 10px">
                                        </asp:CheckBoxList>--%>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <asp:Button ID="btnNextToBomProcess" Text="Next" runat="server" Style="margin: -38px 90px 0 0!important;"
                            CssClass="btnNextNew" OnClick="btnNextToBomProcess_Click" />
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlBomReport" runat="server" Visible="false">
                <%--<div class="RightAt" id="divBomReport" runat="server" visible="true" style="overflow-y: scroll;
                    max-height: 400px; float: right; margin-top: 99px; width: 1047px!important; margin-right: 5px;">--%>
                <div class="RightAtBom" id="div3" runat="server" visible="true" style="float: right;
                    margin-right: 10px; margin-top: 110px; width: 1030px !important; overflow-y: scroll;
                    max-height: 300px;">
                    <asp:GridView ID="grdBomReport" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bom Process" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                 <div class="itemstyle">
                                 <%#DataBinder.Eval(Container.DataItem, "BomProcessName")%>
                                 </div>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                 <div class="itemstyle">
                                <%#DataBinder.Eval(Container.DataItem, "Description")%>
                                 </div>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BOMLevel" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                 <div class="itemstyle">
                                <%-- <%#DataBinder.Eval(Container.DataItem, "BOMLevel")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "BOMLevel") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "BOMLevel")%>
                                 </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BOMRevision" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                <%-- <%#DataBinder.Eval(Container.DataItem, "BOMRevision")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "BOMRevision") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "BOMRevision")%>
                                 </div>                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="weight" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "weight")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "weight") == null ? "-" : DataBinder.Eval(Container.DataItem, "weight")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "UOM")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "UOM") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "UOM")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StandardCost" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "StandardCost", "{0:n}")%>--%>
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "StandardCost") == null ? "-" : DataBinder.Eval(Container.DataItem, "StandardCost")%>--%>
                                    <%#Convert.ToString(Eval("StandardCost")) == "0" ? "0" : "$" + Eval("StandardCost", "{0:f2}")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="WorkingCost" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "WorkingCost", "{0:n}")%>--%>
                                    <%--  <%#DataBinder.Eval(Container.DataItem, "WorkingCost") == null ? "-" : DataBinder.Eval(Container.DataItem, "WorkingCost")%>--%>
                                    <%#Convert.ToString(Eval("WorkingCost")) == "0" ? "0" : "$" + Eval("WorkingCost", "{0:f2}")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StdPackQty" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "StdPackQty")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "StdPackQty") == null ? "-" : DataBinder.Eval(Container.DataItem, "StdPackQty")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxPackLength" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "MaxPackLength")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "MaxPackLength") == null ? "-" : DataBinder.Eval(Container.DataItem, "MaxPackLength")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxPackWidth" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "MaxPackWidth")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "MaxPackWidth") == null ? "-" : DataBinder.Eval(Container.DataItem, "MaxPackWidth")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxPackHeight" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "MaxPackHeight")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "MaxPackHeight") == null ? "-" : DataBinder.Eval(Container.DataItem, "MaxPackHeight")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ContainerQty" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "ContainerQty")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "ContainerQty") == null ? "-" : DataBinder.Eval(Container.DataItem, "ContainerQty")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MedianRelinishmentLT" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "MedianRelinishmentLT")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "MedianRelinishmentLT") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "MedianRelinishmentLT")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MinRLT" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "MinRLT")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "MinRLT") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "MinRLT")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxRLT" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "MaxRLT")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "MaxRLT") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "MaxRLT")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rolling12MnthUsage" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "Rolling12MnthUsage")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "Rolling12MnthUsage") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "Rolling12MnthUsage")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AvgMonthUsage" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "AvgMonthUsage")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "AvgMonthUsage") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "AvgMonthUsage")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RiskFactor" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "RiskFactor")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "RiskFactor") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "RiskFactor")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MonthStdDevRiskFactor" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "MonthStdDevRiskFactor")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "MonthStdDevRiskFactor") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "MonthStdDevRiskFactor")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="KanbanQty" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "KanbanQty")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "KanbanQty") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "KanbanQty")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="InService" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "InService")%>--%>
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "InService") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "InService")%>--%>
                                    <%#(bool)DataBinder.Eval(Container.DataItem, "InService") ==false ? "No" : "Yes"%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In_ServiceDate" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "In_ServiceDate")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "In_ServiceDate") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "In_ServiceDate")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ObsolescenceDate" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "ObsolescenceDate")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "ObsolescenceDate") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "ObsolescenceDate")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OnHandInventory" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "OnHandInventory")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "OnHandInventory") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "OnHandInventory")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OnOrder" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "OnOrder")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "OnOrder") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "OnOrder")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NextShipmentDue" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "NextShipmentDue")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "NextShipmentDue") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "NextShipmentDue")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NextQtyDue" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "NextQtyDue")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "NextQtyDue") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "NextQtyDue")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PartReqNxtPerd" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "PartReqNxtPerd")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "PartReqNxtPerd") == string.Empty ? "-" : DataBinder.Eval(Container.DataItem, "PartReqNxtPerd")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CurrentPurchasingOwner" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "CurrentPurchasingOwner")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "CurrentPurchasingOwner") == null ? "-" : DataBinder.Eval(Container.DataItem, "CurrentPurchasingOwner")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CurrentDesignOwner" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                <div class="itemstyle">
                                    <%--<%#DataBinder.Eval(Container.DataItem, "CurrentDesignOwner")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "CurrentDesignOwner") == null ? "-" : DataBinder.Eval(Container.DataItem, "CurrentDesignOwner")%>
                                    </div>
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
                <div id="divReportName" runat="server" style="float: left; margin-left: 330px; margin-top: 50px;">
                    <span style="color: #40464C; font-size: 14px; font-weight: bold;">Save Report as :</span>
                    &nbsp;&nbsp;<asp:TextBox ID="txtBomReportName" runat="server" CssClass="AttrTxtFild"
                        Style="width: 200px!important; float: none!important" placeholder="Report Name" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                        ControlToValidate="txtBomReportName" ErrorMessage="Please enter report Name"
                        ValidationGroup="addReport" ForeColor="Red" Text="*">
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlAttributeReport" runat="server" Visible="false">
                <div class="RightAt" id="divReport" runat="server" visible="true" style="float: right;
                    margin-right: 10px; margin-top: 110px; width: 1033px !important; overflow-y: scroll;
                    max-height: 300px">
                    <asp:GridView ID="GridView1" runat="server">
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                    </asp:GridView>
                </div>
                <%-- <div style="float: left; width: 100%; padding-bottom: 15px;">
                        <span style="color: #40464C; font-size: 14px; font-weight: bold;">Save Report as :</span>
                        &nbsp;&nbsp;<asp:TextBox ID="txtReportName" runat="server" CssClass="AttrTxtFild"
                            Style="width: 200px!important; float: none!important" placeholder="Report Name" />
                        <asp:RequiredFieldValidator ID="reqtxtAttrivalue" runat="server" InitialValue=""
                            ValidationGroup="addReport" ControlToValidate="txtReportName" ErrorMessage="Please enter report Name"
                            ForeColor="Red" Text="*">
                        </asp:RequiredFieldValidator>
                    </div>--%>
                <div style="float: left; margin-left: 330px; margin-top: 50px;">
                    <span style="color: #40464C; font-size: 14px; font-weight: bold;">Save Report as :</span>
                    &nbsp;&nbsp;<asp:TextBox ID="txtAttributeReportName" runat="server" CssClass="AttrTxtFild"
                        Style="width: 200px!important; float: none!important" placeholder="Report Name" />
                    <asp:RequiredFieldValidator ID="reqtxtAttrivalue" runat="server" InitialValue=""
                        ControlToValidate="txtAttributeReportName" ErrorMessage="Please enter report Name"
                        ValidationGroup="addReport" ForeColor="Red" Text="*">
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlTFGReport" runat="server" Visible="false">
                <div class="RightAt" id="div2" runat="server" visible="true" style="float: right;
                    margin-right: 10px; margin-top: 110px; width: 1030px !important; overflow-y: scroll;
                    max-height: 300px;">
                    <asp:GridView ID="grdTFGReport" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tool" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Tool_Fixture_GageName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TFGDescription")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Part" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TFGVendorPart")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TFGVendor")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Vendor ID" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TFGVendorID")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="StandardCost" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "StandardCost", "{0:n}")%>                                 
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Calibration Date" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CalibrationDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="WorkingCost" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "WorkingCost", "{0:n}")%>                                 
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Cost" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "TFGCost")%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "Cost", "{0:n}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TFGQty")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calibration Cycle" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CalibrationCycle")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time To Cailbrate" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TimeToCailbrate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cost To Calibrate" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CostToCalibrate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calibration Vendor" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CalibrationVendor")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Calibration VendorID" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CalibrationVendorID")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Calibration Vendor Info" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CalibrationVendorInfo")%>
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
                <div id="divTFGName" runat="server" style="float: left; margin-left: 330px; margin-top: 50px;">
                    <span style="color: #40464C; font-size: 14px; font-weight: bold;">Save Report as :</span>
                    &nbsp;&nbsp;<asp:TextBox ID="txtTFGReportName" runat="server" CssClass="AttrTxtFild"
                        Style="width: 200px!important; float: none!important" placeholder="Report Name" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                        ControlToValidate="txtTFGReportName" ErrorMessage="Please enter report Name"
                        ValidationGroup="addReport" ForeColor="Red" Text="*">
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlMachineReport" runat="server" Visible="false">
                <div class="RightAt" id="div4" runat="server" visible="true" style="float: right;
                    margin-right: 10px; margin-top: 110px; width: 1030px !important; overflow-y: scroll;
                    max-height: 300px;">
                    <asp:GridView ID="grdMachineReport" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true">
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Machine Name" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MachineName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Machine Type" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MachineType")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Machine Photo" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MachinePhoto")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%-- <asp:TemplateField HeaderText="PM ScheduleID" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "PMScheduleID")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="MTBF" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MTBF")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MTTR" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MTTR")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Maintenance Cost" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MaintenanceCost")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purchase Price" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "PurchasePrice")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Book Value" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "BookValue")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remaining Life" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "RemainingLife")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Calibration Vendor" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ManualID")%>
                                </ItemTemplate>
                            </asp:TemplateField>    --%>
                            <%-- <asp:TemplateField HeaderText="Calibration Vendor Info" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "PartsListID")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
                <div id="divMachineName" runat="server" style="float: left; margin-left: 330px; margin-top: 50px;">
                    <span style="color: #40464C; font-size: 14px; font-weight: bold;">Save Report as :</span>
                    &nbsp;&nbsp;<asp:TextBox ID="txtMachineReportName" runat="server" CssClass="AttrTxtFild"
                        Style="width: 200px!important; float: none!important" placeholder="Report Name" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                        ControlToValidate="txtMachineReportName" ErrorMessage="Please enter report Name"
                        ValidationGroup="addReport" ForeColor="Red" Text="*">
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>
            
            <asp:Panel ID="pnlESAReport" runat="server" Visible="false">
                <div class="RightAt" id="div5" runat="server" visible="true" style="float: right;
                    margin-right: 10px; margin-top: 110px; width: 1030px !important; overflow-y: scroll;
                    max-height: 300px;">
                    <asp:GridView ID="grdESAReport" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true">
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <div class="itemstyle" style="width:50px" >
                                        <%#Container.DataItemIndex+1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Process ObjectName" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:Literal ID="litFormID" runat="server" Visible="false" Text='<%#Eval("FormID") %>' />
                                        <%#DataBinder.Eval(Container.DataItem, "ProcessObjectName")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Feature Added" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <%#DataBinder.Eval(Container.DataItem, "ProductFeatureAdded")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Function of Product Feature" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle" style="width:170px">
                                        <%#DataBinder.Eval(Container.DataItem, "FunctionofProductFeature")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Error Event" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <%#DataBinder.Eval(Container.DataItem, "ErrorEvent")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Error Event Transfer" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <%#DataBinder.Eval(Container.DataItem, "ErrorEventTransferFunction")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <%#DataBinder.Eval(Container.DataItem, "Actions")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Countermeasure" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <%#DataBinder.Eval(Container.DataItem, "Countermeasure")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Countermeasure Effectiveness" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <%#DataBinder.Eval(Container.DataItem, "CountermeasureEffectiveness")%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CPK" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCpk" runat="server" BackColor="Orange" Text='<%# Eval("Cpk") %>'
                                        CssClass="AttrTxtFild" Style="width: 120px; margin-right: 17px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CP" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCp" runat="server" BackColor="Orange" Text='<%# Eval("Cp") %>'
                                        CssClass="AttrTxtFild" Style="width: 120px; margin-right: 17px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PPK" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPpk" runat="server" BackColor="Orange" Text='<%# Eval("Ppk") %>'
                                        CssClass="AttrTxtFild" Style="width: 120px; margin-right: 17px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Long term Sigma" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLongtermSigma" runat="server" BackColor="Orange" Text='<%# Eval("LongtermSigma") %>'
                                        CssClass="AttrTxtFild" Style="width: 120px; margin-right: 17px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Short-term Sigma" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtShorttermSigma" runat="server" BackColor="Orange" Text='<%# Eval("ShorttermSigma") %>'
                                        CssClass="AttrTxtFild" Style="width: 120px; margin-right: 17px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Z-Score" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtZScore" runat="server" BackColor="Orange" Text='<%# Eval("ZScore") %>'
                                        CssClass="AttrTxtFild" Style="width: 120px; margin-right: 17px;" />
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
                <div id="divESAName" runat="server" style="float: left; margin-left: 330px; margin-top: 50px;">
                    <span style="color: #40464C; font-size: 14px; font-weight: bold;">Save Report as :</span>
                    &nbsp;&nbsp;<asp:TextBox ID="txtESAReportName" runat="server" CssClass="AttrTxtFild"
                        Style="width: 200px!important; float: none!important" placeholder="Report Name" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=""
                        ControlToValidate="txtESAReportName" ErrorMessage="Please enter report Name"
                        ValidationGroup="addReport" ForeColor="Red" Text="*">
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlTgtValueGap" runat="server" >
                <div class="RightAt" id="div6" runat="server" visible="true" style="float: right;
                    margin-right: 10px; margin-top: 110px; width: 1030px !important; overflow-y: scroll;
                    max-height: 300px;">
                        <asp:GridView ID="gridTgtValueGap" runat="server" AlternatingRowStyle-CssClass="field_row bg_white"
                            CssClass="summry_table" AutoGenerateColumns="false"  
                            AllowSorting="true"  CellSpacing="0"
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
                    
                </div>
                <div id="div7" runat="server" style="float: left; margin-left: 330px; margin-top: 50px;">
                    <span style="color: #40464C; font-size: 14px; font-weight: bold;">Save Report as :</span>
                    &nbsp;&nbsp;<asp:TextBox ID="TextBox1" runat="server" CssClass="AttrTxtFild"
                        Style="width: 200px!important; float: none!important" placeholder="Report Name" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue=""
                        ControlToValidate="txtTVGReportName" ErrorMessage="Please enter report Name"
                        ValidationGroup="addReport" ForeColor="Red" Text="*">
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlListSavedReport" runat="server" Visible="false">
                <div class="RightAt" style="float: right; margin-right: 10px; margin-top: 120px;
                    width: 1030px !important;">
                    <asp:GridView ID="grdSavedReport" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="true" OnSorting="grdSavedReport_Sorting"
                        AllowSorting="true" OnPageIndexChanging="grdSavedReport_PageIndexChanging" OnRowEditing="grdSavedReport_RowEditing"
                        OnRowDeleting="grdSavedReport_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Report Name" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ReportName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Report Type" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%-- <%#Convert.ToString(Eval("ReportType")) == "1" ? "Attribute" : "BOM"%>--%>
                                    <%#DataBinder.Eval(Container.DataItem, "ReportTypeName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="GrayBg">
                                <ItemTemplate>
                                    <asp:Literal ID="litReportID" runat="server" Visible="false" Text='<%#Eval("ReportID") %>' />
                                    <asp:ImageButton ID="editBtn" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ReportID")%>'
                                        ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" CssClass="grdEditBtn" />
                                    <asp:ImageButton ID="deleteBtn" runat="server" CommandName="Delete" AlternateText="Delete"
                                        CssClass="grdDeleteBtn" ToolTip="Delete" CommandArgument='<%#Eval("ReportID") %>'
                                        ImageUrl="~/images/delete.png" />
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
