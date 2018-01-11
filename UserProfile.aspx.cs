using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !Page.IsPostBack )
        {
            if (Request.Cookies["NewsVendorCookie"] == null)
            {
                Response.Redirect("~/Index.aspx");
            }
            else
            {
                HttpCookie cookie = Request.Cookies["NewsVendorCookie"];
                int id = int.Parse(cookie["id"]);

                string query = "SELECT * FROM Users WHERE Id = @id";

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
                con.Open();

                try
                {
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = com.ExecuteReader();

                    reader.Read();
                    First_Name.Text = reader["First_Name"].ToString();
                    Last_Name.Text = reader["Last_Name"].ToString();

                }
                catch (Exception ex)
                {
                    SaveErrorLabel.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }

    protected void Save_Profile(object sender, EventArgs e)
    {
        if ( string.IsNullOrEmpty( OldPassword.Text ) )
        {
            SaveErrorLabel.Text = "You need to type in the current password in order to make changes.";
            return;
        }

        if ( !string.IsNullOrEmpty( NewPassword.Text ) )
        {
            if ( !NewPassword.Text.Equals( NewPasswordRepeat.Text ) )
            {
                SaveErrorLabel.Text = "Please retype the new password correctly.";
                return;
            }
        }

        HttpCookie cookie = Request.Cookies["NewsVendorCookie"];
        int id = int.Parse(cookie["id"]);
        string firstName = First_Name.Text;
        string lastName = Last_Name.Text;
        string oldPassword = OldPassword.Text;
        string newPassword = NewPassword.Text;

        string query = "UPDATE Users SET First_Name = @first_name, Last_Name = @last_name, Password = @password WHERE Id = @id AND Password = @oldpassword; SELECT * FROM Users WHERE Id = @id;";

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("first_name", firstName);
            com.Parameters.AddWithValue("last_name", lastName);
            com.Parameters.AddWithValue("password", string.IsNullOrEmpty(newPassword) ? oldPassword : newPassword);
            com.Parameters.AddWithValue("oldpassword", oldPassword);
            com.Parameters.AddWithValue("id", id);

            SqlDataReader reader = com.ExecuteReader();

            if ( reader.RecordsAffected != 0 )
            {

                reader.Read();
                HttpCookie newCookie = new HttpCookie("NewsVendorCookie");
                newCookie.Values.Add("email", reader["Email"].ToString());
                newCookie.Values.Add("role", reader["Role"].ToString());
                newCookie.Values.Add("id", reader["Id"].ToString());
                if (!string.IsNullOrEmpty(reader["First_Name"].ToString()))
                {
                    newCookie.Values.Add("firstName", reader["First_Name"].ToString());
                }
                newCookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Clear();
                Response.Cookies.Add(newCookie);
                Response.Redirect("~/Index.aspx");

            }
            else
            {
                SaveErrorLabel.Text = "Current password is incorrect.";
            }
        }
        catch( Exception ex )
        {
            SaveErrorLabel.Text = "Internal error. Please try again later.";
        }
        finally
        {
            con.Close();
        }

        return;

    }
}