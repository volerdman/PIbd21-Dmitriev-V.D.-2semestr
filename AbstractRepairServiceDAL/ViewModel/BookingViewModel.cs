using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.ViewModel
{
    [DataContract]
    public class BookingViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string CustomerFIO { get; set; }

        [DataMember]
        public int RepairId { get; set; }

        [DataMember]
        public string RepairName { get; set; }

        [DataMember]
        public int? ExecutorId { get; set; }

        [DataMember]
        public string ExecutorFIO { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string CreateDate { get; set; }

        [DataMember]
        public string ImplementDate { get; set; }
    }
}
