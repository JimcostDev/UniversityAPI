using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class OfficeAssignmentRequestDTO
    {
        [Required]
        [Display(Name = "Instructor")]
        public int InstructorID { get; set; }
        [Required]
        [Display(Name = "Location")]
        [StringLength(50)]
        public string Location { get; set; }

    }
    public class OfficeAssignmentResponseDTO
    {
        
        [Display(Name = "Instructor")]
        public int InstructorID { get; set; }
        [Display(Name = "Location")]
        [StringLength(50)]
        public string Location { get; set; }

        //dependencias (modelos):
        public InstructorDTO Instructor { get; set; }

    }
}
