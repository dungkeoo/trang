using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeUtility;
public class SessionUtility
{
    public static string AdminUsername
    {
        get
        {
            return HttpContext.Current.Session["AdminUsername"].ToSafetyString();
        }
        set
        {
            HttpContext.Current.Session["AdminUsername"] = value;
        }
    }

    public static string AdminOid
    {
        get
        {
            return HttpContext.Current.Session["AdminOid"].ToSafetyString();
        }
        set
        {
            HttpContext.Current.Session["AdminOid"] = value;
        }
    }
    public static string UserOid
    {
        get
        {
            //return "766ADE79-152B-4A81-92C7-325BE1B768EF";
            return HttpContext.Current.Session["UserOid"].ToSafetyString();
        }
        set
        {
            //HttpContext.Current.Session["UserOid"] = "766ADE79-152B-4A81-92C7-325BE1B768EF";
            HttpContext.Current.Session["UserOid"] = value;
        }
    }
    public static string AdminAvatar
    {
        get
        {
            return HttpContext.Current.Session["AdminAvatar"].ToSafetyString();
        }
        set
        {
            HttpContext.Current.Session["AdminAvatar"] = value;
        }
    }

    public static bool LoaiTaiKhoan
    {
        get
        {
            return HttpContext.Current.Session["AdminAvatar"].ToBool();
        }
        set
        {
            HttpContext.Current.Session["AdminAvatar"] = value;
        }
    }
    public static string OidObject
    {
        get
        {
            return HttpContext.Current.Session["OidObject"].ToSafetyString();
        }
        set
        {
            HttpContext.Current.Session["OidObject"] = value;
        }
    }
  
    public static string EventMode
    {
        get
        {
            return HttpContext.Current.Session["EventMode"].ToSafetyString();
        }
        set
        {
            HttpContext.Current.Session["EventMode"] = value;
        }
    }

    public static string AvatarImg
    {
        get
        {
            return HttpContext.Current.Session["AvatarImg"].ToSafetyString();
        }
        set
        {
            HttpContext.Current.Session["AvatarImg"] = value;
        }
    }

    public static Cart CurrentCart
    {
        get
        {
            if (HttpContext.Current.Session["CurrenCart"] == null)
                HttpContext.Current.Session["CurrenCart"] = new Cart();
            return HttpContext.Current.Session["CurrenCart"] as Cart;
        }
        set
        {
            HttpContext.Current.Session["CurrenCart"] = value;
        }
    }
}