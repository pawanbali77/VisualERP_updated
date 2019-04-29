<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="FormManager.aspx.cs" Inherits="FormManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function test() {
            
            var contentHeight = $(window).height();
            var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 20; // + "px";        
            var remainCnt = $("#header").height() + $("#footer").height() + 50;
            var newHeight1 = contentHeight - remainCnt;
            $(".TreeView1_0").css("height", newHeight1 + "px");

        }

        function hideSuccessMsg() {
            
            window.setTimeout(function () {
                $(".isa_info").fadeOut('slow', function () { $('.isa_info').remove() });
                $(".isa_success").fadeOut('slow', function () { $('.isa_success').remove() });
                $(".isa_warning").fadeOut('slow', function () { $('.isa_warning').remove() });
                $(".isa_error").fadeOut('slow', function () { $('.isa_error').remove() });
            }, 7000);
        }
    
    </script>
    <script type="text/javascript" language="javascript">
        function calculateInitialSeverity(drpInitialSeverity, txtIntialRPN, drpInitialFrequency, drpInitialDetection) {

            var InitialSeverity = parseInt($(drpInitialSeverity).val(), 0);
            var InitialFrequency = parseInt($("#" + drpInitialFrequency + "").val(), 0);
            var InitialDetection = parseInt($("#" + drpInitialDetection + "").val(), 0);
            var result = InitialSeverity * InitialFrequency * InitialDetection;
            if (result == 0)
                return;
            $("#" + txtIntialRPN + "").val(result);
        }

        function calculateInitialFrequency(drpInitialFrequency, txtIntialRPN, drpInitialSeverity, drpInitialDetection) {

            var InitialSeverity = parseInt($("#" + drpInitialSeverity + "").val(), 0);
            var InitialFrequency = parseInt($(drpInitialFrequency).val(), 0);
            var InitialDetection = parseInt($("#" + drpInitialDetection + "").val(), 0);
            var result = InitialSeverity * InitialFrequency * InitialDetection;
            if (result == 0)
                return;
            $("#" + txtIntialRPN + "").val(result);
        }

        function calculateInitialDetection(drpInitialDetection, txtIntialRPN, drpInitialSeverity, drpInitialFrequency) {

            var InitialSeverity = parseInt($("#" + drpInitialSeverity + "").val(), 0);
            var InitialFrequency = parseInt($("#" + drpInitialFrequency + "").val(), 0);
            var InitialDetection = parseInt($(drpInitialDetection).val(), 0);
            var result = InitialSeverity * InitialFrequency * InitialDetection;
            if (result == 0)
                return;
            $("#" + txtIntialRPN + "").val(result);
        }

        function calculateFinalSeverity(drpFinalSeverity, txtFinalRPN, drpFinalFrequency, drpFinalDetection) {

            var FinalSeverity = parseInt($(drpFinalSeverity).val(), 0);
            var FinalFrequency = parseInt($("#" + drpFinalFrequency + "").val(), 0);
            var FinalDetection = parseInt($("#" + drpFinalDetection + "").val(), 0);
            var result = FinalSeverity * FinalFrequency * FinalDetection;
            if (result == 0)
                return;
            $("#" + txtFinalRPN + "").val(result);
        }

        function calculateFinalFrequency(drpFinalFrequency, txtFinalRPN, drpFinalSeverity, drpFinalDetection) {

            var FinalSeverity = parseInt($("#" + drpFinalSeverity + "").val(), 0);
            var FinalFrequency = parseInt($(drpFinalFrequency).val(), 0);
            var FinalDetection = parseInt($("#" + drpFinalDetection + "").val(), 0);
            var result = FinalSeverity * FinalFrequency * FinalDetection;
            if (result == 0)
                return;
            $("#" + txtFinalRPN + "").val(result);
        }

        function calculateFinalDetection(drpFinalDetection, txtFinalRPN, drpFinalSeverity, drpFinalFrequency) {

            var FinalSeverity = parseInt($("#" + drpFinalSeverity + "").val(), 0);
            var FinalFrequency = parseInt($("#" + drpFinalFrequency + "").val(), 0);
            var FinalDetection = parseInt($(drpFinalDetection).val(), 0);
            var result = FinalSeverity * FinalFrequency * FinalDetection;
            if (result == 0)
                return;
            $("#" + txtFinalRPN + "").val(result);
        }
    </script>
    
    <script type="text/javascript" language="javascript">
        function actvieclassByid(ele) {
            //alert(ele);
            $('ContentPlaceHolder1_lnkbtnAddPPESAForm').removeClass('active');
            $('ContentPlaceHolder1_lnkbtnViewPPESAForm').removeClass('active');
            $('ContentPlaceHolder1_lnkbtnViewPDESAForm').removeClass('active');
            $(ele).addClass('active');
            //$('#' + ele).css("class", "active");
        }

        function actvieclass(ele) {
            //alert(ele);
            $('#ContentPlaceHolder1_lnkbtnAddPPESAForm').removeClass('active');
            $('#ContentPlaceHolder1_lnkbtnViewPPESAForm').removeClass('active');
            $('#ContentPlaceHolder1_lnkbtnViewPDESAForm').removeClass('active');
            $('#' + ele.id).addClass('active');
            //$('#' + ele).css("class", "active");
        }
    </script>
    <style type="text/css">
        .RightAtExcel table th
        {
            font: bold 12px Arial, Helvetica, sans-serif;
            padding: 5px; /*0 15px 0 15px;*/
            text-align: center;
            height: auto;
        }
        
        .RightAtExcel table td
        {
            font: normal 12px Arial, Helvetica, sans-serif;
            text-align: center;
            padding: 4px 4px 4px 4px; /* 12px 15px 12px 15px;*/
        }
        /* .itemstyle
        {
            width: 150px;
            overflow: hidden;
            white-space: nowrap;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container">
                <div class="right_container_top" id="Title">
                    <h1 id="headerTitle" runat="server">
                        Form Manager</h1>
                    <div id="divErrorMsg" runat="server" visible="false" style="font: bold 12px Arial, Helvetica, sans-serif;
                        color: #555; padding: 8px 0px 0px 60px; height: 30px; float: right; margin-left: 0;
                        margin-right: 600px; width: 290px;">
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="right_nav">
                        <ul id="ulTopmenus" runat="server" class="ulPDSA">
                            <li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnSaveForm" runat="server" Text="Save Record" CssClass="DesignBtn"
                                    ValidationGroup="addForm" OnClick="lnkbtnSaveForm_Click" Visible="true" />
                            </li>
                            <%-- <li id="li1" runat="server" style="height: 44px; margin-top: 6px;" onclick="return AddPPESA();">
                                <a title="Add PPESA" class="PPESAIcon"></a></li>
                            <li id="liPPESA" runat="server" style="height: 44px; margin-top: 6px;" onclick="return AddPDESA();">
                                <a title="Add PDESA" class="PPESAIcon"></a></li>--%>
                            <%--<li style="margin-right: 1px" id="liAddFormP" runat="server">
                                <asp:LinkButton ID="lnkbtnAddPPESAForm" runat="server" ToolTip="Add PPESA" CssClass="AddPPESA"
                                    Visible="false" OnClick="lnkbtnAddPPESAForm_Click" />
                            </li>--%>
                            <%--<li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnAddPDESAForm" runat="server" ToolTip="Add PDESA" CssClass="AddPPESA"
                                    OnClick="lnkbtnAddPDESAForm_Click" />
                            </li>--%>
                            <li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnViewPPESAForm" runat="server" ToolTip="View PPESA" CssClass="ViewPPESA"
                                    OnClick="lnkbtnViewPPESAForm_Click" />
                            </li>
                            <li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnViewPDESAForm" runat="server" ToolTip="View PDESA" CssClass="ViewPPESA"
                                    OnClick="lnkbtnViewPDESAForm_Click" />
                            </li>

                               <li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnErrorRecord" runat="server" ToolTip="Error Record" 
                                    CssClass="ViewPPESA"
                                    OnClick="lnkbtnErrorRecord_Click" />
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <%--<asp:Panel ID="pnlActivity" runat="server" Visible="false">
                <div class="ActivityPopupTop" id="divAttribute" runat="server">
                    <div class="ActivityPopup">
                        <h2 style="font: bold 15px/32px Arial,Helvetica,sans-serif!important">
                            <asp:Literal ID="ltrActivity" runat="server" Text="Select Activity" /></h2>
                        <div class="ActivitybMid">
                            <div class="LeftAtActivity">
                                <ul class="ActivtyNew">
                                    <li>
                                        <div class="fixheight" style="height: 150px; margin-top: 15px; overflow-y: scroll;
                                            overflow-x: hidden;">
                                            <asp:RadioButtonList ID="radiobtnlistActivity" runat="server" />
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
            </asp:Panel>--%>
            <%--<asp:Panel ID="pnlAddForm" runat="server" Visible="false">
                <div class="RightAt1" id="divBom" runat="server" style="float: left; margin: 100px 0px 0px 340px;
                    width: 1000px; overflow: auto;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 20px">
                        <tr>
                            <td>
                                <label>
                                    Product Feature Added</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProductFeatureAdded" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="txtProductFeatureAdded" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Style="width: 100%;
                                    float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                    runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                    ControlToValidate="txtProductFeatureAdded" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                Function of Product Feature
                            </td>
                            <td>
                                <asp:TextBox ID="txtFunctionProductFeature" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="txtFunctionProductFeature" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="width: 100%;
                                    float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                    runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                    ControlToValidate="txtFunctionProductFeature" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Error Event
                            </td>
                            <td>
                                <asp:TextBox ID="txtErrorEvent" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="txtErrorEvent" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Style="width: 100%;
                                    float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                    runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                    ControlToValidate="txtErrorEvent" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                Error Event Transfer Function
                            </td>
                            <td>
                                <asp:TextBox ID="txtErrorEventTransferFunction" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="txtErrorEventTransferFunction" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Style="width: 100%;
                                    float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                    runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                    ControlToValidate="txtErrorEventTransferFunction" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Actions
                            </td>
                            <td>
                                <asp:TextBox ID="txtActions" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="txtActions" ErrorMessage="*" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Style="width: 100%;
                                    float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                    runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                    ControlToValidate="txtActions" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <label>
                                    Action Critical Parameter</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpActionCriticalParameter" runat="server" CssClass="AttrSeFildExcel">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                    <asp:ListItem Value="False">No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Conditions
                            </td>
                            <td>
                                <asp:TextBox ID="txtConditions" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="txtConditions" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Conditon Critical Parameter
                            </td>
                            <td>
                                <asp:DropDownList ID="drpConditonCriticalParameter" runat="server" CssClass="AttrSeFildExcel">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                    <asp:ListItem Value="False">No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Initial Severity
                            </td>
                            <td>
                                <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlInitialSeverity"
                                    runat="server">
                                    <asp:ListItem Value="0" Text="--Select--" />
                                    <asp:ListItem Value="1" Text="1" />
                                    <asp:ListItem Value="2" Text="2" />
                                    <asp:ListItem Value="3" Text="3" />
                                    <asp:ListItem Value="4" Text="4" />
                                    <asp:ListItem Value="5" Text="5" />
                                    <asp:ListItem Value="6" Text="6" />
                                    <asp:ListItem Value="7" Text="7" />
                                    <asp:ListItem Value="8" Text="8" />
                                    <asp:ListItem Value="9" Text="9" />
                                    <asp:ListItem Value="10" Text="10" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="ddlInitialSeverity" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>                               
                            </td>
                            <td>
                                Initial Frequency
                            </td>
                            <td>
                                <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlInitialFrequency"
                                    runat="server">
                                    <asp:ListItem Value="0" Text="--Select--" />
                                    <asp:ListItem Value="1" Text="1" />
                                    <asp:ListItem Value="2" Text="2" />
                                    <asp:ListItem Value="3" Text="3" />
                                    <asp:ListItem Value="4" Text="4" />
                                    <asp:ListItem Value="5" Text="5" />
                                    <asp:ListItem Value="6" Text="6" />
                                    <asp:ListItem Value="7" Text="7" />
                                    <asp:ListItem Value="8" Text="8" />
                                    <asp:ListItem Value="9" Text="9" />
                                    <asp:ListItem Value="10" Text="10" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="ddlInitialFrequency" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Initial Detection
                            </td>
                            <td>
                                <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlInitialDetection"
                                    runat="server">
                                    <asp:ListItem Value="0" Text="--Select--" />
                                    <asp:ListItem Value="1" Text="1" />
                                    <asp:ListItem Value="2" Text="2" />
                                    <asp:ListItem Value="3" Text="3" />
                                    <asp:ListItem Value="4" Text="4" />
                                    <asp:ListItem Value="5" Text="5" />
                                    <asp:ListItem Value="6" Text="6" />
                                    <asp:ListItem Value="7" Text="7" />
                                    <asp:ListItem Value="8" Text="8" />
                                    <asp:ListItem Value="9" Text="9" />
                                    <asp:ListItem Value="10" Text="10" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="ddlInitialDetection" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Intial RPN
                            </td>
                            <td>
                                <asp:TextBox ID="txtIntialRPN1" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="txtIntialRPN1" ErrorMessage="*" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtIntialRPN"
                                    ValidChars="0123456789" BehaviorID="txtIntialRPN1">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Countermeasure
                            </td>
                            <td>
                                <asp:TextBox ID="txtCountermeasure" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="txtCountermeasure" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Style="width: 100%;
                                    float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                    runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                    ControlToValidate="txtCountermeasure" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                Countermeasure Effectiveness
                            </td>
                            <td>
                                <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlCountermeasureEffectiveness"
                                    runat="server">
                                    <asp:ListItem Value="0" Text="--Select--" />
                                    <asp:ListItem Value="1" Text="1" />
                                    <asp:ListItem Value="2" Text="2" />
                                    <asp:ListItem Value="3" Text="3" />
                                    <asp:ListItem Value="4" Text="4" />
                                    <asp:ListItem Value="5" Text="5" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="ddlCountermeasureEffectiveness"
                                    ErrorMessage="*" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Final Severity
                            </td>
                            <td>
                                <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlFinalSeverity" runat="server">
                                    <asp:ListItem Value="0" Text="--Select--" />
                                    <asp:ListItem Value="1" Text="1" />
                                    <asp:ListItem Value="2" Text="2" />
                                    <asp:ListItem Value="3" Text="3" />
                                    <asp:ListItem Value="4" Text="4" />
                                    <asp:ListItem Value="5" Text="5" />
                                    <asp:ListItem Value="6" Text="6" />
                                    <asp:ListItem Value="7" Text="7" />
                                    <asp:ListItem Value="8" Text="8" />
                                    <asp:ListItem Value="9" Text="9" />
                                    <asp:ListItem Value="10" Text="10" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="ddlFinalSeverity" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Final Frequency
                            </td>
                            <td>
                                <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlFinalFrequency"
                                    runat="server">
                                    <asp:ListItem Value="0" Text="--Select--" />
                                    <asp:ListItem Value="1" Text="1" />
                                    <asp:ListItem Value="2" Text="2" />
                                    <asp:ListItem Value="3" Text="3" />
                                    <asp:ListItem Value="4" Text="4" />
                                    <asp:ListItem Value="5" Text="5" />
                                    <asp:ListItem Value="6" Text="6" />
                                    <asp:ListItem Value="7" Text="7" />
                                    <asp:ListItem Value="8" Text="8" />
                                    <asp:ListItem Value="9" Text="9" />
                                    <asp:ListItem Value="10" Text="10" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="ddlFinalFrequency" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Final Detection
                            </td>
                            <td>
                                <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlFinalDetection"
                                    runat="server">
                                    <asp:ListItem Value="0" Text="--Select--" />
                                    <asp:ListItem Value="1" Text="1" />
                                    <asp:ListItem Value="2" Text="2" />
                                    <asp:ListItem Value="3" Text="3" />
                                    <asp:ListItem Value="4" Text="4" />
                                    <asp:ListItem Value="5" Text="5" />
                                    <asp:ListItem Value="6" Text="6" />
                                    <asp:ListItem Value="7" Text="7" />
                                    <asp:ListItem Value="8" Text="8" />
                                    <asp:ListItem Value="9" Text="9" />
                                    <asp:ListItem Value="10" Text="10" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue=""
                                    ValidationGroup="addForm" ControlToValidate="ddlFinalDetection" ErrorMessage="*"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <td>
                                    Final RPN
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFinalRPN1" runat="server" CssClass="AttrTxtFildExcel"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="txtFinalRPN1" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtFinalRPN"
                                        ValidChars="0123456789" BehaviorID="txtFinalRPN1">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="hdnFormType" runat="server" />
            </asp:Panel>--%>
            <asp:Panel ID="pnlListPPESA" runat="server" Visible="false">
                <div class="RightAtExcel" id="div2" runat="server" visible="true" style="float: right;
                    margin-right: 10px; margin-top: 110px; width: 1030px !important; overflow-y: scroll;
                    max-height: 400px;">
                    <asp:GridView ID="gridPPESA" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="false" OnSorting="gridPPESA_Sorting"
                        OnSelectedIndexChanging="gridPPESA_SelectedIndexChanging" AllowSorting="true"
                        OnPageIndexChanging="gridPPESA_PageIndexChanging" OnRowDeleting="gridPPESA_RowDeleting"
                        OnRowCreated="gridPPESA_RowCreated" OnRowDataBound="gridPPESA_RowDataBound" OnRowCommand="gridPPESA_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Insert Row">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:Literal ID="litSequence" runat="server" Visible="false" Text='<%#Eval("Sequence") %>' />
                                        <asp:ImageButton ID="imgbtnAddrow" runat="server" CommandName="add" AlternateText="Add Row"
                                            ToolTip="Add Row" CommandArgument='<%#Eval("Sequence") %>' ImageUrl="~/images/plus.png" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete Row" ItemStyle-CssClass="GrayBg">
                                <ItemTemplate>
                                    <asp:Literal ID="litFormID" runat="server" Visible="false" Text='<%#Eval("FormID") %>' />
                                    <asp:Literal ID="litpoid" runat="server" Visible="false" Text='<%#Eval("ProcessObjectID") %>' />
                                    <asp:Literal ID="litFormType" runat="server" Visible="false" Text='<%#Eval("FormType") %>' />
                                    <asp:Literal ID="litDelSequence" runat="server" Visible="false" Text='<%#Eval("Sequence") %>' />
                                    <%--<asp:ImageButton ID="editBtn" runat="server" CommandName="Edit" CommandArgument='<%#Eval("FormID")%>'
                                        ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />--%>
                                    <asp:ImageButton ID="deleteBtn" runat="server" CommandName="Delete" AlternateText="Delete"
                                        ToolTip="Delete" CommandArgument='<%#Eval("FormID") %>' ImageUrl="~/images/delete.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <%--<%#Container.DataItemIndex+1 %>--%>
                                        <%# Eval("Sequence") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Process Object Name" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:DropDownList ID="ddlProcessObjectID" runat="server" CssClass="AttrSeFildExcel">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" InitialValue="--Select Activity--"
                                            ValidationGroup="addForm" ControlToValidate="ddlProcessObjectID" ErrorMessage="Select Activity"
                                            ForeColor="Red" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Feature Added" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtProductFeatureAdded" runat="server" Text='<%# Eval("ProductFeatureAdded") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 136px; margin-right: 0px;" />
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue=""
                                            ValidationGroup="addForm" ControlToValidate="txtProductFeatureAdded" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Style="width: 100%;
                                            float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                            runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                            ControlToValidate="txtProductFeatureAdded" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Function of Product Feature" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtFunctionProductFeature" runat="server" Text='<%# Eval("FunctionofProductFeature") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 136px; margin-right: 0px;" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                            ValidationGroup="addForm" ControlToValidate="txtFunctionProductFeature" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="width: 100%;
                                            float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                            runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                            ControlToValidate="txtFunctionProductFeature" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Error Event" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtErrorEvent" runat="server" Text='<%# Eval("ErrorEvent") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" InitialValue=""
                                            ValidationGroup="addForm" ControlToValidate="txtErrorEvent" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Style="width: 100%;
                                            float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                            runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                            ControlToValidate="txtErrorEvent" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Error Event Transfer Function" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtErrorEventTransferFunction" runat="server" Text='<%# Eval("ErrorEventTransferFunction") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 136px; margin-right: 0px;" />
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue=""
                                            ValidationGroup="addForm" ControlToValidate="txtErrorEventTransferFunction" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Style="width: 100%;
                                            float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                            runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                            ControlToValidate="txtErrorEventTransferFunction" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtActions" runat="server" Text='<%# Eval("Actions") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                            ValidationGroup="addForm" ControlToValidate="txtActions" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Style="width: 100%;
                                            float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                            runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                            ControlToValidate="txtActions" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action Critical Parameter" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:DropDownList ID="drpActionCriticalParameter" runat="server" CssClass="AttrSeFildExcel">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conditions" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtConditions" runat="server" Text='<%# Eval("Conditions") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue=""
                                            ValidationGroup="addForm" ControlToValidate="txtConditions" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conditon Critical Parameter" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:DropDownList ID="drpConditonCriticalParameter" runat="server" CssClass="AttrSeFildExcel">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="InitialSeverity" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlInitialSeverity"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="1" />
                                        <asp:ListItem Value="2" Text="2" />
                                        <asp:ListItem Value="3" Text="3" />
                                        <asp:ListItem Value="4" Text="4" />
                                        <asp:ListItem Value="5" Text="5" />
                                        <asp:ListItem Value="6" Text="6" />
                                        <asp:ListItem Value="7" Text="7" />
                                        <asp:ListItem Value="8" Text="8" />
                                        <asp:ListItem Value="9" Text="9" />
                                        <asp:ListItem Value="10" Text="10" />
                                    </asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="ddlInitialSeverity" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Initial Frequency" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlInitialFrequency"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="1" />
                                        <asp:ListItem Value="2" Text="2" />
                                        <asp:ListItem Value="3" Text="3" />
                                        <asp:ListItem Value="4" Text="4" />
                                        <asp:ListItem Value="5" Text="5" />
                                        <asp:ListItem Value="6" Text="6" />
                                        <asp:ListItem Value="7" Text="7" />
                                        <asp:ListItem Value="8" Text="8" />
                                        <asp:ListItem Value="9" Text="9" />
                                        <asp:ListItem Value="10" Text="10" />
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="ddlInitialFrequency" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Initial Detection" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "InitialDetection")%>--%>
                                    <%-- <asp:TextBox ID="txtInitialDetection" runat="server"  Text='<%# Eval("InitialDetection") %>'
                                        CssClass="AttrTxtFildExcel" Style="width: 120px; margin-right: 17px;" />--%>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlInitialDetection"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="1" />
                                        <asp:ListItem Value="2" Text="2" />
                                        <asp:ListItem Value="3" Text="3" />
                                        <asp:ListItem Value="4" Text="4" />
                                        <asp:ListItem Value="5" Text="5" />
                                        <asp:ListItem Value="6" Text="6" />
                                        <asp:ListItem Value="7" Text="7" />
                                        <asp:ListItem Value="8" Text="8" />
                                        <asp:ListItem Value="9" Text="9" />
                                        <asp:ListItem Value="10" Text="10" />
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="ddlInitialDetection" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Intial RPN" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtIntialRPN" runat="server" Text='<%# Eval("IntialRPN") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                            ValidationGroup="addForm" ControlToValidate="txtIntialRPN" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtIntialRPN"
                                            ValidChars="0123456789" BehaviorID="txtIntialRPN">
                                        </asp:FilteredTextBoxExtender>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Countermeasure" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "Countermeasure")%>--%>
                                    <asp:TextBox ID="txtCountermeasure" runat="server" Text='<%# Eval("Countermeasure") %>'
                                        CssClass="AttrTxtFildExcel" Style="width: 136px; margin-right: 0px;" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="txtCountermeasure" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Style="width: 100%;
                                        float: left;" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"
                                        runat="server" ErrorMessage="Special symbols not allowed" ValidationGroup="addForm"
                                        ControlToValidate="txtCountermeasure" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Countermeasure Effectiveness" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%--<%#DataBinder.Eval(Container.DataItem, "CountermeasureEffectiveness")%>--%>
                                    <%--<asp:TextBox ID="txtCountermeasureEffectiveness" runat="server" 
                                        Text='<%# Eval("CountermeasureEffectiveness") %>' CssClass="AttrTxtFildExcel" Style="width: 120px;
                                        margin-right: 17px;" />--%>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlCountermeasureEffectiveness"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="1" />
                                        <asp:ListItem Value="2" Text="2" />
                                        <asp:ListItem Value="3" Text="3" />
                                        <asp:ListItem Value="4" Text="4" />
                                        <asp:ListItem Value="5" Text="5" />
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="ddlCountermeasureEffectiveness"
                                        ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Severity" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "FinalSeverity")%>--%>
                                    <%--<asp:TextBox ID="txtFinalSeverity" runat="server"  Text='<%# Eval("FinalSeverity") %>'
                                        CssClass="AttrTxtFildExcel" Style="width: 120px; margin-right: 17px;" />--%>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlFinalSeverity"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="1" />
                                        <asp:ListItem Value="2" Text="2" />
                                        <asp:ListItem Value="3" Text="3" />
                                        <asp:ListItem Value="4" Text="4" />
                                        <asp:ListItem Value="5" Text="5" />
                                        <asp:ListItem Value="6" Text="6" />
                                        <asp:ListItem Value="7" Text="7" />
                                        <asp:ListItem Value="8" Text="8" />
                                        <asp:ListItem Value="9" Text="9" />
                                        <asp:ListItem Value="10" Text="10" />
                                    </asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="ddlFinalSeverity" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Frequency" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%-- <%#DataBinder.Eval(Container.DataItem, "FinalFrequency")%>--%>
                                    <%-- <asp:TextBox ID="txtFinalFrequency" runat="server"  Text='<%# Eval("FinalFrequency") %>'
                                        CssClass="AttrTxtFildExcel" Style="width: 120px; margin-right: 17px;" />--%>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlFinalFrequency"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="1" />
                                        <asp:ListItem Value="2" Text="2" />
                                        <asp:ListItem Value="3" Text="3" />
                                        <asp:ListItem Value="4" Text="4" />
                                        <asp:ListItem Value="5" Text="5" />
                                        <asp:ListItem Value="6" Text="6" />
                                        <asp:ListItem Value="7" Text="7" />
                                        <asp:ListItem Value="8" Text="8" />
                                        <asp:ListItem Value="9" Text="9" />
                                        <asp:ListItem Value="10" Text="10" />
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="ddlFinalFrequency" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Detection" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%--<%#DataBinder.Eval(Container.DataItem, "FinalDetection")%>--%>
                                    <%-- <asp:TextBox ID="txtFinalDetection" runat="server"  Text='<%# Eval("FinalDetection") %>'
                                        CssClass="AttrTxtFildExcel" Style="width: 120px; margin-right: 17px;" />--%>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="false" ID="ddlFinalDetection"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="1" />
                                        <asp:ListItem Value="2" Text="2" />
                                        <asp:ListItem Value="3" Text="3" />
                                        <asp:ListItem Value="4" Text="4" />
                                        <asp:ListItem Value="5" Text="5" />
                                        <asp:ListItem Value="6" Text="6" />
                                        <asp:ListItem Value="7" Text="7" />
                                        <asp:ListItem Value="8" Text="8" />
                                        <asp:ListItem Value="9" Text="9" />
                                        <asp:ListItem Value="10" Text="10" />
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue=""
                                        ValidationGroup="addForm" ControlToValidate="ddlFinalDetection" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final RPN" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtFinalRPN" runat="server" Text='<%# Eval("FinalRPN") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue=""
                                            ValidationGroup="addForm" ControlToValidate="txtFinalRPN" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtFinalRPN"
                                            ValidChars="0123456789" BehaviorID="txtFinalRPN">
                                        </asp:FilteredTextBoxExtender>
                                        --%>
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
                <div class="RightAt" id="div1" runat="server" visible="true" style="float: left;
                    margin-top: 10px; margin-left: 330px;">
                    <asp:Button ID="btnAddNewRow" runat="server" CssClass="btnNextNew" Text="Add New Row"
                        OnClick="btnAddNewRow_Click" Style="width: 127px; margin: 10px 0px 0px 0; float: left;
                        font-size: 11px; line-height: 14px; height: 24px;" />
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlListErrorRecord" runat="server" Visible="false">
                <div class="RightAtExcel" id="div3" runat="server" visible="true" style="float: right;
                    margin-right: 10px; margin-top: 110px; width: 1030px !important; overflow-y: scroll;
                    max-height: 400px;">
                    <asp:GridView ID="grdErrorGrid" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="false"  
                        OnRowCommand="grdErrorGrid_RowCommand"
                        AllowSorting="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Insert Row">
                               <ItemTemplate>
                                    <div class="itemstyle">
                                        
                                        <asp:ImageButton ID="imgbtnAddrow" runat="server" CommandName="Add" AlternateText="Add Row"
                                            ToolTip="Add Row" CommandArgument="0" ImageUrl="~/images/plus.png" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete Row" ItemStyle-CssClass="GrayBg">
                                <ItemTemplate>
                                    <asp:Literal ID="litFormID" runat="server" Visible="false" Text='<%#Eval("ErrorID") %>' />
                                    <asp:Literal ID="litpoid" runat="server" Visible="false" Text='<%#Eval("ErrorID") %>' />
                                    <asp:Literal ID="litFormType" runat="server" Visible="false" Text='<%#Eval("ErrorID") %>' />

                                    <asp:HiddenField ID="hdnErrorID" Value='<%#Eval("ErrorID") %>' runat="server" />
                                      <asp:HiddenField ID="hdnProcessID" Value='<%#Eval("ProcessID") %>' runat="server" />
                                   <%-- <asp:Literal ID="litDelSequence" runat="server" Visible="false" Text='<%#Eval("Sequence") %>' />--%>
                                    <%--<asp:ImageButton ID="editBtn" runat="server" CommandName="Edit" CommandArgument='<%#Eval("FormID")%>'
                                        ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />--%>
                                    <asp:ImageButton ID="deleteBtn" runat="server" CommandName="Remove" AlternateText="Delete"
                                        ToolTip="Delete" CommandArgument='<%#Eval("ErrorID") %>' ImageUrl="~/images/delete.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Error" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtError" runat="server" Text='<%# Eval("Error") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 200px; margin-right: 0px;" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cycle Time" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtCycleTime" runat="server" Text='<%# Eval("CycleTime") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 50px; margin-right: 0px;" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Content" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtWorkContent" runat="server" Text='<%# Eval("WorkContent") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 136px; margin-right: 0px;" />
                                       
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Counter Measure" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtCounterMeasure" runat="server" Text='<%# Eval("CounterMeasure") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 100px; margin-right: 0px;" />
                                        
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CounterMeasure Strength" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtCounterMeasureStrength" runat="server" Text='<%# Eval("CounterMeasureStrength") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 100px; margin-right: 0px;" />
                                        
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
                <div class="RightAt" id="div4" runat="server" visible="true" style="float: left;
                    margin-top: 10px; margin-left: 330px;">
                    <asp:Button ID="btnAddNewErrorRecord" runat="server" CssClass="btnNextNew" Text="Add New Row"
                        OnClick="btnAddNewErrorRecord_Click" Style="width: 127px; margin: 10px 0px 0px 0; float: left;
                        font-size: 11px; line-height: 14px; height: 24px;" />
                </div>
            </asp:Panel>

        </ContentTemplate>



    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="AjaxLoaderOuter123">
                <div class="AjaxLoaderInner123">
                    <img src='<%= ResolveUrl("~/images/ajax-loading11.gif") %>' alt="Wait..." style="background-color: Transparent;" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

    
</asp:Content>

