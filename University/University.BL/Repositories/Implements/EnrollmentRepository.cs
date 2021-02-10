using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class EnrollmentRepository : GeneryRepository<Enrollment>, IEnrollmentRepository
    {
        private readonly UniversityContext _universityContext;
        public EnrollmentRepository(UniversityContext universityContext) : base(universityContext)
        {
            _universityContext = universityContext;
        }
        public new async Task<IEnumerable<Enrollment>> GetAll()
        {
            var enrollments = _universityContext.Enrollments.Include("Student");
            return await enrollments.ToListAsync();
        }
    }
}
