using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CartViewModel
    {
        public Cart cart { set; get; }

        public string ReturnUrl { set; get; }
    }
}