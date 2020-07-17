using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class NhomHang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btn_Luu);
            LoadData();
        }
    }
        
    public void LoadData()
    {   
        DataProvider dt = new DataProvider();
        Repeater_Data_List.DataSource = dt.NhomHang_List();
        Repeater_Data_List.DataBind();
    }
    protected void EditObject_Click(object sender, EventArgs e)
    {
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
        SessionUtility.EventMode = "Edit";

        ucMessage.HideAll();
        LoadEdit();
        OpenModal("NewObjectModal");
    }

    public void LoadEdit()
    {
        DataProvider dtp = new DataProvider();
        DataTable tb = dtp.NhomHang_Find(SessionUtility.OidObject.ToSafetyString());
        if (tb.Rows.Count > 0)
        {
            DataRow row = tb.Rows[0];

            MaQuanLy.Value = row["MANHOMHANG"].ToSafetyString();
            MaQuanLy.Disabled = true;
            Ten.Value = row["TENNHOMHANG"].ToSafetyString();
            GhiChu.Value = row["GHICHU"].ToSafetyString();
            //SessionUtility.OidObject = "";
            UpdatePanel_Object.Update();
        }
        else
        {
            ucMessage.ShowError("Lỗi hệ thống!"); return;
        }
    }
    public void OpenModal(string idmodal)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), idmodal, "$('#" + idmodal + "').modal();", true);
        UpdatePanel_Object.Update();
    }

    public void CloseModal(string idmodal)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), idmodal, "$('#" + idmodal + "').hideModal();", true);
        UpdatePanel_Object.Update();
    }
    protected void DeleteObject_Click(object sender, EventArgs e)
    {

        OpenModal("DeleteObjectModal");
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        DataProvider dtp = new DataProvider();
        if (dtp.NhomHang_Delete(SessionUtility.OidObject))
        {
            CloseModal("DeleteObjectModal");
            LoadData();
            UpdatePanel_View.Update();
        }
    }
    protected void btn_NewObject_Click(object sender, EventArgs e)
    {
        ClearForm();
        ucMessage.HideAll();
        OpenModal("NewObjectModal");

        SessionUtility.EventMode = "Create";
        if (SessionUtility.EventMode == "Create")
            SessionUtility.AvatarImg = "";
        UpdatePanel_Object.Update();
    }

    protected void btn_Luu_Click(object sender, EventArgs e)
    {
        //string oidUser = SessionUtility.AdminOid;
        string oidUser = SessionUtility.UserOid;
        //if (!string.IsNullOrEmpty(oidUser))
        //{
            if (CheckInfo())
            {
                if (SessionUtility.EventMode == "Create")
                {

                    DataProvider dtp = new DataProvider();
                    if (dtp.NhomHang_Find(MaQuanLy.Value).Rows.Count <= 0)
                    {
                        if (dtp.Nhomhang_Insert(MaQuanLy.Value, Ten.Value, GhiChu.Value))
                        {
                            ucMessage.ShowSuccess("Thêm mới thành công.");
                            LoadData();
                            ClearForm();
                            UpdatePanel_View.Update();
                        }
                        else
                        {
                            ucMessage.ShowError("Lỗi hệ thống!"); return;
                        }
                    }
                    else
                    {
                        ucMessage.ShowError("Mã quản lý kho đã tồn tại!"); return;
                    }
                }
                if (SessionUtility.EventMode == "Edit")
                {
                    DataProvider dtp = new DataProvider();
                    if (dtp.NhomHang_Update(MaQuanLy.Value, Ten.Value, GhiChu.Value))
                {
                        ucMessage.ShowSuccess("Cập nhật thành công.");
                        LoadData();
                        UpdatePanel_View.Update();
                    }
                    else
                    {
                        ucMessage.ShowError("Lỗi hệ thống!"); return;
                    }
                }
            }
        //}
    }

    public bool CheckInfo()
    {
        bool result = true;
        if (string.IsNullOrEmpty(MaQuanLy.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập mã quản lý");
            return result;
        }

        if (string.IsNullOrEmpty(Ten.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập tên nhóm hàng");
            return result;
        }
        return result;
    }

    public void ClearForm()
    {
        MaQuanLy.Value = "";
        MaQuanLy.Disabled = false;
        Ten.Value = "";
        GhiChu.Value = "";
    }


    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        CloseModal("DeleteObjectModal");
        LoadData();
    }

}