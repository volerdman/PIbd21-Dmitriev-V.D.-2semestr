using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

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
                RepairName = rec.Repair.RepairName,
                ExecutorId = rec.ExecutorId,
                ExecutorFIO = rec.Executor.ExecutorFIO
            })
            .ToList();
            return result;
        }

        public void CreateBooking(BookingBindingModel model)
        {
            var booking = new Booking
            {
                CustomerId = model.CustomerId,
                RepairId = model.RepairId,
                ExecutorId = model.ExecutorId,
                CreateDate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = BookingStatus.Принят
            };
            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerId);
            SendEmail(customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} создан успешно", booking.Id, booking.CreateDate.ToShortDateString()));
            context.SaveChanges();
        }

        public void TakeBookingInWork(BookingBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
                try
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != BookingStatus.Принят && element.Status !=
                    BookingStatus.НедостаточноРесурсов)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var materialRepairs = context.MaterialRepairs.Include(rec =>
                    rec.Material).Where(rec => rec.RepairId == element.RepairId).ToList();
                    // списываем
                    foreach (var materialRepair in materialRepairs)
                    {
                        int countOnStorage = materialRepair.Count * element.Count;
                        var storageMaterials = context.StorageMaterials.Where(rec =>
                        rec.MaterialId == materialRepair.MaterialId).ToList();
                        foreach (var storageMaterial in storageMaterials)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (storageMaterial.Count >= countOnStorage)
                            {
                                storageMaterial.Count -= countOnStorage;
                                countOnStorage = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorage -= storageMaterial.Count;
                                storageMaterial.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorage > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                           materialRepair.Material.MaterialName + " требуется " + materialRepair.Count + ", не хватает " +
                           countOnStorage);
                         }
                    }
                    element.ExecutorId = model.ExecutorId;
                    element.ImplementDate = DateTime.Now;
                    element.Status = BookingStatus.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.Customer.Mail, "Оповещение по заказам",
                    string.Format("Заказ №{0} от {1} передеан в работу", element.Id,
                    element.CreateDate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.Status = BookingStatus.НедостаточноРесурсов;
                    context.SaveChanges();
                    transaction.Commit();
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
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передан на оплату", 
                element.Id, element.CreateDate.ToShortDateString()));
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
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} оплачен успешно", 
                element.Id, element.CreateDate.ToShortDateString()));
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

        public List<BookingViewModel> GetFreeBookings()
        {
            List<BookingViewModel> result = context.Bookings
            .Where(x => x.Status == BookingStatus.Принят || x.Status ==
           BookingStatus.НедостаточноРесурсов)
            .Select(rec => new BookingViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new
               MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new
               NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
               ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
