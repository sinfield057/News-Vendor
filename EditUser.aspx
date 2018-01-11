<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditUser.aspx.cs" Inherits="EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Edit User</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <asp:Panel ID="p" runat="server" DefaultButton="SaveEditButton">
        <br />
        <div class="container">
            <asp:Label ID="UserEmail" runat="server" Text="default" CssClass="h1 text-muted mx-auto"></asp:Label>
        </div>
        <br />
        <div class="container">
                <h3>Allow user to post comments:</h3>
                <asp:RadioButtonList ID="BasicUser" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="row">
                    <asp:ListItem runat="server" Text="Yes" Value="1" ID="BasicYes" class="col-md-1" />
                    <asp:ListItem runat="server" Text="No" Value="0" ID="BasicNo" class="col-md-1" />
                </asp:RadioButtonList>
   
            <br />
                <h3>Allow user to post articles:</h3>
                <asp:RadioButtonList ID="EditorUser" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="row">
                    <asp:ListItem runat="server" Text="Yes" Value="1" ID="EditorYes" class="col-md-1" />
                    <asp:ListItem runat="server" Text="No" Value="0" ID="EditorNo" class="col-md-1" />
                </asp:RadioButtonList>
   
            <br />
                <h3>Give user admin rights:</h3>
                <asp:RadioButtonList ID="AdminUser" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="row">
                    <asp:ListItem runat="server" Text="Yes" Value="1" ID="AdminYes" class="col-md-1" />
                    <asp:ListItem runat="server" Text="No" Value="0" ID="AdminNo" class="col-md-1" />
                </asp:RadioButtonList>
        </div>
        
        <div class="container">
            <asp:Button runat="server" ID="SaveEditButton" OnClick="Save_Edit" CssClass="btn btn-default" Text="Save Changes" />
            <asp:Label ID="EditErrorLabel" runat="server"></asp:Label>
        </div>

    </asp:Panel>
</asp:Content>

