<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelpVideos.aspx.cs" Inherits="HelpVideos" MasterPageFile="~/FrontMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        td {
            text-align: center;
            border: 1px solid black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
    <table style="width: 70%; border: 1px solid black; margin: 0 auto; padding: 0;">
        <tr style="background: black; color: white;">
            <th>Video's Name</th>
            <th>Video</th>
        </tr>
        <tr>
            <td>Registration</td>
            <td>
               
                <video width="320" height="240" controls="controls">
                    <source src="HelpVideos/Registration.mp4" type="video/mp4" />
                </video>
            </td>
        </tr>
        <tr>

            <td>Forget Password
            </td>
            <td>
                <video width="320" height="240" controls="controls" title="Forget Password">
                    <source src="HelpVideos/ForgetPassword.mp4" type="video/mp4" />
                </video>
            </td>
        </tr>
        <tr>
            <td>User's Registration</td>
            <td>
                <video width="320" height="240" controls="controls">
                    <source src="HelpVideos/UserRegistration.mp4" type="video/mp4" />
                </video>
            </td>
        </tr>
    </table>
        </div>
   </asp:Content>

