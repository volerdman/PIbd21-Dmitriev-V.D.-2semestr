using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveRepairPrice(ReportBindingModel model);

        List<StorageLoadViewModel> GetStorageLoad();

        void SaveStorageLoad(ReportBindingModel model);

        List<CustomerBookingModel> GetCustomerBooking(ReportBindingModel model);

        void SaveCustomerBooking(ReportBindingModel model);
    }
}
