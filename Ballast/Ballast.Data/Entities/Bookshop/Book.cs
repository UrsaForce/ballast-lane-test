using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballast.Data.Entities.Bookshop
{
    public class Book
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime RegistrationTimestamp { get; set; }
        public string RegistrationUser { get; set; }
        public string RegistrationIp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateIp { get; set; }
        public bool IsActive { get; set; }
    }
}
