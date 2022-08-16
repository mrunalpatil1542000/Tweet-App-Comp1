using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace com.tweetapp.Models
{
    class UserCred
    {
        [Key]
        [Required(ErrorMessage = "Please Enter Email...")]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter First Name...")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name...")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Gender...")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Enter Date of Birth...")]
        public DateTime DateOfBirth { get; set; } 

        [Required(ErrorMessage = "Please Enter Password...")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool ActiveStatus { get; set; } = false;

    }
}
