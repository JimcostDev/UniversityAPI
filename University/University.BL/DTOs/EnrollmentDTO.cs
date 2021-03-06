﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BL.DTOs
{
    public enum Grade
    {
        A, B, C, D, E
    }
    public class EnrollmentRequestDTO
    {
        

        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }
        public Grade Grade { get; set; } = Grade.A;

    }

    public class EnrollmentResponseDTO
    {


        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }
        public Grade Grade { get; set; } = Grade.A;


        //dependencias (modelos):
        public CourseDTO Course { get; set; }
        public StudentDTO Student { get; set; }
    }
}
