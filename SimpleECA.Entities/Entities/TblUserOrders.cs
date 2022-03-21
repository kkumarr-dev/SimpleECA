using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleECA.Entities
{
    [Table("tbluserorders")]
    public class TblUserOrders
    {
        [Key]
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

    }
}
