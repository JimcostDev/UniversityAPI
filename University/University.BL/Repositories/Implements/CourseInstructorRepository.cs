﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class CourseInstructorRepository : GeneryRepository<CourseInstructor>, ICourseInstructorRepository
    {
        private readonly UniversityContext _universityContext;
        public CourseInstructorRepository(UniversityContext universityContext) : base(universityContext)
        {
            _universityContext = universityContext;
        }

        public new async Task<IEnumerable<CourseInstructor>> GetAll()
        {
            var enrollments = _universityContext.CourseInstructors.Include("Course").Include("Instructor");
            return await enrollments.ToListAsync();
        }
    }
}
