using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domian.Entities.DB.Users
{
    [Table("tbl_Users")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// Username
        /// </summary>
       
        [Display(Name = "Username")]
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>

        [Display(Name = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// Password
        /// </summary>

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
