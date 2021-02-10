using University.BL.Models;
using University.BL.Repositories;

namespace University.BL.Services.Implements
{
    public class DepartmentService : GenericService<Department>, IDepartmentService
    {
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {

        }
    }
}