using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class StudentRepository : GeneryRepository<Student>, IStudentRepository
    {
        private readonly UniversityContext _universityContext;
        public StudentRepository(UniversityContext universityContext) : base(universityContext)
        {
            _universityContext = universityContext;
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudent(int id)
        {
            //SELECT course.*
            //   FROM[University].[dbo].[Enrollment] Enroll
            //   JOIN[dbo].[Course] course ON course.CourseID = Enroll.CourseID -- RELACION DE LA FK A LA PK
            //   WHERE Enroll.StudentID = 1009
            var courses = _universityContext.Enrollments
                            .Include("Course")
                            .Where(x => x.StudentID == id)
                            .Select(x => x.Course);

            return await courses.ToListAsync();
        }
    }
}
