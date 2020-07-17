using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btn_Luu);
            LoadData();
            LoadData_XuatKho();
            LoadData_NhapKho();
        }
    }
    public void LoadData()
    {
        DataProvider dt = new DataProvider();
        Repeater_Data_List.DataSource = dt.Kho_HangHoa_ChiTiet_List(SessionUtility.AdminUsername != "" ? "ADMIN" : SessionUtility.UserOid,"3");
        Repeater_Data_List.DataBind();
    }

    public void LoadData_NhapKho()
    {
        DataProvider dt = new DataProvider();
        Repeater_Data_List_NhapKho.DataSource = dt.Kho_HangHoa_ChiTiet_List(SessionUtility.AdminUsername != "" ? "ADMIN" : SessionUtility.UserOid,"1");
        Repeater_Data_List_NhapKho.DataBind();
    }

    public void LoadData_XuatKho()
    {
        DataProvider dt = new DataProvider();
        Repeater_Data_List_XuatKho.DataSource = dt.Kho_HangHoa_ChiTiet_List(SessionUtility.AdminUsername != "" ? "ADMIN" : SessionUtility.UserOid, "2");
        Repeater_Data_List_XuatKho.DataBind();
    }
    protected void EditObject_Click(object sender, EventArgs e)
    {
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
        SessionUtility.EventMode = "Edit";

        ucMessage.HideAll();
        OpenModal("NewObjectModal");
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
        if (dtp.NhaSanXuat_Delete(SessionUtility.OidObject))
        {
            CloseModal("DeleteObjectModal");
            LoadData();
            UpdatePanel_View.Update();
        }
    }
    protected void btn_NewObject_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();
        OpenModal("NewObjectModal");    
        UpdatePanel_Object.Update();
    }

    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        CloseModal("DeleteObjectModal");
        LoadData();
    }

   
    protected void LinkButton_PhieuChuyen_Click(object sender, EventArgs e)
    {
        DataProvider dtp = new DataProvider();
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
        if (dtp.Kho_HangHoa_ChiTiet_Accpet(SessionUtility.OidObject.ToInt(), SessionUtility.UserOid))
        {
            btn_NewObject_Click(sender, e);
            ucMessage.ShowSuccess("Thêm mới thành công.");
            LoadData();
            LoadData_NhapKho();
            LoadData_XuatKho();
            UpdatePanel_View.Update();
        }
        else
        {
            btn_NewObject_Click(sender, e);
            ucMessage.ShowError("lỗi hệ thống.");   
        }
    }

    protected void LinkButton_PhieuNhap_Click(object sender, EventArgs e)
    {
        DataProvider dtp = new DataProvider();
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
        if (dtp.Kho_HangHoa_ChiTiet_Accpet(SessionUtility.OidObject.ToInt(), SessionUtility.UserOid))
        {
            btn_NewObject_Click(sender, e);
            ucMessage.ShowSuccess("Thêm mới thành công.");
            LoadData();
            LoadData_NhapKho();
            LoadData_XuatKho();
            UpdatePanel_View.Update();
        }
        else
        {
            btn_NewObject_Click(sender, e);
            ucMessage.ShowError("lỗi hệ thống.");
        }
    }

    protected void LinkButton_PhieuXuât_Click(object sender, EventArgs e)
    {
        DataProvider dtp = new DataProvider();
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
        if (dtp.Kho_HangHoa_ChiTiet_Accpet(SessionUtility.OidObject.ToInt(), SessionUtility.UserOid))
        {
            btn_NewObject_Click(sender, e);
            ucMessage.ShowSuccess("Thêm mới thành công.");
            LoadData();
            LoadData_NhapKho();
            LoadData_XuatKho();
            UpdatePanel_View.Update();
        }
        else
        {
            btn_NewObject_Click(sender, e);
            ucMessage.ShowError("lỗi hệ thống.");
        }
    }

    protected void btn_Luu_Click(object sender, EventArgs e)
    {

    }
}