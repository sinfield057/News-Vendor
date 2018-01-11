<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Sign Up For News Vendor</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="col-sm-6 col-sm-offset-3">
        <div class="panel panel-default panel-body ">
            
            <div class="form-group center-block">
                <asp:TextBox runat="server" ID="SignUpEmail" class="form-control" placeholder="Username"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="SignUpEmailValidator" runat="server" CssClass="alert alert-danger" Display="Dynamic"
                    ControlToValidate="SignUpEmail" ErrorMessage="Please enter email" ValidationGroup="SignUpValidationGroup"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="SignUpEmailRegularExpressionValidator" runat="server" CssClass="alert alert-danger"
                    ControlToValidate="SignUpEmail" ErrorMessage="Please use a valid email address." Display="Dynamic"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="SignUpValidationGroup"></asp:RegularExpressionValidator>
                <br />
            </div>
            
            <div class="form-group center-block">
                <asp:TextBox runat="server" ID="SignUpPassword" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="SignUpPasswordValidator" runat="server" CssClass="alert alert-danger" Display="Dynamic"
                    ControlToValidate="SignUpPassword" ErrorMessage="Please enter a password" ValidationGroup="SignUpValidationGroup"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="SignUpPasswordRegularExpressionValidator" runat="server" CssClass="alert alert-danger"
                    ControlToValidate="SignUpPassword" ErrorMessage="Your password must be ay least 6 alphanumeric characters long." Display="Dynamic"
                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$" ValidationGroup="SignUpValidationGroup"></asp:RegularExpressionValidator>
                <br />
            </div>
            
            <div class="form-group center-block">
                <asp:TextBox runat="server" ID="SignUpPasswordRepeat" class="form-control" placeholder="Repeat Password" TextMode="Password"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="SignUpPasswordRepeatValidator" runat="server" Display="Dynamic" CssClass="alert alert-danger"
                    ControlToValidate="SignUpPasswordRepeat" ErrorMessage="Please confirm your password." ValidationGroup="SignUpValidationGroup"></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="SignUpPasswordRepeatCompareValidator" ControlToCompare="SignUpPassword" ControlToValidate="SignUpPasswordRepeat"
                    ErrorMessage="Passwords do not match." ValidationGroup="SignUpValidationGroup" CssClass="alert alert-danger" Display="Dynamic"></asp:CompareValidator> 
                <br />
            </div>
            
            <asp:Button ID="SignUpButton" OnClick="Sign_Up" runat="server" Text="Sign Up" CssClass="center-block"/>
            <asp:Label runat="server" ID="SignUpErrorLabel" CssClass="center-block text-center" ForeColor="#990000"></asp:Label>
        </div>
    </div>
</asp:Content>

