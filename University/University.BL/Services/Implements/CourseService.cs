using System.Collections.Generic;
using System.Threading.Tasks;
using University.BL.Models;
using University.BL.Repositories;

namespace University.BL.Services.Implements
{
    public class CourseService : GenericService<Course>, ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository) : base(courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            var flag = await _courseRepository.DeleteCheckOnEntity(id);
            return flag;
        }

        public async Task<IEnumerable<Student>> GetStudentByCourses(int id)
        {
            return await _courseRepository.GetStudentByCourses(id);
        }
    }
}
