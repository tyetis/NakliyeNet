using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Common;

namespace NakliyeNet.Domain.Services
{
    public interface IReservationService
    {
        PaginationResult<Reservation> GetReservations();
    }
}
