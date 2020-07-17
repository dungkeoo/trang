using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckLogin();
            UserName.Text = SessionUtility.UserOid;
        }
    }
    void CheckLogin()
    {
        if (string.IsNullOrEmpty(SessionUtility.UserOid))
        {
            UserName.Text = SessionUtility.UserOid;
            Response.Redirect("/DangNhap.aspx");
        }
    }

    protected void Logout_Click(object sender, EventArgs e)
    {
        SessionUtility.UserOid = "";
        SessionUtility.AdminUsername = "";
        SessionUtility.AdminOid = "";
        Response.Redirect("/DangNhap.aspx");
    }
}
