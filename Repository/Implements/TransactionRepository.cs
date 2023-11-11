using BusinessObject.Models;
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
                return context.Transactions.ToList();
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
                return context.Transactions.Where(trans => trans.Id == id).FirstOrDefault();
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
                return context.Transactions.Where(trans => trans.ProjectId == id).ToList();
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Transaction?> GetByUserId(Guid userId)
        {

            try
            {
                using var context = new IdtDbContext();
                return context.Transactions.Where(trans => trans.UserId == userId).ToList();
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
                var transaction = context.Transactions.Where(trans => trans.Id == id).FirstOrDefault();
                if (transaction != null)
                {
                    context.Transactions.Remove(transaction);
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
