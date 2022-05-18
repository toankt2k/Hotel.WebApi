using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.WebApi.core.Attributies.CustomAttribute;

namespace Hotel.WebApi.core.Entities
{
    public class Bill
    {
        public Guid? BillId { get; set; }

        public string? PaymentType { get; set; }
        public DateTime? PaymentDate { get; set; }
        public float? PaymentAmount { get; set; }
        public string? Note { get; set; }
        [NotMap]
        public User? User { get; set; }
        [NotMap]
        public Booking? Booking { get; set; }

    }
}
