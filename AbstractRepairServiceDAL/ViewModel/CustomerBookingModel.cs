using System.Runtime.Serialization;

namespace AbstractRepairServiceDAL.ViewModel
{
    [DataContract]
    public class CustomerBookingModel
    {
        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CreateDate { get; set; }

        [DataMember]
        public string RepairName { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
