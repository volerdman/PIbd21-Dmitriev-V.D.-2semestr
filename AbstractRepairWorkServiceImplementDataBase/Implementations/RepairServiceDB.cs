using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkServiceImplementDataBase.Implementations
{
    public class RepairServiceDB:IRepairService
    {
        private AbstractRepairDbContext context;

        public RepairServiceDB(AbstractRepairDbContext context)
        {
            this.context = context;
        }

        public List<RepairViewModel> ListGet()
        {
            List<RepairViewModel> result = context.Repairs.Select(rec => new RepairViewModel
            {
                Id = rec.Id,
                RepairName = rec.RepairName,
                Cost = rec.Cost,
                MaterialRepair = context.MaterialRepairs
                    .Where(recMR => recMR.RepairId == rec.Id)
                    .Select(recMR => new MaterialRepairViewModel
                    {
                        Id = recMR.Id,
                        RepairId = recMR.RepairId,
                        MaterialId = recMR.MaterialId,
                        MaterialName = recMR.Material.MaterialName,
                        Count = recMR.Count
                    }).ToList()
            }).ToList();
            return result;
        }

        public RepairViewModel ElementGet(int id)
        {
            Repair element = context.Repairs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new RepairViewModel
                {
                    Id = element.Id,
                    RepairName = element.RepairName,
                    Cost = element.Cost,
                    MaterialRepair = context.MaterialRepairs
                        .Where(recMR => recMR.RepairId == element.Id)
                        .Select(recMR => new MaterialRepairViewModel
                        {
                            Id = recMR.Id,
                            RepairId = recMR.RepairId,
                            MaterialId = recMR.MaterialId,
                            MaterialName = recMR.Material.MaterialName,
                            Count = recMR.Count
                        }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(RepairBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Repair element = context.Repairs.FirstOrDefault(rec =>
                   rec.RepairName == model.RepairName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть услуга с таким названием");
                    }
                    element = new Repair
                    {
                        RepairName = model.RepairName,
                        Cost = model.Cost
                    };
                    context.Repairs.Add(element);
                    context.SaveChanges();
                    // убираем дубли по материалам
                    var groupMaterials = model.MaterialRepair
                     .GroupBy(rec => rec.MaterialId)
                    .Select(rec => new
                    {
                        MaterialId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем материалы
                    foreach (var groupMaterial in groupMaterials)
                    {
                        context.MaterialRepairs.Add(new MaterialRepair
                        {
                            RepairId = element.Id,
                            MaterialId = groupMaterial.MaterialId,
                            Count = groupMaterial.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateElement(RepairBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Repair element = context.Repairs.FirstOrDefault(rec =>
                   rec.RepairName == model.RepairName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть услуга с таким названием");
                    }
                    element = context.Repairs.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.RepairName = model.RepairName;
                    element.Cost = model.Cost;
                    context.SaveChanges();
                    // обновляем существуюущие материалы
                    var compIds = model.MaterialRepair.Select(rec =>
                   rec.MaterialId).Distinct();
                    var updateMaterials = context.MaterialRepairs.Where(rec =>
                   rec.RepairId == model.Id && compIds.Contains(rec.MaterialId));
                    foreach (var updateMaterial in updateMaterials)
                    {
                        updateMaterial.Count =
                       model.MaterialRepair.FirstOrDefault(rec => rec.Id == updateMaterial.Id).Count;
                    }
                    context.SaveChanges();
                    context.MaterialRepairs.RemoveRange(context.MaterialRepairs.Where(rec =>
                    rec.RepairId == model.Id && !compIds.Contains(rec.MaterialId)));
                    context.SaveChanges();
                    // новые записи
                    var groupMaterials = model.MaterialRepair
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.MaterialId)
                   .Select(rec => new
                   {
                       MaterialId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupMaterial in groupMaterials)
                    {
                        MaterialRepair elementMR =
                       context.MaterialRepairs.FirstOrDefault(rec => rec.RepairId == model.Id &&
                       rec.MaterialId == groupMaterial.MaterialId);
                        if (elementMR != null)
                        {
                            elementMR.Count += groupMaterial.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.MaterialRepairs.Add(new MaterialRepair
                            {
                                RepairId = model.Id,
                                MaterialId = groupMaterial.MaterialId,
                                Count = groupMaterial.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Repair element = context.Repairs.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по материалам при удалении услуги
                        context.MaterialRepairs.RemoveRange(context.MaterialRepairs.Where(rec =>
                        rec.RepairId == id));
                        context.Repairs.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
