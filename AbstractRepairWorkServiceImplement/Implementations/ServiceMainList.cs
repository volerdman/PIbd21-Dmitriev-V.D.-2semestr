using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairWorkServiceImplement;
using System;
using System.Collections.Generic;
using System.Linq;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairWorkModel;

namespace AbstractRepairsWorkServiceImplement.Implementations
{
    public class ServiceMainList:IServiceMain
    {
        private DataSingletonList source;
        public ServiceMainList()
        {
            source = DataSingletonList.GetInstance();
        }
        public List<BookingViewModel> ListGet()
        {
            List<BookingViewModel> result = new List<BookingViewModel>();
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                string customerFIO = string.Empty;
                for (int j = 0; j < source.Customers.Count; ++j)
                {
                    if (source.Customers[j].Id == source.Bookings[i].CustomerId)
                    {
                        customerFIO = source.Customers[j].CustomerFIO;
                        break;
                    }
                }
                string repairName = string.Empty;
                for (int j = 0; j < source.Repairs.Count; ++j)
                {
                    if (source.Repairs[j].Id == source.Bookings[i].RepairId)
                    {
                        repairName = source.Repairs[j].RepairName;
                        break;
                    }
                }
                result.Add(new BookingViewModel
                {
                    Id = source.Bookings[i].Id,
                    CustomerId = source.Bookings[i].CustomerId,
                    CustomerFIO = customerFIO,
                    RepairId = source.Bookings[i].RepairId,
                    RepairName = repairName,
                    Count = source.Bookings[i].Count,
                    Sum = source.Bookings[i].Sum,
                    CreateDate = source.Bookings[i].CreateDate.ToLongDateString(),
                    ImplementDate = source.Bookings[i].ImplementDate?.ToLongDateString(),
                    Status = source.Bookings[i].Status.ToString()
                });
            }
            return result;
        }
        public void CreateBooking(BookingBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Bookings[i].Id > maxId)
                {
                    maxId = source.Customers[i].Id;
                }
            }
            source.Bookings.Add(new Booking
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                RepairId = model.RepairId,
                CreateDate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = BookingStatus.Принят
            });
        }
        public void TakeBookingInWork(BookingBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Bookings[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Bookings[index].Status != BookingStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.Bookings[index].ImplementDate = DateTime.Now;
            source.Bookings[index].Status = BookingStatus.Выполняется;
        }
        public void FinishBooking(BookingBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Bookings[index].Status != BookingStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.Bookings[index].Status = BookingStatus.Готов;
        }
        public void PayBooking(BookingBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Bookings[index].Status != BookingStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.Bookings[index].Status = BookingStatus.Оплачен;
        }
    }
}
