using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string whereClause = "";

            if (string.IsNullOrEmpty(Request.Params["category"]))
            {
                if (string.IsNullOrEmpty(Request.Params["search"]))
                {
                    ArticlesAbout.Text = "All News";
                }
                else
                {
                    ArticlesAbout.Text = "Articles found for '" + Request.Params["search"].ToString() + "'";
                    whereClause = "WHERE Articles.Title LIKE '%' + @newcatname + '%' ";
                }
            }
            else
            {
                ArticlesAbout.Text = Request.Params["category"];
                whereClause = "WHERE Categories.Name = @newcatname ";
            }

            string query = "SELECT Articles.Id as id, Articles.Title as title, Articles.Content as content, Articles.Date_Created as date, Articles.Category as category, Articles.ImageLink as image, " +
                "Users.First_Name + ' ' + Users.Last_Name as author, Categories.Id as catid, Categories.Name as catname FROM " +
                "Articles JOIN Users ON Articles.Author = Users.Id " +
                "JOIN Categories ON Articles.Category = Categories.Id " +
                whereClause +
                "ORDER BY CONVERT(DateTime, Articles.Date_Created, 101) DESC;";

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString);
            con.Open();

            try
            {
                SqlCommand com = new SqlCommand(query, con);

                if (!string.IsNullOrEmpty(Request.Params["category"]))
                {
                    com.Parameters.AddWithValue("newcatname", Request.Params["category"]);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.Params["search"]))
                    {
                        com.Parameters.AddWithValue("newcatname", Request.Params["search"]);
                    }
                }

                // Se executa comanda si se returneaza valorile intr-un reader
                SqlDataReader reader = com.ExecuteReader();
                ListView1.DataSource = reader; // Alocam readerul pentru citirea datelor
                ListView1.DataBind(); // Incarca datele din reader

            }
            catch (Exception ex)
            {
                IndexErrorLabel.Text = "Eroare din baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}