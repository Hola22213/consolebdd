using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolebdd.Models
{
    public class Course : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        [Column("CourseID")]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }
        
        public int DepartmentId { get; set; }

        // Propiedades de navegación
        public virtual Department? Department { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<CourseAssignment>? CourseAssignments { get; set; }

        public Course()
        {
        }
    }
}
