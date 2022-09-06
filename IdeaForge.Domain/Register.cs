using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class Register : PropertyValidateModel
    {
        public int id { get; set; }

        //public string email { get; set; }
        private string _email;
        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string email
        {
            get { return _email; }
            set { _email = value;
            OnPropertyChanged(nameof(email));
                ValidateProperty(value);
            }
        }


        private string _name;
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name should have min 3 characters and max 30")]
        [Display(Name = "Name")]
        public string name
        {
            get { return _name; }
            set { _name = value;
                
                OnPropertyChanged(nameof(name));
                ValidateProperty(value);
            }
        }


        public string languageID { get; set; }
        //[Required]
        //public string contactNo { get; set; }
        private string _contactNo;
        [Required]
        [Phone]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact Number must be 10 Digits")]
        [Display(Name = "Contact Number")]
        public string contactNo
        {
            get { return _contactNo; }
            set {
                _contactNo = value;
                OnPropertyChanged(nameof(contactNo));
                ValidateProperty(contactNo);
            }
        }

        //[Required]
        //public string organizationName { get; set; }
       
        private string _organizationName;
        [Required]
        [Display(Name = "Organization Name")]
        public string organizationName
        {
            get { return _organizationName; }
            set { _organizationName = value;
            OnPropertyChanged(organizationName);
                ValidateProperty(value);
            }
        }

        public string departmentName { get; set; }
        //[Required]
        //public string city { get; set; }
       
        private string _city;
        [Required]
        [Display(Name = "City")]
        public string city
        {
            get { return _city; }
            set { _city = value;
                OnPropertyChanged(city);
                ValidateProperty(value);
            }
        }

        public string token { get; set; }
        public int roleID { get; set; }
        public bool isActive { get; set; }
        public bool isApproved { get; set; }
    }
    public class RegisterResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
        public Register userData { get; set; }
    }
}
