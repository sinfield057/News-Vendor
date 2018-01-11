<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewArticle.aspx.cs" Inherits="ViewArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Article</title>
    <script type="text/javascript">
        function goToPage( url ){
            var win = window.open(url, '_blank');
            win.focus();
         };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-lg-8">
                <asp:Label ID="ArticleTitle" runat="server" Text="" CssClass="h1 mt-4"></asp:Label>
                <p class="lead">
                    by
                    <asp:Label ID="ArticleAuthor" runat="server" Text=""></asp:Label>
                </p>

                <hr />
                <p>
                    Posted on <asp:Label ID="ArticleDate" runat="server" Text=""></asp:Label>
                </p>
                
                <hr />
                <asp:Image ID="ArticleImage" runat="server" ImageUrl="" CssClass="img-thumbnail img-rounded custom-left" onerror="this.style.display='none'" />
                <asp:Label ID="ArticleBody" runat="server" Text=""></asp:Label>
                <br />

                <asp:Label ID="Test" runat="server" ></asp:Label>
            </div>
        </div>
    </div>
    <br />
    <hr />
    <br />
    
        <div class="container">
            <div class="row">
                <div class="container-fluid" style="max-width: 80%">
                    <asp:ListView ID="CommentsListView" runat="server">
                        <ItemTemplate>
                                <div class="row"  runat="server">
                                    <div class="col-sm-4 text-muted">
                                        <h4><asp:Label ID="UserName" runat="server" Text='<%# string.IsNullOrEmpty( Eval("username").ToString() ) ? Eval("email") : Eval("username") %>' /></h4>
                                        <span><asp:Label ID="DateTime" runat="server" Text='<%# Eval("date") %>' /></span>
                                    </div>

                                    <div class="col-sm-8">
                                        <p>
                                            <asp:Label ID="CommentBody" runat="server" Text='<%# Eval("comment") %>' />
                                        </p>
                                    </div>
                                </div>
                                <hr />
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <p>No comments for this article.</p>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                                <div class="span4" id="itemPlaceholder" runat="server">
                    
                                </div>
                        </LayoutTemplate>
                    </asp:ListView>

                    <div class="form-group center-block">
                        <asp:Label ID="CommentBodyLabel" runat="server" Text="Enter a comment:"></asp:Label>
                        <asp:TextBox runat="server" ID="CommentBody" class="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>
            
                    <asp:Button ID="FinishCommentButton" OnClick="Finish_Comment" runat="server" Text="Post Comment" CssClass="center-block btn btn-default"/>
                
                    <br />
                    <br />

                    <div class="span4" runat="server">
                        <asp:Label ID="IndexErrorLabel" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    
</asp:Content>

