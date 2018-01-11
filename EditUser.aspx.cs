using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!(Request.Cookies["NewsVendorCookie"] != null && new Bitmask().checkAdmin(int.Parse(Request.Cookies.Get("NewsVendorCookie")["role"])))
                || string.IsNullOrEmpty( Request.Params["id"]) )
            {
                Response.Redirect("~/Index.aspx");
            }
            else
            {
                int id = int.Parse(Request.Params["id"].ToString());
                string query = "SELECT * FROM Users WHERE Id = @id;";

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
                con.Open();

                try
                {
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = com.ExecuteReader();

                    reader.Read();
                    UserEmail.Text = "Edit role for " + reader["Email"].ToString();
                    int role = int.Parse(reader["role"].ToString());

                    Bitmask bitmask = new Bitmask();

                    if ( bitmask.checkBasic( role ) )
                    {
                        BasicUser.SelectedValue = "1";
                    }
                    else
                    {
                        BasicUser.SelectedValue = "0";
                    }

                    if (bitmask.checkEditor(role))
                    {
                        EditorUser.SelectedValue = "1";
                    }
                    else
                    {
                        EditorUser.SelectedValue = "0";
                    }

                    if (bitmask.checkAdmin(role))
                    {
                        AdminUser.SelectedValue = "1";
                    }
                    else
                    {
                        AdminUser.SelectedValue = "0";
                    }


                }
                catch (Exception ex)
                {
                    EditErrorLabel.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }

    protected void Save_Edit( object sender, EventArgs e )
    {
        if (!(Request.Cookies["NewsVendorCookie"] != null && new Bitmask().checkAdmin(int.Parse(Request.Cookies.Get("NewsVendorCookie")["role"]))))
        {
            EditErrorLabel.Text = "You are not allowed to do make changes.";
        }
        else
        {
            int id = int.Parse(Request.Params["id"].ToString());
            string query = "UPDATE Users SET Role = @role WHERE Id = @id";
            int role = 0;
            Bitmask bitmask = new Bitmask();

            if ( BasicUser.SelectedValue == "1" )
            {
                role = bitmask.makeBasic(role);
            }
            else
            {
                role = bitmask.removeBasic(role);
            }

            if (EditorUser.SelectedValue == "1")
            {
                role = bitmask.makeEditor(role);
            }
            else
            {
                role = bitmask.removeEditor(role);
            }

            if (AdminUser.SelectedValue == "1")
            {
                role = bitmask.makeAdmin(role);
            }
            else
            {
                role = bitmask.removeAdmin(role);
            }

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
            con.Open();

            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("id", id);
                com.Parameters.AddWithValue("role", role.ToString());

                SqlDataReader reader = com.ExecuteReader();

                if (reader.RecordsAffected != 0)
                {
                    EditErrorLabel.Text = "User roles updated.";
                }
                else
                {
                    EditErrorLabel.Text = "Nothing changed.";
                }
            }
            catch (Exception ex)
            {
                EditErrorLabel.Text = "Internal error. Please try again later.";
            }
            finally
            {
                con.Close();
            }
        }
    }
}