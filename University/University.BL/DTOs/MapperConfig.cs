
using AutoMapper;
using University.BL.Models;

namespace University.BL.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>();//GET
                cfg.CreateMap<CourseDTO, Course>();//POST, PUT

                cfg.CreateMap<Instructor, InstructorDTO>();
                cfg.CreateMap<InstructorDTO, Instructor>();

                cfg.CreateMap<Student, StudentDTO>();
                cfg.CreateMap<StudentDTO, Student>();

                cfg.CreateMap<Enrollment, EnrollmentDTO>();
                cfg.CreateMap<EnrollmentDTO, Enrollment>();

                cfg.CreateMap<OfficeAssignment, OfficeAssignmentResponseDTO>();
                cfg.CreateMap<OfficeAssignmentRequestDTO, OfficeAssignment>();

                cfg.CreateMap<Department, DepartmentDTO>();
                cfg.CreateMap<DepartmentDTO, Department>();

                cfg.CreateMap<CourseInstructor, CourseInstructorDTO>();
                cfg.CreateMap<CourseInstructorDTO, CourseInstructor>();
            });
        }
    }
}