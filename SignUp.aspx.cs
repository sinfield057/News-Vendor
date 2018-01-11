using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["NewsVendorCookie"] != null)
        {
            Response.Redirect("~/Index.aspx");
        }
    }


    protected void Sign_Up(object sender, EventArgs e)
    {
        Page.Validate();
        if ( Page.IsValid )
        {
            string signUpEmail = SignUpEmail.Text.ToString();
            string signUpPassword = SignUpPassword.Text.ToString();

            string query = "IF NOT EXISTS ( Select * FROM Users WHERE Email = @email ) INSERT INTO Users ( Email, Password ) VALUES ( @email, @password ); SELECT * FROM Users WHERE Email = @email;";
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("email", signUpEmail);
                com.Parameters.AddWithValue("password", signUpPassword);
                SqlDataReader reader = com.ExecuteReader();

                if ( reader.RecordsAffected == -1 )
                {
                    SignUpErrorLabel.Text = "Email " + signUpEmail + " is already in use.";
                }
                else
                {
                    reader.Read();
                    HttpCookie cookie = new HttpCookie("NewsVendorCookie");
                    cookie.Values.Add("email", reader["Email"].ToString());
                    cookie.Values.Add("role", reader["Role"].ToString());
                    cookie.Values.Add("id", reader["Id"].ToString());
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Add(cookie);
                    Response.Redirect("~/Index.aspx");
                }
            }
            catch( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine(ex);
                SignUpErrorLabel.Text = "Internal error. Please try again later.";
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            SignUpErrorLabel.Text = "Invalid fields.";
        }
    }
}