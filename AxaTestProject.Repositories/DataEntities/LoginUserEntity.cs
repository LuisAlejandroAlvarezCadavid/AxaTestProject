using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaTestProject.Repositories.DataEntities
{
    public class LoginUserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string? User { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }
    }
}
