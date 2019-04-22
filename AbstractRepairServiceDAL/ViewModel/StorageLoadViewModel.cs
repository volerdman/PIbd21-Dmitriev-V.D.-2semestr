using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractRepairServiceDAL.ViewModel
{
    [DataContract]
    public class StorageLoadViewModel
    {
        [DataMember]
        public string StorageName { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public IEnumerable<Tuple<string, int>> Materials { get; set; }
    }
}
