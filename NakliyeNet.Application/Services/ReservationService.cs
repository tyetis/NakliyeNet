using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Application.Models.Membership;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Common;
using TransportationApp.Domain.Repository;
using TransportationApp.Domain.Services;
using TransportationApp.Infrastructure.Common;

namespace TransportationApp.Application.Services
{
    public class ReservationService : BaseService, IReservationService
    {
        IRepository<Reservation> RepoReservation { get; set; }
        LoggedUser LoggedUser { get; set; }

        public ReservationService(IRepository<Reservation> repoReservation, LoggedUser loggedUser)
        {
            RepoReservation = repoReservation;
            LoggedUser = loggedUser;
        }

        public PaginationResult<Reservation> GetReservations()
        {
            var query = RepoReservation.GetAll(n => n.CompanyId == LoggedUser.Id).Include(n => n.Request).ThenInclude(n => n.User).Include(n => n.Request).ThenInclude(n => n.Category);
            return new PaginationResult<Reservation>
            {
                Data = query.ApplyPagination(20, 0).ToList(),
                Total = query.Count()
            };
        }
    }
}
