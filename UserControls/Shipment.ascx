<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Shipment.ascx.cs" Inherits="UserControls_Shipment" %>


<style type="text/css">
        .Shipment
        {
            background-image: url("images/Controls/External_Shipment.png")!important;
            background-repeat:no-repeat;
            width: 200px;  /*130px;*/
            height: 200px;  /*130px;*/
            padding: 0.5em;            
            color: #0000;
            position:absolute;
            background-size: 100% 100%; 
            padding-top:85px;
            padding-left:37px;
        }
    </style>
    <asp:LinkButton ID="lnkbtnDeleteShipment" runat="server" CssClass="ControlCloseSmall" 
    onclick="lnkbtnDeleteShipment_Click1"></asp:LinkButton>
<div class="Shipment" id="divShipment" runat="server"></div>
