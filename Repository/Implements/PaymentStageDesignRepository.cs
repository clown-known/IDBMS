using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class PaymentStageDesignRepository : IPaymentStageDesignRepository
    {
        public void DeleteById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                var psd = context.PaymentStageDesigns.Where(psd => psd.Id == id).FirstOrDefault();
                if (psd != null)
                {
                    context.PaymentStageDesigns.Remove(psd);
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<PaymentStageDesign> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.PaymentStageDesigns.ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<PaymentStageDesign> GetByDecorProjectDesignId(int designId)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.PaymentStageDesigns
                    .Where(psd => psd.DecorProjectDesignId == designId).ToList();
            }
            catch
            {
                throw;
            }
        }

        public PaymentStageDesign? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.PaymentStageDesigns.Where(psd => psd.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public PaymentStageDesign? Save(PaymentStageDesign entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var psd = context.PaymentStageDesigns.Add(entity);
                context.SaveChanges();
                return psd.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(PaymentStageDesign entity)
        {
            try
            {
                using var context = new IdtDbContext();
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
