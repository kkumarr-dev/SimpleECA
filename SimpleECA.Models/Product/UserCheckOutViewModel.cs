using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models
{
    public class UserCheckOutViewModel
    {
        public int userid { get; set; }
        public int addressid { get; set; }
        public List<CheckoutProductList> productList { get; set; }
        public float totalPrice { get; set; }
    }
    public class CheckoutProductList
    {
        public int productId { get; set; }
        public float price { get; set; }
    }
}
