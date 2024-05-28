using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolebdd.Models
{
    public class Instructor : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [StringLength(50)]
        public string? FirstMidName { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public string FullName => LastName + ", " + FirstMidName;

        // Propiedades de navegación
        public ICollection<CourseAssignment> CourseAssignments { get; private set; } = new List<CourseAssignment>();
        public OfficeAssignment? OfficeAssignment { get; private set; }
    }
}
