using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class SharedSpaceRepository : ISharedSpaceRepository
    {
        private readonly ApplicationDbContext Db;

        public SharedSpaceRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(SharedSpace sharedSpace)
        {
            try
            {
                bool propertyExists = Db.Properties.Any(p => p.Id == sharedSpace.PropertyId);
                if (!propertyExists)
                    return (true, "Property not found");

                Db.SharedSpaces.Add(sharedSpace);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(SharedSpace newSharedSpace, int oldSharedSpaceId)
        {
            try
            {
                var oldSpace = Db.SharedSpaces.FirstOrDefault(s => s.Id == oldSharedSpaceId);
                if (oldSpace == null)
                    return (true, "SharedSpace not found");

                oldSpace.Name = newSharedSpace.Name;
                oldSpace.SpaceType = newSharedSpace.SpaceType;
                oldSpace.Description = newSharedSpace.Description;

                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<SharedSpace>?) GetAll()
        {
            try
            {
                var spaces = Db.SharedSpaces
                    .Include(s => s.Property)
                    .ToList();

                return (false, null, spaces);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, SharedSpace?) GetById(int sharedSpaceId)
        {
            try
            {
                var space = Db.SharedSpaces
                    .Include(s => s.Property)
                    .FirstOrDefault(s => s.Id == sharedSpaceId);

                if (space == null)
                    return (true, "SharedSpace not found", null);

                return (false, null, space);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int sharedSpaceId)
        {
            try
            {
                var space = Db.SharedSpaces.FirstOrDefault(s => s.Id == sharedSpaceId);
                if (space == null)
                    return (true, "SharedSpace not found");

                Db.SharedSpaces.Remove(space);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }
    }
}
