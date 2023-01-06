using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AxaTestProject.Repositories.DataEntities
{
    public class SoatDataEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Identification { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        public DateTime? InitDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string? PlateCar { get; set; }

        [Required]
        [MaxLength(30)]
        public string? CityName { get; set; }      

    }
}
