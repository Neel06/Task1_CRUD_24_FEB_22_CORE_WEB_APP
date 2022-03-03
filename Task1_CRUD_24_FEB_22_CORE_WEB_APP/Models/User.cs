using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_CRUD_24_FEB_22_CORE_WEB_APP.Models
{
    public class User
    {
        
        public int Id { get; set; }

        [DisplayName("First Name")]
        public String First_name { get; set; }
        
        [DisplayName("Last Name")]
        public String Last_name { get; set; }
        
        [DisplayName("Mobile No")]
        public long Mobile_No { get; set; }

        public string Email { get; set; }

        public int Pincode { get; set; }
        

    }
}
