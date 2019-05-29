using AbstractRepairServiceDAL.Attributies;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с исполнителями")]
    public interface IExecutorService
    {
        [CustomMethod("Метод получения списка исполнителей")]
        List<ExecutorViewModel> GetList();

        [CustomMethod("Метод получения исполнителя по id")]
        ExecutorViewModel GetElement(int id);

        [CustomMethod("Метод добавления исполнителя")]
        void AddElement(ExecutorBindingModel model);

        [CustomMethod("Метод изменения данных по исполнителю")]
        void UpdElement(ExecutorBindingModel model);

        [CustomMethod("Метод удаления исполнителя")]
        void DelElement(int id);

        ExecutorViewModel GetFreeWorker();
    }
}
