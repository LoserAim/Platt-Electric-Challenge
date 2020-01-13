using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassList.Models
{
    public class RPGClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ClassName { get; set; }
        [Required]
        public int HealthPoints { get; set; }
        [Required]
        public int Speed { get; set; }
        public string SpecialAbility { get; set; }

    }
}
