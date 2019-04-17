<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="BOMReport.aspx.cs" Inherits="BOMReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container_top TopMarg" id="Title">
                <h1>
                    Report Manager
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <%-- <div id="divRecordCount" runat="server" style="font: bold 14px Arial, Helvetica, sans-serif;
                                color: #555; padding: 0px 0px 0px 135px; float: right; width: 300px; margin-top: -19px;
                                display: none">
                                (Total Records: <span style="color: #EF705B">
                                    <asp:Literal ID="ltrTotalRecord" runat="server" /></span>)</div>--%>
                            <div id="divRecordCount" runat="server" style="font: bold 12px Arial, Helvetica, sans-serif;
                                color: #555; padding: 0px 0px 0px 530px; float: right; width: 900px; margin-top: -5px;">
                                <asp:Literal ID="ltrTotalRecord" runat="server" />
                                <asp:Label ID="lblMsg" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </h1>
                <%--<div style="width: 100%; float: left; margin-top: 2px; height: 29px; margin-left: -3px;" id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" style="padding: 0px 13px 9px !important;"></asp:Label></div>--%>
                <div class="right_nav">
                    <ul>
                        <li>
                            <%--<asp:UpdatePanel ID="updExport" runat="server">
                        <ContentTemplate>--%>
                            <asp:LinkButton ID="lnkbtnSaveReport" runat="server" Text="Save Report" CssClass="DesignBtn"
                                OnClick="lnkbtnSaveReport_Click" ValidationGroup="addReport"></asp:LinkButton>
                            <%--<asp:LinkButton ID="lnkbtnExporttoExcel" runat="server" Text="Export to Excel" CssClass="DesignBtn" />--%>
                            <%--</ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkbtnExporttoExcel" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                        </li>
                        <li style="line-height: 27px; margin-top: 4px;">
                            <asp:LinkButton ID="lnkbtnList" runat="server" ToolTip="Saved Report" CssClass="iconList" OnClick="lnkbtnList_Click"></asp:LinkButton>
                        </li>
                        <li style="line-height: 27px; margin-top: 4px;">
                            <asp:LinkButton ID="lnkbtnAdd" runat="server" ToolTip="New Report" CssClass="iconAdd" OnClick="lnkbtnAdd_Click"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
            <asp:Panel ID="PanelReportType" runat="server" Visible="false">
                <div class="ActivityPopupTop" id="div1" runat="server">
                    <div class="ActivityPopup">
                        <h2 style="font: bold 15px/32px Arial,Helvetica,sans-serif!important">
                            Select Report Type</h2>
                        <div class="ActivitybMid" style="min-height:50px!important">
                            <div class="LeftAtActivity">
                                <ul class="ActivtyNew">                                
                                    <li><asp:LinkButton ID="lnkBtnAttributeReport" OnClick="lnkBtnAttributeReport_Click" runat="server" Text="Attribute Report" style="font: bold 13px/32px Arial,Helvetica,sans-serif; color:#555555; margin-left: 17px;" /></li>
                                    <li><asp:LinkButton ID="lnkBtnBOMReport" OnClick="lnkBtnBOMReport_Click" runat="server" Text="BOM Report" style="font: bold 13px/32px Arial,Helvetica,sans-serif; color:#555555; margin-left: 17px;" /></li>
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
                <div class="ActivityPopupTop" id="divAttribute" runat="server">
                    <div class="ActivityPopup">
                        <h2 style="font: bold 15px/32px Arial,Helvetica,sans-serif!important">
                            Select Activity</h2>
                        <div class="ActivitybMid">
                            <div class="LeftAtActivity">
                                <ul class="ActivtyNew">
                                    <li>
                                        <div class="fixheight" style="height: 150px; margin-top: 15px; overflow-y: scroll;
                                            overflow-x: hidden;">
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
                                            <asp:CheckBoxList ID="chkboxBomProcess" CssClass="checkbox" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                        <%-- <asp:CheckBoxList ID="chkboxBomProcess" runat="server" Style="margin-left: 10px">
                                        </asp:CheckBoxList>--%>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <asp:Button ID="btnNextToBomProcess" Text="Next" runat="server" style="margin:-38px 90px 0 0!important;" CssClass="btnNextNew" OnClick="btnNextToBomProcess_Click" />
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlReport" runat="server" Visible="false">
                <div class="RightAt" id="divReport" runat="server" visible="true" style="overflow-y: scroll;
                    max-height: 400px; float: right; margin-top: 99px; width: 1047px!important; margin-right: 5px;">
                    <asp:GridView ID="grdBomReport" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Description")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BOMLevel" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "BOMLevel")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BOMRevision" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "BOMRevision")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="weight" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "weight")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "UOM")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StandardCost" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "StandardCost")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="WorkingCost" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "WorkingCost")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StdPackQty" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "StdPackQty")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxPackLength" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MaxPackLength")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxPackWidth" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MaxPackWidth")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxPackHeight" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MaxPackHeight")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ContainerQty" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ContainerQty")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MedianRelinishmentLT" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MedianRelinishmentLT")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MinRLT" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MinRLT")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxRLT" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MaxRLT")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rolling12MnthUsage" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Rolling12MnthUsage")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AvgMonthUsage" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "AvgMonthUsage")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RiskFactor" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "RiskFactor")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MonthStdDevRiskFactor" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "MonthStdDevRiskFactor")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="KanbanQty" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "KanbanQty")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="InService" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "InService")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In_ServiceDate" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "In_ServiceDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ObsolescenceDate" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ObsolescenceDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OnHandInventory" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "OnHandInventory")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OnOrder" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "OnOrder")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NextShipmentDue" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "NextShipmentDue")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NextQtyDue" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "NextQtyDue")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PartReqNxtPerd" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "PartReqNxtPerd")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CurrentPurchasingOwner" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CurrentPurchasingOwner")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CurrentDesignOwner" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "CurrentDesignOwner")%>
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
                <div style="float: left; margin-left: 330px; margin-top: 50px;">
                    <span style="color: #40464C; font-size: 14px; font-weight: bold;">Save Report as :</span>
                    &nbsp;&nbsp;<asp:TextBox ID="txtReportName" runat="server" CssClass="AttrTxtFild"
                        Style="width: 200px!important; float: none!important" placeholder="Report Name" />
                    <asp:RequiredFieldValidator ID="reqtxtAttrivalue" runat="server" InitialValue=""
                        ValidationGroup="addReport" ControlToValidate="txtReportName" ErrorMessage="Please enter report Name"
                        ForeColor="Red" Text="*">
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
