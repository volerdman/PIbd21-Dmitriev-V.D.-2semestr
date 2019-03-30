using System;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.ViewModel
{
    public class StorageLoadViewModel
    {
        public string StorageName { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Materials { get; set; }
    }
}
