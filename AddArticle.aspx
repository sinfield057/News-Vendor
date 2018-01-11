<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddArticle.aspx.cs" Inherits="AddArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Add New Article</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="col-sm-6 col-sm-offset-3">
        <div class="panel panel-default panel-body ">
            
            <div class="form-group center-block">
                <asp:Label ID="LabelTitle" runat="server" Text="Title:"></asp:Label>
                <asp:TextBox runat="server" ID="ArticleTitle" class="form-control" placeholder="Title"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="ArticleTitleValidator" runat="server" CssClass="alert alert-danger" Display="Dynamic"
                    ControlToValidate="ArticleTitle" ErrorMessage="Article needs a title." ValidationGroup="ArticleValidationGroup"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group center-block">
                <asp:Label ID="LabelImage" runat="server" Text="Image Url (Optional):"></asp:Label>
                <asp:TextBox runat="server" ID="ArticleImage" class="form-control" placeholder="Image Url (Optional)"></asp:TextBox>
            </div>
            
            <div class="form-group center-block">
                <asp:DropDownList ID="ArticleCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ArticleCategories_SelectedIndexChanged">
                </asp:DropDownList>
                <span>...or type in a new one</span>
                <br />
                <asp:TextBox runat="server" ID="ArticleCategory" AutoPostBack="true" class="form-control" placeholder="Category" OnTextChanged="ArticleCategory_TextChanged"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="ArticleCategoryValidator" runat="server" CssClass="alert alert-danger" Display="Dynamic"
                    ControlToValidate="ArticleCategory" ErrorMessage="Please enter a category" ValidationGroup="ArticleValidationGroup"></asp:RequiredFieldValidator>
            </div>

            
            <div class="form-group center-block">
                <asp:Label ID="LabelBody" runat="server" Text="Body (if you wish to link to another website, leave only the url here):"></asp:Label>
                <asp:TextBox runat="server" ID="ArticleBody" class="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="ArticleBodyValidator" runat="server" Display="Dynamic" CssClass="alert alert-danger"
                    ControlToValidate="ArticleBody" ErrorMessage="Your article body is empty." ValidationGroup="ArticleValidationGroup"></asp:RequiredFieldValidator>
                <br />
            </div>
            
            <asp:Button ID="FinishArticleButton" OnClick="Finish_Article" runat="server" Text="Finish Article" CssClass="center-block"/>
            <asp:Label runat="server" ID="FinishArticleErrorLabel" CssClass="center-block text-center" ForeColor="#990000"></asp:Label>
        </div>
    </div>
</asp:Content>

