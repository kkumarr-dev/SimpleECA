using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Models
{
    public class OrderViewModel
    {
        public int orderid { get; set; }
        public int userid { get; set; }
        public DateTime ordereddate { get; set; }
        public DateTime? deliverydate { get; set; }
        public DateTime? dispatchdate { get; set; }
        public DateTime? delivereddate { get; set; }
        public DateTime? canceleddate { get; set; }
        public bool? iscanceled { get; set; }
        public bool? isdelivered { get; set; }
        public bool? ispaid { get; set; }
        public double? price { get; set; }
        public int? addressid { get; set; }
        public UserAddressViewModel OrderedAddress { get; set; }
        public List<ProductViewModel> OrderedProducts { get; set; }
    }
}
