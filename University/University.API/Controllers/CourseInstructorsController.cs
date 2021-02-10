using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;

namespace University.API.Controllers
{
    public class CourseInstructorsController : ApiController
    {
        private IMapper _mapper;
        private readonly CourseInstructorService courseInstructorService = new CourseInstructorService(new CourseInstructorRepository(UniversityContext.Create()));
        public CourseInstructorsController()
        {
            //crear mapper
            _mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        #region GET
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var courseInstructors = await courseInstructorService.GetAll();
            //AutoMapper
            var courseInstructorsDTO = courseInstructors.Select(x => _mapper.Map<CourseInstructorDTO>(x));
            return Ok(courseInstructorsDTO);
        }
        #endregion

        #region GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)//se devuelve un DTO
        {
            var courseInstructor = await courseInstructorService.GetById(id);
            if (courseInstructor == null)
            {
                return NotFound();
            }

            var courseInstructorDTO = _mapper.Map<CourseInstructorDTO>(courseInstructor);

            return Ok(courseInstructorDTO);
        }
        #endregion

        #region INSERT
        /// <summary>
        /// Crear un objeto de courseInstructor
        /// </summary>
        /// <param name="courseInstructorDTO">Objeto del courseInstructor</param>
        /// <returns>Objeto de courseInstructor</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Insert(CourseInstructorDTO courseInstructorDTO)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var courseInstructor = _mapper.Map<CourseInstructor>(courseInstructorDTO);
                courseInstructor = await courseInstructorService.Insert(courseInstructor);
                return Ok(courseInstructor);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

    }
}
