using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.Interfaces
{
    public interface IExecutorService
    {
        List<ExecutorViewModel> GetList();

        ExecutorViewModel GetElement(int id);

        void AddElement(ExecutorBindingModel model);

        void UpdElement(ExecutorBindingModel model);

        void DelElement(int id);

        ExecutorViewModel GetFreeWorker();
    }
}
