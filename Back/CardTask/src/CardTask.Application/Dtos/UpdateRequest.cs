using CardTask.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Application.Dtos
{
    public class UpdateRequest
    {
        private string _password;
        private string _confirmPassword;
        //public string Role;
        private string _userName;

        public string FirstName { get; set; }
        public string LastName { get; set; }

       /* [EnumDataType(typeof(Role))]
        public Role Role
        {
            get => _role;
            set => _role = replaceEmptyWithNull(value.);
        }*/

        [EmailAddress]
        public string UserName
        {
            get => _userName;
            set => _userName = replaceEmptyWithNull(value);
        }

        [MinLength(6)]
        public string Password
        {
            get => _password;
            set => _password = replaceEmptyWithNull(value);
        }

        [Compare("Password")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = replaceEmptyWithNull(value);
        }

        // helpers

        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }

}
