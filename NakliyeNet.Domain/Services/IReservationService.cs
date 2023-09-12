using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Common;

namespace TransportationApp.Domain.Services
{
    public interface IReservationService
    {
        PaginationResult<Reservation> GetReservations();
    }
}
