using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            SqlConnection conSelect = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
            SqlCommand cmdSelect = new SqlCommand("SELECT Id, Name FROM Categories;", conSelect);

            conSelect.Open();

            try
            {
                DataTable dt = new DataTable();
                dt.Load(cmdSelect.ExecuteReader());

                ArticleCategoriesIndex.DataSource = dt;
                ArticleCategoriesIndex.DataTextField = "Name";
                ArticleCategoriesIndex.DataValueField = "Name";
                ArticleCategoriesIndex.DataBind();

                ArticleCategoriesIndex.Items.Insert(0, new ListItem("Select Category", ""));
                
            }
            catch (Exception ex)
            {
                ArticleCategoriesIndex.Visible = false;
            }
            finally
            {
                conSelect.Close();
            }
        }
    }

    protected void Sign_In(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(email.Text) && !string.IsNullOrEmpty(password.Text))
        {
            string formEmail = email.Text.ToString();
            string formPassword = password.Text.ToString();

            string query = "SELECT * FROM Users WHERE Email = @email AND Password = @password";
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("email", formEmail);
                com.Parameters.AddWithValue("password", formPassword);

                // Se executa comanda si se returneaza valorile intr-un reader
                SqlDataReader reader = com.ExecuteReader();
                HttpCookie cookie = new HttpCookie("NewsVendorCookie");
                if (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine(reader);
                    cookie.Values.Add("email", reader["Email"].ToString());
                    cookie.Values.Add("role", reader["Role"].ToString());
                    cookie.Values.Add("id", reader["Id"].ToString());
                    if ( !string.IsNullOrEmpty( reader["First_Name"].ToString() ))
                    {
                        cookie.Values.Add("firstName", reader["First_Name"].ToString());
                    }
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Add(cookie);
                    Response.Redirect(Request.RawUrl);

                }
                else
                {
                    EroareBazaDate.Text = "Invalid Email or Password";
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }

        }

    }

    protected void Sign_Out(object sender, EventArgs e)
    {
        Response.Cookies["NewsVendorCookie"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect(Request.RawUrl);
    }

    protected void ArticleCategoriesIndex_IndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx?category=" + ArticleCategoriesIndex.SelectedValue);
    }

    protected void Search_Article(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx?search=" + SearchTextBox.Text);
    }
}