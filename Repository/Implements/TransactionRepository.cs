using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TransactionRepository : ITransactionRepository
    {
        public IEnumerable<Transaction> GetAll()
        {

            try
            {
                using var context = new IdtDbContext();
                return context.Transactions.OrderByDescending(time => time.CreatedDate)
                    .Include(u => u.User)
                    .Include(p => p.Project)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public Transaction? GetById(Guid id)
        {

            try
            {
                using var context = new IdtDbContext();
                return context.Transactions.Where(trans => trans.Id == id && trans.IsDeleted == false).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Transaction?> GetByProjectId(Guid id)
        {

            try
            {
                using var context = new IdtDbContext();
                return context.Transactions
                    .OrderByDescending(time => time.CreatedDate)
                    .Where(trans => trans.ProjectId == id && trans.IsDeleted == false).ToList();
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Transaction?> GetByUserId(Guid id)
        {

            try
            {
                using var context = new IdtDbContext();
                return context.Transactions
                    .OrderByDescending(time => time.CreatedDate)
                    .Where(trans => trans.UserId == id && trans.IsDeleted == false).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Transaction?> GetByWarrantyId(Guid id)
        {

            try
            {
                using var context = new IdtDbContext();
                return context.Transactions
                    .OrderByDescending(time => time.CreatedDate)
                    .Where(trans => trans.WarrantyClaimId == id && trans.IsDeleted == false).ToList();
            }
            catch
            {
                throw;
            }
        }

        public Transaction? Save(Transaction entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var transaction = context.Transactions.Add(entity);
                context.SaveChanges();
                return transaction.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(Transaction entity)
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
        public void DeleteById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                var transaction = context.Transactions.Where(trans => trans.Id == id && trans.IsDeleted == false).FirstOrDefault();
                if (transaction != null)
                {
                    transaction.IsDeleted = true;
                    context.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
