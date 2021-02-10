using University.BL.Models;
using University.BL.Repositories;

namespace University.BL.Services.Implements
{
    public class EnrollmentService : GenericService<Enrollment>, IEnrollmentService
    {
        public EnrollmentService(IEnrollmentRepository enrollmentRepository) : base(enrollmentRepository)
        {

        }
    }
}
