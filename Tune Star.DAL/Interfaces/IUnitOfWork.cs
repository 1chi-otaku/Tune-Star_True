
using System.Numerics;
using Tune_Star.DAL.Entities;
using Tune_Star.DAL.Interfaces;

namespace Soccer.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Songs> Songs { get; }
        IRepository<Genres> Genres { get; }
        IRepository<Users> Users { get; }
        Task Save();
    }
}
