using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeUtility;
public class SessionURL
{
    public static string BackUrl
    {
        get
        {
            return HttpContext.Current.Session["BackUrl"].ToSafetyString();
        }
        set
        {
            HttpContext.Current.Session["BackUrl"] = value;
        }
    }
}