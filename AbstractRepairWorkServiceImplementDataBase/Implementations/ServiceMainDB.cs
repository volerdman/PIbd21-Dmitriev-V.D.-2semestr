using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkServiceImplementDataBase.Implementations
{
    public class ServiceMainDB : IServiceMain
    {
        private AbstractRepairDbContext context;

        public ServiceMainDB(AbstractRepairDbContext context)
        {
            this.context = context;
        }

        public List<BookingViewModel> ListGet()
        {
            List<BookingViewModel> result = context.Bookings.Select(rec => new BookingViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                RepairId = rec.RepairId,
                CreateDate = SqlFunctions.DateName("dd", rec.CreateDate) + " " +
            SqlFunctions.DateName("mm", rec.CreateDate) + " " +
            SqlFunctions.DateName("yyyy", rec.CreateDate),
                ImplementDate = rec.ImplementDate == null ? "" :
            SqlFunctions.DateName("dd",
           rec.ImplementDate.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.ImplementDate.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.ImplementDate.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                CustomerFIO = rec.Customer.CustomerFIO,
                RepairName = rec.Repair.RepairName
            })
            .ToList();
            return result;
        }

        public void CreateBooking(BookingBindingModel model)
        {
            context.Bookings.Add(new Booking
            {
                CustomerId = model.CustomerId,
                RepairId = model.RepairId,
                CreateDate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = BookingStatus.Принят
            });
            context.SaveChanges();
        }

        public void TakeBookingInWork(BookingBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Booking element = context.Bookings.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != BookingStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var repairMaterials = context.MaterialRepairs.Include(rec => rec.Material)
                        .Where(rec => rec.RepairId == element.RepairId).ToList();
                    // списываем
                    foreach (var repairMaterial in repairMaterials)
                    {
                        int countOnStorages = repairMaterial.Count * element.Count;
                        var stockMaterials = context.StorageMaterials.Where(rec =>
                        rec.MaterialId == repairMaterial.MaterialId).ToList();
                        foreach (var stockMaterial in stockMaterials)
                        {
                            // материалов на одном складе может не хватать
                            if (stockMaterial.Count >= countOnStorages)
                            {
                                stockMaterial.Count -= countOnStorages;
                                countOnStorages = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorages -= stockMaterial.Count;
                                stockMaterial.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorages > 0)
                        {
                            throw new Exception("Не достаточно материала " +
                           repairMaterial.Material.MaterialName + " требуется " + repairMaterial.Count + 
                           ", не хватает " + countOnStorages);
                         }
                    }
                    element.ImplementDate = DateTime.Now;
                    element.Status = BookingStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishBooking(BookingBindingModel model)
        {
            Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != BookingStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = BookingStatus.Готов;
            context.SaveChanges();
        }

        public void PayBooking(BookingBindingModel model)
        {
            Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != BookingStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = BookingStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutMaterialOnStorage(StorageMaterialBindingModel model)
        {
            StorageMaterial element = context.StorageMaterials.FirstOrDefault(rec =>
           rec.StorageId == model.StorageId && rec.MaterialId == model.MaterialId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StorageMaterials.Add(new StorageMaterial
                {
                    StorageId = model.StorageId,
                    MaterialId = model.MaterialId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
