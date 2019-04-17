<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dummy.aspx.cs" Inherits="dummy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<style type="text/css">
        .style1
        {
            width: 80%;
        }
        .ModalPopupBG
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .HellowWorldPopup
        {
            min-width: 200px;
            min-height: 150px;
            background: white;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
    <div>
       
    
        <table align="center" class="style1">
        <tr>
        <td class="th_fieldsi left">
                                            <asp:LinkButton ID="lnkbtn" runat="server" >Attributes</asp:LinkButton>
                                            </td>
        </tr>
            <tr>
               
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Button" style="display:none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="ModalPopupBG"
                        PopupControlID="up1" TargetControlID="Button1" BehaviorID="ModalPopupExtender1"/>
                    
                </td>
            </tr>
            <tr>
                <td>
                  <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
      <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="HellowWorldPopup">
                       
                                 <asp:TextBox ID="txtex" runat="server" ValidationGroup="addValue" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqddlUnits" runat="server"  InitialValue="" ValidationGroup="addValue" ControlToValidate="txtex" ErrorMessage="*" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:Button ID="btn1" runat="server"  ValidationGroup="addValue" Text="Submit" />
                        </div>
                    </asp:Panel>
                        </ContentTemplate>
        </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                     
                </td>
            </tr>
            <tr>
                <td>
                     
                </td>
            </tr>
        </table>
    
    </div>
    
    </form>
</body>
</html>
