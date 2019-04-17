using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace inProjects.WebApp.Models
{
    public class LoginModel
    {
        [Required]
        public string LoginId { get; set; }

        [Required]
        [DataType( DataType.Password )]
        public string Password { get; set; }
    }
}
