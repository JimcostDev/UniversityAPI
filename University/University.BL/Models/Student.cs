﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.BL.Models
{
    [Table("Student", Schema = "dbo")]
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        
        public DateTime EnrollmentDate { get; set; }

        //Esta referenciado en las siguientes entidades:
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
