<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="FormManager.aspx.cs" Inherits="FormManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function test() {

            var contentHeight = $(window).height();
            var contentWidth = $(window).width();

            var NewWidth = contentWidth - $("#divSidebar").width() - 20;


            var newHeight = contentHeight - $("#header").height() - $("#footer").height() - $("#Title").height() - $("#Bpmn").height() - 20; // + "px";        
            var remainCnt = $("#header").height() + $("#footer").height() + 50;
            var newHeight1 = contentHeight - remainCnt;
            $(".TreeView1_0").css("height", newHeight1 + "px");


            $("#ContentPlaceHolder1_div2").css("width", NewWidth + "px");

        }

        function hideSuccessMsg() {

            window.setTimeout(function () {
                $(".isa_info").fadeOut('slow', function () { $('.isa_info').remove() });
                $(".isa_success").fadeOut('slow', function () { $('.isa_success').remove() });
                $(".isa_warning").fadeOut('slow', function () { $('.isa_warning').remove() });
                $(".isa_error").fadeOut('slow', function () { $('.isa_error').remove() });
            }, 7000);
        }


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


        function SetFocus(id, Controltype) {

            if (Controltype == 1) {
                
                var el = "#" + id.id + "";
                var nextElement = $(el).closest("td").next("td").find(".itemstyle").find("input");
                if (nextElement.length > 0) {
                    var nextElementId = $(el).closest("td").next("td").find("input").attr("id");
                    var nextElementId_el = "#" + nextElementId + "";
                    var NextElement_idSyntax = $(nextElementId_el);
                    var strLength = NextElement_idSyntax.val().length;
                    NextElement_idSyntax.focus();
                    NextElement_idSyntax[0].setSelectionRange(strLength, strLength);
                }
                else {
                    var nextElementId = $(el).closest("td").next("td").find("select").attr("id");
                    var nextElementId_el = "#" + nextElementId + "";
                    var NextElement_idSyntax = $(nextElementId_el);
                    NextElement_idSyntax.focus().select();
                }
            }
            else if (Controltype == 2) {
                
                var el = "#" + id.id + "";
                var nextElement = $(el).closest("td").next("td").find(".itemstyle").find("input");
                if (nextElement.length > 0) {
                    var nextElementId = $(el).closest("td").next("td").find("input").attr("id");
                    var nextElementId_el = "#" + nextElementId + "";
                    var NextElement_idSyntax = $(nextElementId_el);
                    var strLength = NextElement_idSyntax.val().length;
                    NextElement_idSyntax.focus();
                    NextElement_idSyntax[0].setSelectionRange(strLength, strLength);
                }
                else {
                    var nextElementId = $(el).closest("td").next("td").find("select").attr("id");
                    var nextElementId_el = "#" + nextElementId + "";
                    var NextElement_idSyntax = $(nextElementId_el);
                    NextElement_idSyntax.focus().select();
                }
            }


        }


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
        .RightAtExcel table th {
            font: bold 12px Arial, Helvetica, sans-serif;
            padding: 5px; /*0 15px 0 15px;*/
            text-align: center;
            height: auto;
        }

        .RightAtExcel table td {
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
                    <h1 id="headerTitle" runat="server">Error Flow</h1>
                    <div id="divErrorMsg" runat="server" visible="false" style="font: bold 12px Arial, Helvetica, sans-serif; color: #555; padding: 8px 0px 0px 6.000px; height: 30px; float: right; margin-left: 0; margin-right: 600px; width: 290px;">
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="right_nav">
                        <ul id="ulTopmenus" runat="server" class="ulPDSA">
                            <li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnSaveForm" runat="server" Text="Save Record" CssClass="DesignBtn"
                                    ValidationGroup="addForm" OnClick="lnkbtnSaveForm_Click" Visible="true" />
                            </li>

                            <li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnViewPPESAForm" runat="server" ToolTip="View Process Error Flow" CssClass="ViewPPESA"
                                    OnClick="lnkbtnViewPPESAForm_Click" />
                            </li>
                            <li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnViewPDESAForm" runat="server" ToolTip="View Product Error Flow" CssClass="ViewPPESA"
                                    OnClick="lnkbtnViewPDESAForm_Click" />
                            </li>

                            <%--<li style="margin-right: 1px">
                                <asp:LinkButton ID="lnkbtnErrorRecord" runat="server" ToolTip="Error Record"
                                    CssClass="ViewErrorLog"
                                    OnClick="lnkbtnErrorRecord_Click" />
                            </li>--%>
                        </ul>
                    </div>
                </div>
            </div>


            <asp:Panel ID="pnlListPPESA" runat="server" Visible="false">
                <div class="RightAtExcel" id="div2" runat="server" visible="true" style="float: right; margin-right: 10px; margin-top: 110px; overflow-y: scroll; max-height: 400px;">

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
                                        <asp:DropDownList ID="ddlProcessObjectID" AutoPostBack="true" OnSelectedIndexChanged="txtProductFeatureAdded_TextChanged" runat="server" CssClass="AttrSeFildExcel">
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
                                        <asp:TextBox ID="txtProductFeatureAdded" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" runat="server" Text='<%# Eval("ProductFeatureAdded") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 136px; margin-right: 0px;" />

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Function of Product Feature" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtFunctionProductFeature" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" runat="server" Text='<%# Eval("FunctionofProductFeature") %>'
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
                                        <asp:TextBox ID="txtErrorEvent" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" runat="server" Text='<%# Eval("ErrorEvent") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Error Event Transfer Function" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtErrorEventTransferFunction" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" runat="server" Text='<%# Eval("ErrorEventTransferFunction") %>'
                                            CssClass="AttrTxtFildExcel" Style="width: 136px; margin-right: 0px;" />

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtActions" runat="server" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" Text='<%# Eval("Actions") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action Critical Parameter" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:DropDownList ID="drpActionCriticalParameter" AutoPostBack="true" OnSelectedIndexChanged="txtProductFeatureAdded_TextChanged" runat="server" CssClass="AttrSeFildExcel">
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
                                        <asp:TextBox ID="txtConditions" runat="server" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" Text='<%# Eval("Conditions") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conditon Critical Parameter" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:DropDownList ID="drpConditonCriticalParameter" AutoPostBack="true" OnSelectedIndexChanged="txtProductFeatureAdded_TextChanged" runat="server" CssClass="AttrSeFildExcel">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="InitialSeverity" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="true" OnSelectedIndexChanged="txtProductFeatureAdded_TextChanged" ID="ddlInitialSeverity"
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

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Initial Frequency" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="true" OnSelectedIndexChanged="txtProductFeatureAdded_TextChanged" ID="ddlInitialFrequency"
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

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Initial Detection" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>

                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="true" OnSelectedIndexChanged="txtProductFeatureAdded_TextChanged" ID="ddlInitialDetection"
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

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Intial RPN" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtIntialRPN" runat="server" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" Text='<%# Eval("IntialRPN") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Countermeasure" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>

                                    <asp:TextBox ID="txtCountermeasure" runat="server" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" Text='<%# Eval("Countermeasure") %>'
                                        CssClass="AttrTxtFildExcel" Style="width: 136px; margin-right: 0px;" />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Countermeasure Effectiveness" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>

                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="true" OnSelectedIndexChanged="txtProductFeatureAdded_TextChanged" ID="ddlCountermeasureEffectiveness"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="1" />
                                        <asp:ListItem Value="2" Text="2" />
                                        <asp:ListItem Value="3" Text="3" />
                                        <asp:ListItem Value="4" Text="4" />
                                        <asp:ListItem Value="5" Text="5" />
                                    </asp:DropDownList>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Severity" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>

                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="true" OnSelectedIndexChanged="txtProductFeatureAdded_TextChanged" ID="ddlFinalSeverity"
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

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Frequency" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>

                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" ID="ddlFinalFrequency"
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

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Detection" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>

                                    <asp:DropDownList class="AttrSeFildExcel" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" ID="ddlFinalDetection"
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

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final RPN" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <div class="itemstyle">
                                        <asp:TextBox ID="txtFinalRPN" runat="server" AutoPostBack="true" OnTextChanged="txtProductFeatureAdded_TextChanged" Text='<%# Eval("FinalRPN") %>' CssClass="AttrTxtFildExcel"
                                            Style="width: 136px; margin-right: 0px;" />

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div>
                                &nbsp;
                            </div>
                            <div class="msgSucess12">
                                <p>
                                    No records found !
                               
                                </p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="RightAt" id="div1" runat="server" visible="true" style="float: left; margin-top: 10px; margin-left: 330px;">
                    <asp:Button ID="btnAddNewRow" runat="server" CssClass="btnNextNew" Text="Add New Row"
                        OnClick="btnAddNewRow_Click" Style="width: 127px; margin: 10px 0px 0px 0; float: left; font-size: 11px; line-height: 14px; height: 24px;" />
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

