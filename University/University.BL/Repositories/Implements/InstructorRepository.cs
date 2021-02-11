using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class InstructorRepository : GeneryRepository<Instructor>, IInstructorRepository
    {
        private readonly UniversityContext _universityContext;
        public InstructorRepository(UniversityContext universityContext) : base(universityContext)
        {
            _universityContext = universityContext;
        }

        public async Task<IEnumerable<Course>> GetCoursesByInstructor(int id)
        {

            //SELECT course.*
            //  FROM[University].[dbo].[CourseInstructor] courseInst
            //  JOIN[dbo].[Course] course ON course.CourseID = courseInst.CourseID --RELACION DE LA FK A LA PK
            //  WHERE courseInst.InstructorID = 5
            var courses = _universityContext.CourseInstructors
                            .Include("Course")
                            .Where(x => x.InstructorID == id)
                            .Select(x => x.Course);

            ////*****************OTRA FORMA DE HACER LA CONSULTA*****************
            //var _courses = (from courseInstructors in universityContext.CourseInstructors
            //                join course in universityContext.Courses on courseInstructors.CourseID
            //                equals course.CourseID
            //                where courseInstructors.InstructorID == id
            //                select course);
            return await courses.ToListAsync();
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            

            //LINQ
            var flag = await _universityContext.CourseInstructors.Where(x => x.InstructorID == id).AnyAsync() ||
                await _universityContext.Departments.Where(x => x.InstructorID == id).AnyAsync() ||
                await _universityContext.OfficeAssignments.Where(x => x.InstructorID == id).AnyAsync();
            return flag;
        }
    }
}
