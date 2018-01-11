<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Manage Users</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <h1>Edit User Roles</h1>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="UserSource" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" DataKeyNames="Id"
        UseAccessibleHeader ="true" CssClass="table table-hover table-striped" GridLines="None">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/EditUser.aspx?id={0}" HeaderText="Edit User" Text="Edit" />
        </Columns>
    </asp:GridView>
    
    <asp:SqlDataSource runat="server" ID="UserSource" Connectionstring="<%$ ConnectionStrings: MyDb %>"
        SelectCommand="SELECT Users.Id, Users.Email, Users.Role FROM Users;">
    </asp:SqlDataSource>
</asp:Content>

