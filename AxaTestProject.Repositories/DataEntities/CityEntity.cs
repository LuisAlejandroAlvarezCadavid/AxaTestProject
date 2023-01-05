using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaTestProject.Repositories.DataEntities
{
    public class CityEntity
    {
        [Required]
        [Key]
        public int? CityId { get; set; }

        [Required]
        [MaxLength(80)]
        public string? CityName { get; set; }
    }
}
