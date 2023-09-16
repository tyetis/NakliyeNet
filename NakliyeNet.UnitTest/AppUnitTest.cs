using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Application.Services;
using NakliyeNet.Domain.Common;
using NakliyeNet.Domain.Repository;
using NakliyeNet.Domain.Services;
using NakliyeNet.Infrastructure.EntityFramework;
using NakliyeNet.Infrastructure.EntityFramework.Repository;
using NUnit.Framework;
using System;
using System.IO;

namespace NakliyeNet.UnitTest
{
    public class Tests
    {
        private IConfiguration configuration { get; set; }
        private IUserService UserService { get; set; }
        private IRequestService RequestService { get; set; }

        [SetUp]
        public void Setup()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseSqlServer(GetConnectionString(configuration));

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<UserService>().As<IUserService>();
            containerBuilder.RegisterType<RequestService>().As<IRequestService>();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            containerBuilder.RegisterInstance(new LoggedUser { Id = 4, Type = (int)MemberType.User });
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            containerBuilder.RegisterInstance(new AppDbContext(options.Options)).As<DbContext>();

            var container = containerBuilder.Build();

            UserService = container.Resolve<IUserService>();
            RequestService = container.Resolve<IRequestService>();
        }

        [Test]
        public void GetUsers()
        {
            var users = UserService.GetUsers();
            if (users.Data.Count > 0) Assert.Pass(); else Assert.Fail();
        }

        [Test]
        public void GetUser()
        {
            var user = UserService.GetUser("ali.yilmaz@email.com", "12345");
            if (user != null) Assert.Pass(); else Assert.Fail();
        }

        [Test]
        public void GetRequests()
        {
            var requests = RequestService.GetRequests();
            if (requests.Data.Count > 0) Assert.Pass(); else Assert.Fail();
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var rootFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent;
            return string.Format(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value, rootFolder);
        }
    }
}