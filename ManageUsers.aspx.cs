using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManageUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!(Request.Cookies["NewsVendorCookie"] != null && new Bitmask().checkAdmin(int.Parse(Request.Cookies.Get("NewsVendorCookie")["role"]))))
            {
                Response.Redirect("~/Index.aspx");
            }
        }
    }
}