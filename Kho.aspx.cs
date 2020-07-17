using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Kho : System.Web.UI.Page
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
        Repeater_Data_List.DataSource = dt.Kho_List(SessionUtility.AdminOid == "" ? SessionUtility.UserOid : "ADMIN");
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
        DataTable tb = dtp.Kho_Find(SessionUtility.OidObject.ToSafetyString());
        // DataTable tb = dtp.Kho_GetObject("CD6CE778-0CA0-4D8C-AD49-52BE3BF7AB6B");
        if (tb.Rows.Count > 0)
        {
            DataRow row = tb.Rows[0];

            MaKho.Value = row["MAKHO"].ToSafetyString();
            MaKho.Disabled = true;
            TenKho.Value = row["TENKHO"].ToSafetyString();
            DiaDiem.Value = row["DIADIEM"].ToSafetyString();
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
        if (dtp.Kho_Delete(SessionUtility.OidObject))
        {
            CloseModal("DeleteObjectModal");
            LoadData();
            UpdatePanel_View.Update();
        }
        else
        {

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
                    if (dtp.Kho_Find(MaKho.Value).Rows.Count <= 0)
                    {
                        if (dtp.Kho_Insert(MaKho.Value, TenKho.Value, DiaDiem.Value))
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
                    if (dtp.Kho_Update(MaKho.Value, TenKho.Value, DiaDiem.Value))
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
        if (string.IsNullOrEmpty(MaKho.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập mã quản lý kho");
            return result;
        }

        if (string.IsNullOrEmpty(TenKho.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập tên kho");
            return result;
        }

        if (string.IsNullOrEmpty(DiaDiem.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập địa điểm");
            return result;
        }
       
        return result;
    }

    public void ClearForm()
    {
        MaKho.Value = "";
        MaKho.Disabled = false;
        TenKho.Value = "";
        DiaDiem.Value = "";
    }


    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        CloseModal("DeleteObjectModal");
        LoadData();
    }
}