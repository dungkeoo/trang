using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cart
/// </summary>
public class Cart
{
    public string ClientID
    {
        get;
        set;
    }

    public string Email
    {
        get;
        set;
    }

    public string FullName
    {
        get;
        set;
    }

    public string Mobi1
    {
        get;
        set;
    }

    public string Mobo2
    {
        get;
        set;
    }

    public bool Gender
    {
        get;
        set;
    }

    public string Adress
    {
        get;
        set;
    }

    public int PaymenMethod
    {
        get;
        set;
    }

    private double _Total;
    public double Total
    {
        get
        {
            _Total=0;
            foreach(KeyValuePair<string, CartItem> item in  CartItems)
            { 
                _Total += (item.Value.Quantity * item.Value.Price);
            }
            return _Total;
        }
        
    }

    private double _Bonus;
    public double Bonus
    {
        get
        {
            _Bonus = Total - Amount;
            return _Bonus;
        }
    }

    private double _Amount;
    public double Amount
    {
        get
        {
            _Amount = 0;
            foreach (KeyValuePair<string, CartItem> item in CartItems)
            {
                _Amount += (item.Value.Quantity * item.Value.Price);
            }
            return _Amount;

        }
      
    }

    private double _CountItems;
    public double CountItems
    {
        get
        {
            _CountItems = 0;
            foreach(KeyValuePair <string,CartItem> item in  CartItems)
            {
                _CountItems += item.Value.Quantity;
            }
            return _CountItems;
        }
        
    }

    private Dictionary<string, CartItem> _CartItems;
    public Dictionary<string, CartItem> CartItems
    {
        get
        {
            if (_CartItems == null)
                _CartItems = new Dictionary<string, CartItem>();
            return _CartItems; 

        }
        set
        {
            _CartItems = value;

        }
    }
}