<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
    <div class="right_container">
        <%--<div class="right_top"></div>--%>
     
   
       
   
           
        <div class="Clear">
        </div>
    </div>
   
</asp:Content>

