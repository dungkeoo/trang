﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class PhanQuyen : System.Web.UI.Page
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
    public void LoadKhoHang()
    {
        string id = Request.QueryString["mathukho"].ToSafetyString();
        DataProvider dt = new DataProvider();
        KhoHang_MaThuKho_Dropdown.DataSource = dt.ThuKho_Kho_List_Kho(id);
        KhoHang_MaThuKho_Dropdown.DataValueField = "MAKHO";
        KhoHang_MaThuKho_Dropdown.DataTextField = "TENKHO";
        KhoHang_MaThuKho_Dropdown.DataBind();
    }
    public void LoadData()
    {
        string id = Request.QueryString["mathukho"].ToSafetyString();
        DataProvider dt = new DataProvider();
        Repeater_ThuKho_List.DataSource = dt.ThuKho_Kho_List(id);
        Repeater_ThuKho_List.DataBind();
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
        DataTable tb = dtp.ThuKho_Kho_Find(SessionUtility.OidObject.ToInt());
        // DataTable tb = dtp.Kho_GetObject("CD6CE778-0CA0-4D8C-AD49-52BE3BF7AB6B");
        if (tb.Rows.Count > 0)
        {
            DataRow row = tb.Rows[0];

            KhoHang_MaThuKho_Dropdown.SelectedItem.Text = row["TENKHO"].ToSafetyString();
            KhoHang_MaThuKho_Dropdown.SelectedItem.Value = row["MAKHO"].ToSafetyString();
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
        LoadKhoHang();
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
        if (dtp.ThuKho_Kho_Delete(SessionUtility.OidObject.ToInt()))
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
        string oidUser = SessionUtility.UserOid;
        string id = Request.QueryString["mathukho"].ToSafetyString();

        if (CheckInfo())
        {
            if (SessionUtility.EventMode == "Create")
            {
                DataProvider dtp = new DataProvider();
                if (dtp.ThuKho_Kho_Insert(id,KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToSafetyString()))
                {
                    ucMessage.ShowSuccess("Đăng ký thành công.");
                    LoadData();
                    ClearForm();
                    UpdatePanel_View.Update();
                }
                else
                {
                    ucMessage.ShowError("Lỗi hệ thống!"); return;
                }
            }
        }
        if (SessionUtility.EventMode == "Edit")
        {
            DataProvider dtp = new DataProvider();
            if (dtp.ThuKho_Kho_Update(SessionUtility.OidObject.ToInt(), id))
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

    public bool CheckInfo()
    {
        bool result = true;

        if (string.IsNullOrEmpty(KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString()))
        {
            result = false;
            ucMessage.ShowError("Vui lòng chọn kho hàng");
            return result;
        }
        return result;
    }

    public void ClearForm()
    {
        KhoHang_MaThuKho_Dropdown.ClearSelection();
        KhoHang_MaThuKho_Dropdown.SelectedItem.Text = "Chọn kho";

    }


    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        CloseModal("DeleteObjectModal");
        LoadData();
    }
}