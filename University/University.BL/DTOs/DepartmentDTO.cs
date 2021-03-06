﻿
using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class DepartmentRequestDTO
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public Decimal Budget { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        public int InstructorID { get; set; }
    }

    public class DepartmentResponseDTO
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public Decimal Budget { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int InstructorID { get; set; }

        //dependencias (modelos):
        public InstructorDTO Instructor { get; set; }
    }
}
