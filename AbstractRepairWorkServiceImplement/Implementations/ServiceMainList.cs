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
            List<BookingViewModel> result = source.Bookings.Select(rec => new BookingViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                RepairId = rec.RepairId,
                CreateDate = rec.CreateDate.ToLongDateString(),
                ImplementDate = rec.ImplementDate?.ToLongDateString(),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                CustomerFIO = source.Customers.FirstOrDefault(recC => recC.Id == rec.CustomerId)?.CustomerFIO,
                RepairName = source.Repairs.FirstOrDefault(recR => recR.Id == rec.RepairId)?.RepairName,
            }).ToList();
            return result;
        }

        public void CreateBooking(BookingBindingModel model)
        {
            int maxId = source.Bookings.Count > 0 ? source.Bookings.Max(rec => rec.Id) : 0;
            source.Bookings.Add(new Booking
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                RepairId = model.RepairId,
                ExecutorId = model.ExecutorId,
                CreateDate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = BookingStatus.Принят
            });
        }

        public void TakeBookingInWork(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != BookingStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству материалов на складах
            var productMaterials = source.MaterialRepairs.Where(rec => rec.RepairId
        == element.RepairId);
            foreach (var productMaterial in productMaterials)
            {
                int countOnStorages = source.StorageMaterials
                .Where(rec => rec.MaterialId ==
               productMaterial.MaterialId)
               .Sum(rec => rec.Count);
                if (countOnStorages < productMaterial.Count * element.Count)
                {
                    var componentName = source.Materials.FirstOrDefault(rec => rec.Id ==
                   productMaterial.MaterialId);
                    throw new Exception("Не достаточно материала " +
                   componentName?.MaterialName + " требуется " + (productMaterial.Count * element.Count) +
                   ", в наличии " + countOnStorages);
                }
            }
            // списываем
            foreach (var productMaterial in productMaterials)
            {
                int countOnStorages = productMaterial.Count * element.Count;
                var stockMaterials = source.StorageMaterials.Where(rec => rec.MaterialId
               == productMaterial.MaterialId);
                foreach (var stockMaterial in stockMaterials)
                {
                    // материалов на одном слкаде может не хватать
                    if (stockMaterial.Count >= countOnStorages)
                    {
                        stockMaterial.Count -= countOnStorages;
                        break;
                    }
                    else
                    {
                        countOnStorages -= stockMaterial.Count;
                        stockMaterial.Count = 0;
                    }
                }
            }
            element.ImplementDate = DateTime.Now;
            element.Status = BookingStatus.Выполняется;
        }

        public void FinishBooking(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != BookingStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = BookingStatus.Готов;

        }

        public void PayBooking(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != BookingStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = BookingStatus.Оплачен;
        }

        public void PutMaterialOnStorage(StorageMaterialBindingModel model)
        {
            StorageMaterial element = source.StorageMaterials.FirstOrDefault(rec =>
           rec.StorageId == model.StorageId && rec.MaterialId == model.MaterialId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageMaterials.Count > 0 ?
               source.StorageMaterials.Max(rec => rec.Id) : 0;
                source.StorageMaterials.Add(new StorageMaterial
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    MaterialId = model.MaterialId,
                    Count = model.Count
                });
            }
        }

        public List<BookingViewModel> GetFreeBookings()
        {
            List<BookingViewModel> result = source.Bookings
                .Select(rec => new BookingViewModel
                {
                    Id = rec.Id
                })
                .ToList();
            return result;
        }
    }
}
