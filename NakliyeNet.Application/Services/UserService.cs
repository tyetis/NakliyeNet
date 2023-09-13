using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Common;
using NakliyeNet.Domain.Models.Membership;
using NakliyeNet.Domain.Repository;
using NakliyeNet.Domain.Services;

namespace NakliyeNet.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        IRepository<User> RepoUser { get; set; }

        public UserService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            RepoUser = unitOfWork.GetRepository<User>();
        }

        public PaginationResult<User> GetUsers()
        {
            var query = RepoUser.GetAll();
            return new PaginationResult<User>
            {
                Data = query.ToList(),
                Total = query.Count()
            };
        }

        public User GetUser(string email, string password)
        {
            return RepoUser.Get(n => n.Email == email && n.Password == password);
        }

        public User SignUp(SignUpModel model)
        {
            var entity = new User
            {
                Name = model.Name,
                Surname = model.SurName,
                Email = model.Email,
                Password = model.Password,
                CreateDate = DateTime.Now
            };
            RepoUser.Add(entity);
            UnitOfWork.SaveChanges();
            return entity;
        }
    }
}
