using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class DangNhap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            
            ucMessage.HideAll();
        }
    }
    public bool CheckInfo()
    {
        bool result = true;
        if (string.IsNullOrEmpty(TaiKhoan.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập mã thủ kho");
            return result;
        }

        if (string.IsNullOrEmpty(MatKhau.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập mật khẩu");
            return result;
        }
        return result;
    }
    protected void btn_DangNhap_Click(object sender, EventArgs e)
    {
        if (CheckInfo())
        {
            string taikhoan = TaiKhoan.Value.Trim();
            string matkhau = MatKhau.Value;
            DataProvider dtp = new DataProvider();
            if (dtp.ThuKho_Login(taikhoan, matkhau))
            {
                ucMessage.ShowSuccess("Đăng nhập thành công");
                if (dtp.ThuKho_CheckAdmin(taikhoan.Trim()))
                {
                    SessionUtility.AdminUsername = taikhoan.Trim();
                    SessionUtility.AdminOid = taikhoan.Trim();
                }
                else
                {
                    SessionUtility.AdminUsername = "";
                    SessionUtility.AdminOid = "";
                }
                SessionUtility.UserOid = taikhoan;
                //SessionUtility.UserOid = dtp.TaiKhoan_LayOid(taikhoan);
                if (!string.IsNullOrEmpty(SessionURL.BackUrl))
                {
                    Response.Redirect(SessionURL.BackUrl);
                }
                else
                {
                    Response.Redirect("/");
                }
                return;

            }
            else
            {
                ucMessage.ShowError("Tài khoản hoặc mật khẩu không đúng.");
                return;
            }
        }
    }
}