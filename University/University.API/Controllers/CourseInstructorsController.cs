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
        /// <summary>
        /// Obtiene los objetos de CourseInstructors
        /// </summary>
        /// <returns>Listado de los objetos de courseInstructors</returns>
        /// <response code="200">Ok. Devuelve el listado de objetos solicitado.</response>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var courseInstructors = await courseInstructorService.GetAll();
            //AutoMapper
            var courseInstructorsDTO = courseInstructors.Select(x => _mapper.Map<CourseInstructorResponseDTO>(x));
            return Ok(courseInstructorsDTO);
        }
        #endregion

        #region GET BY ID
        /// <summary>
        /// Obtiene un objeto por su Id.
        /// </summary>
        /// <remarks>
        /// Aquí una descripción mas larga si fuera necesario. Obtiene un objeto por su Id.
        /// </remarks>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto CourseInstructor</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)//se devuelve un DTO
        {
            var courseInstructor = await courseInstructorService.GetById(id);
            if (courseInstructor == null)
            {
                return NotFound();
            }

            var courseInstructorDTO = _mapper.Map<CourseInstructorResponseDTO>(courseInstructor);

            return Ok(courseInstructorDTO);
        }
        #endregion

        #region INSERT
        /// <summary>
        /// Crear un objeto de courseInstructor
        /// </summary>
        /// <param name="courseInstructorDTO">Objeto del courseInstructor</param>
        /// <returns>Objeto de courseInstructor</returns>
        /// <response code="200">Ok. Crea el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Insert(CourseInstructorRequestDTO courseInstructorDTO)//se devuelve un modelo
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

        #region PUT
        /// <summary>
        /// Modifica un objeto de courseInstructor
        /// </summary>
        /// <param name="courseInstructorDTO">Objeto del courseInstructor</param>
        /// <returns>Objeto de courseInstructor</returns>
        /// <response code="200">Ok. Modifica el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="404">NotFound. No se encuentra el recurso solicitado.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPut]
        public async Task<IHttpActionResult> Edit(CourseInstructorRequestDTO courseInstructorDTO, int id)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (courseInstructorDTO.ID != id)
            {
                return BadRequest();
            }

            var flag = await courseInstructorService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }

            try
            {
                var courseInstructor = _mapper.Map<CourseInstructor>(courseInstructorDTO);
                courseInstructor = await courseInstructorService.Update(courseInstructor);
                return Ok(courseInstructor);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region DELETE
        /// <summary>
        /// Elimina un objeto por su id 
        /// </summary>
        /// <param name="id">Id del Objeto</param>
        /// <response code="200">Ok. Elimina el objeto solicitado.</response>
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)//se devuelve un DTO
        {
            var flag = await courseInstructorService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }
            try
            {
                    await courseInstructorService.Delete(id);
                    return Ok(); 
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }
        #endregion

    }
}
