<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Supplier.ascx.cs" Inherits="UserControls_Supplier" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
        .Supplier
        {
            background-image:url("images/Controls/Customer_Supplier.png")!important;
            background-repeat:no-repeat;
            width: 200px; /*135*/
            height: 200px;  /*125*/
            padding: 0.5em;
           /* background: #FF5555;*/
            color: #0000;
            position:absolute;
            float: left;
            font: 12px Arial, Helvetica, sans-serif;
            text-align: center;
            padding-top:100px; /*45px;*/
            background-size: 100% 100%; 
        }
    </style>
    
   <asp:LinkButton ID="lnkbtnDeleteSupplier" runat="server" CssClass="ControlCloseSmall"
    onclick="lnkbtnDeleteSupplier_Click1"></asp:LinkButton>
    <div class="Supplier" id="divSupplier" runat="server"></div>
    
