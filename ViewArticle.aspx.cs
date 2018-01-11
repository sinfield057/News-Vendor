using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewArticle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !Page.IsPostBack )
        {
            Load_Stuff();
        }
       
    }

    private void Load_Stuff()
    {
        if (string.IsNullOrEmpty(Request.Params["id"]))
        {
            Response.Redirect("~/Index.aspx");
            return;
        }

        ArticleImage.Attributes.Add("style", "float: left; margin-right: 10px;");

        string id = Request.Params["id"].ToString();
        string query = "SELECT articles.Id as id, Articles.Title as title, Articles.Content as content, Articles.Date_Created as date, Articles.Category as category, Articles.ImageLink as image, Users.First_Name as fname, Users.Last_Name as lname FROM " +
            "Articles JOIN Users ON Articles.Author = Users.Id " +
            "WHERE Articles.Id = @id;";

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("id", id.ToString());
            SqlDataReader reader = com.ExecuteReader();

            reader.Read();

            if (new UrlCheck().checkUrl(reader["content"].ToString()))
            {
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:goToPage('" + reader["content"].ToString() + "');", true);
                Response.Redirect("~/Index.aspx");
            }

            ArticleTitle.Text = reader["title"].ToString();
            ArticleBody.Text = reader["content"].ToString();
            ArticleAuthor.Text = reader["fname"].ToString() + " " + reader["lname"];
            ArticleDate.Text = reader["date"].ToString();
            ArticleImage.ImageUrl = reader["image"].ToString();

            string commentsId = reader["id"].ToString();
            string queryComments = "SELECT Comments.Comment as comment, Comments.Date_Created as date, Users.First_Name + ' ' + Users.Last_Name as username, Users.Email as email " +
                "FROM Comments JOIN Users ON Comments.UserId = Users.Id WHERE Comments.Article = @id ORDER BY CONVERT(DateTime, Comments.Date_Created, 101) DESC;";
            SqlConnection conComments = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
            conComments.Open();

            try
            {
                SqlCommand comComments = new SqlCommand(queryComments, conComments);
                comComments.Parameters.AddWithValue("id", commentsId);
                SqlDataReader readerComments = comComments.ExecuteReader();

                CommentsListView.DataSource = readerComments;
                CommentsListView.DataBind();

            }
            catch (Exception ex)
            {
                IndexErrorLabel.Text = "Failed to load comments. Try again later." + ex.Message;
            }
            finally
            {
                conComments.Close();
            }
        }
        catch (Exception ex)
        {
            IndexErrorLabel.Text = "Failed to load article. Try again later. ";
        }
        finally
        {
            con.Close();
        }
    }

    protected void Finish_Comment(object sender, EventArgs e)
    {
        if (!(Request.Cookies["NewsVendorCookie"] != null && new Bitmask().checkBasic(int.Parse(Request.Cookies.Get("NewsVendorCookie")["role"]))))
        {
            IndexErrorLabel.Text = "You are not allowed to post comments.";
            return;
        }

        if (string.IsNullOrEmpty(CommentBody.Text))
        {
            IndexErrorLabel.Text = "Cannot post an empty comment.";
            return;
        }

        string authorId = Request.Cookies.Get("NewsVendorCookie")["id"];
        string articleId = Request.Params["id"].ToString();

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
        string query = "INSERT INTO Comments ( Article, UserId, Comment ) VALUES ( @article, @userid, @comment );";
        SqlCommand com = new SqlCommand(query, con);

        con.Open();

        try
        {
            com.Parameters.AddWithValue("article", articleId);
            com.Parameters.AddWithValue("userid", authorId);
            com.Parameters.AddWithValue("comment", CommentBody.Text);

            string readerInsert = com.ExecuteScalar().ToString();

            if (!string.IsNullOrEmpty(readerInsert))
            {
                IndexErrorLabel.Text = "Comment added successfully.";
                
            }
            else
            {
                IndexErrorLabel.Text = "Comment could not be added.";
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
            Response.Redirect(Request.RawUrl);
        }
    }
}