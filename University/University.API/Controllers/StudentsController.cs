using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;

namespace University.API.Controllers
{
    public class StudentsController : ApiController
    {
        private IMapper _mapper;
        private readonly StudentService studentService = new StudentService(new StudentRepository(UniversityContext.Create()));
        public StudentsController()
        {
            //crear mapper
            _mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        #region GET
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var students = await studentService.GetAll();
            //AutoMapper
            var studentsDTO = students.Select(x => _mapper.Map<StudentDTO>(x));
            return Ok(studentsDTO);
        }
        #endregion

        #region GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)//se devuelve un DTO
        {
            var student = await studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentDTO = _mapper.Map<StudentDTO>(student);
          
            return Ok(studentDTO);
        }
        #endregion

        #region INSERT
        [HttpPost]
        public async Task<IHttpActionResult> Insert(StudentDTO studentDTO)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var student = _mapper.Map<Student>(studentDTO);
                student = await studentService.Insert(student);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        } 
        #endregion

    }
}
