using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Domain.Repository;

namespace TransportationApp.Domain.Services
{
    public abstract class BaseService : IDisposable
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public BaseService()
        {
        }

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}
