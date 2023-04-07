using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.DTOs.Users
{
    public class UserForDisplayDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
    }
}