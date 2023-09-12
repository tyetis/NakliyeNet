using System;

namespace TransportationApp.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int ExecuteSql(string sql, params object[] paramaters);
        int SaveChanges();
    }
}