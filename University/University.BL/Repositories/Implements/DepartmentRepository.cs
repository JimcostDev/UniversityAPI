using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class DepartmentRepository : GeneryRepository<Department>, IDepartmentRepository
    {
        private readonly UniversityContext _universityContext;
        public DepartmentRepository(UniversityContext universityContext) : base(universityContext)
        {
            _universityContext = universityContext;
        }
        public new async Task<IEnumerable<Department>> GetAll()
        {
            var department = _universityContext.Departments.Include("Instructor");
            return await department.ToListAsync();
        }
    }
}
