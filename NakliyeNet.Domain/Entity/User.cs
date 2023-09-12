using System;
using System.Collections.Generic;

#nullable disable

namespace TransportationApp.Domain.Entity
{
    public partial class User
    {
        public User()
        {
            CompanyComments = new HashSet<CompanyComment>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<CompanyComment> CompanyComments { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
