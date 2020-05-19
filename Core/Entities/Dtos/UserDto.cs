using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Dtos
{
    public class UserDto : BaseDto, IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[] ProfilePicture { get; set; }
    }


    public class LoginDto : IDto
    {
        [Display(Name = "Email Address")]
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class RegisterDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
