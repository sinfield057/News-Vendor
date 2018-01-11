using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddArticle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Request.Cookies["NewsVendorCookie"] != null && new Bitmask().checkEditor(int.Parse(Request.Cookies.Get("NewsVendorCookie")["role"]))))
        {
            Response.Redirect("~/Index.aspx");
        }

        if (!this.IsPostBack)
        {
           SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
           SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Categories;", con );

            con.Open();

            try
            {
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                ArticleCategories.DataSource = dt;
                ArticleCategories.DataTextField = "Name";
                ArticleCategories.DataValueField = "Name";
                ArticleCategories.DataBind();

                ArticleCategories.Items.Insert(0, new ListItem("-- Select Category --", "0"));

            }
            catch(Exception ex)
            {
                FinishArticleErrorLabel.Text = "Internal Error";
            }
            finally
            {
                con.Close();
            }
        }
    }

    protected void Finish_Article(object sender, EventArgs e)
    {
        if (!(Request.Cookies["NewsVendorCookie"] != null && new Bitmask().checkEditor(int.Parse(Request.Cookies.Get("NewsVendorCookie")["role"]))))
        {
            FinishArticleErrorLabel.Text = "You are not allowed to do make changes.";
            return;
        }

        Page.Validate();
        if (!Page.IsValid)
        {
            FinishArticleErrorLabel.Text = "Could not finish article. Some fields are invalid.";
            return;
        }

        string category = ArticleCategory.Text;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
        string query = "IF NOT EXISTS ( Select * FROM Categories WHERE Name = @name ) INSERT INTO Categories ( Name ) VALUES ( @name ); SELECT * FROM Categories WHERE Name = @name;";
        SqlCommand com = new SqlCommand(query, con);

        con.Open();

        try
        {
            com.Parameters.AddWithValue("name", category);
            SqlDataReader reader = com.ExecuteReader();

            reader.Read();
            string categoryId = reader["Id"].ToString();

            SqlConnection conInsert = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
            string queryInsert = "INSERT INTO Articles ( Title, Content, Author, Category, ImageLink ) OUTPUT INSERTED.ID VALUES (  @title, @content, @author, @category, @imagelink );";
            SqlCommand comInsert = new SqlCommand(queryInsert, conInsert);

            conInsert.Open();

            try
            {
                comInsert.Parameters.AddWithValue("title", ArticleTitle.Text);
                comInsert.Parameters.AddWithValue("content", ArticleBody.Text.Replace("\r\n", "<br />") );
                comInsert.Parameters.AddWithValue("author", Request.Cookies.Get("NewsVendorCookie")["id"]);
                comInsert.Parameters.AddWithValue("category", categoryId);
                comInsert.Parameters.AddWithValue("imagelink", ArticleImage.Text );

                string readerInsert = comInsert.ExecuteScalar().ToString();

                if (!string.IsNullOrEmpty(readerInsert) )
                {
                    Response.Redirect("~/ViewArticle.aspx?id=" + readerInsert );
                }
                else
                {
                    FinishArticleErrorLabel.Text = "Article could not be added.";
                }
            }
            catch(Exception ex)
            {
                FinishArticleErrorLabel.Text = "Internal error 2. Please try again later. " + ex.Message;
            }
            finally
            {
                conInsert.Close();
            }

        }
        catch (Exception ex)
        {
            FinishArticleErrorLabel.Text = "Internal error. Please try again later.";
        }
        finally
        {
            con.Close();
        }
    }

    protected void ArticleCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ArticleCategories.SelectedValue != "0")
        {
            ArticleCategory.Text = ArticleCategories.SelectedValue;
        }
        else
        {
            ArticleCategory.Text = "";
        }
    }

    protected void ArticleCategory_TextChanged(object sender, EventArgs e)
    {
        ArticleCategories.SelectedValue = "0";
    }
}