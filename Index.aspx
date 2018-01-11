<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>News Vendor</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="container-fluid" style="max-width: 80%">
        <asp:Label runat="server" ID="ArticlesAbout" CssClass="h2"></asp:Label>
        <br />
        <br />
        <asp:ListView ID="ListView1" runat="server">
            <ItemTemplate>
                    <div class="row"  runat="server">
                        <asp:HyperLink ID="LinkToArticle" runat="server" NavigateUrl='<%# new UrlCheck().checkUrl(Eval("content").ToString()) ? Eval("content").ToString() : "~/ViewArticle.aspx?id=" + Eval("id")%>' >
                            <div class="col-sm-4">
                                <asp:Image ID="ArticleImage" runat="server" ImageUrl='<%# Eval("image").ToString() %>' CssClass="img-thumbnail float-left" onerror="this.style.display='none'; this.classList.remove('img-thumbnail');" />
                            </div>
                            <div class="col-sm-8" runat="server">
                                <h3><asp:Label ID="Title" runat="server" Text='<%# Eval("title") %>' /></h3>
                        
                                <p>
                                    <asp:Label ID="Author" runat="server" Text='<%# "by " + Eval("author").ToString() %>' CssClass="text-muted" />
                                </p>
                        
                                <p>
                                    <asp:Label ID="Category" runat="server" Text='<%# Eval("catname").ToString() %>' CssClass="badge badge-default"/>
                                    <asp:Label ID="Date" runat="server" Text='<%# Eval("date").ToString() %>' />
                                </p>
                        
                                <p>
                                    <asp:Label ID="Content" runat="server" Text='<%# Eval("content").ToString().Length <= 1000 ? Eval("content").ToString() : Eval("content").ToString().Substring(0, 1000) %>' CssClass="ShortDesc" />
                                </p>      
                        
                                
                            </div>
                        </asp:HyperLink>
                    </div>
                    <hr />
            </ItemTemplate>
            <EmptyDataTemplate>
                <p>No articles.</p>
            </EmptyDataTemplate>
            <LayoutTemplate>
                    <div class="span4" id="itemPlaceholder" runat="server">
                    
                    </div>
            </LayoutTemplate>
        </asp:ListView>

        <div class="span4" runat="server">
            <asp:Label ID="IndexErrorLabel" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>

