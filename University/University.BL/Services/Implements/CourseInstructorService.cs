using University.BL.Models;
using University.BL.Repositories;

namespace University.BL.Services.Implements
{
    public class CourseInstructorService : GenericService<CourseInstructor>, ICourseInstructorService
    {
        public CourseInstructorService(ICourseInstructorRepository courseInstructorRepository) : base(courseInstructorRepository)
        {

        }
    }
}
