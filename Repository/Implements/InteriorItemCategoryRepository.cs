﻿using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class InteriorItemCategoryRepository : IInteriorItemCategoryRepository
    {
        public IEnumerable<InteriorItemCategory> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItemCategories.ToList();
            }
            catch
            {
                throw;
            }
        }

        public InteriorItemCategory? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItemCategories.FirstOrDefault(iic => iic.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public InteriorItemCategory Save(InteriorItemCategory entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var iic = context.InteriorItemCategories.Add(entity);
                context.SaveChanges();
                return iic.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(InteriorItemCategory entity)
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

        public void DeleteById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                var iic = context.InteriorItemCategories.FirstOrDefault(iic => iic.Id == id);
                if (iic != null)
                {
                    context.InteriorItemCategories.Remove(iic);
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