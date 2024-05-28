using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolebdd.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorId { get; set; }

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string? Location { get; set; }

        // Propiedades de navegación
        public Instructor? Instructor { get; set; }
    }
}
