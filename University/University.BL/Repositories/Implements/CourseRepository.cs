using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class CourseRepository : GeneryRepository<Course>, ICourseRepository
    {
        private readonly UniversityContext _universityContext;
        public CourseRepository(UniversityContext universityContext) : base(universityContext)
        {
            _universityContext = universityContext;
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            //LINQ
            var flag = await _universityContext.CourseInstructors.Where(x => x.CourseID == id).AnyAsync() ||
            await _universityContext.Enrollments.Where(x => x.CourseID == id).AnyAsync();
            return flag;
        }


        //SELECT student.*
        //  FROM[dbo].[Enrollment] Enroll
        //  JOIN [dbo].[Student] student ON student.ID = Enroll.StudentID-- RELACION DE LA FK A LA PK
        //  WHERE CourseID = 1045
        public async Task<IEnumerable<Student>> GetStudentByCourses(int id)
        {
            var students = _universityContext.Enrollments
                            .Include("Student")
                            .Where(x => x.CourseID == id)
                            .Select(x => x.Student);

            return await students.ToListAsync();
        }
    }
}
