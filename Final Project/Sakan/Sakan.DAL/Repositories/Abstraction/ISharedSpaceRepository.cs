using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface ISharedSpaceRepository
    {
        (bool, string?) Create(SharedSpace sharedSpace);
        (bool, string?) Edit(SharedSpace newSharedSpace, int oldSharedSpaceId);
        (bool, string?, List<SharedSpace>?) GetAll();
        (bool, string?, SharedSpace?) GetById(int sharedSpaceId);
        (bool, string?) Delete(int sharedSpaceId);
    }
} 

