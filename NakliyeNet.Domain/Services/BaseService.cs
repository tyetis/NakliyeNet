using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Repository;

namespace NakliyeNet.Domain.Services
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
