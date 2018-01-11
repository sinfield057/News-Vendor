<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Profile Page</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <asp:Panel ID="p" runat="server" DefaultButton="SaveProfileButton">
        <div class="col-sm-6 col-sm-offset-3">
            <div class="panel panel-default panel-body ">
            
                <div class="panel panel-body">
                    <div class="form-group center-block">
                        <asp:TextBox runat="server" ID="First_Name" class="form-control" placeholder="First Name"></asp:TextBox>
                    </div>

                    <div class="form-group center-block">
                        <asp:TextBox runat="server" ID="Last_Name" class="form-control" placeholder="Last Name"></asp:TextBox>
                    </div>
                </div>
            
                <div class="panel panel-body">
                    <div class="form-group center-block">
                        <asp:TextBox runat="server" ID="NewPassword" class="form-control" placeholder="New Password" TextMode="Password"></asp:TextBox>
                    </div>

                    <div class="form-group center-block">
                        <asp:TextBox runat="server" ID="NewPasswordRepeat" class="form-control" placeholder="Repeat New Password" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
            
                <div class="form-group center-block">
                    <asp:TextBox runat="server" ID="OldPassword" class="form-control" placeholder="Type the current password to save changes" TextMode="Password"></asp:TextBox>
                    <br />
                </div>
            
                <asp:Button ID="SaveProfileButton" runat="server" OnClick="Save_Profile" Text="Save" CssClass="center-block"/>
                <asp:Label runat="server" ID="SaveErrorLabel" CssClass="center-block text-center" ForeColor="#990000"></asp:Label>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

