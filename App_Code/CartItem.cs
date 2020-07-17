using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartItem
/// </summary>
public class CartItem
{
    public string CartItemID
    {
        get;
        set;
    }
    public string Title
    {
        get;
        set;
    }
    public string Avatar
    {
        get;
        set;
    }
    public double Price
    {
        get;
        set;
    }
   
    public int Quantity
    {
        get;
        set;
    }
}