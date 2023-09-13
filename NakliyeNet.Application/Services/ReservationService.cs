using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Common;
using NakliyeNet.Domain.Repository;
using NakliyeNet.Domain.Services;
using NakliyeNet.Infrastructure.Common;

namespace NakliyeNet.Application.Services
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
