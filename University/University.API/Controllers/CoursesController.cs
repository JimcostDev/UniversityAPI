using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;

namespace University.API.Controllers
{
    [Authorize]
    //[RoutePrefix("api/v1/Courses")]
    public class CoursesController : ApiController
    {
        private IMapper _mapper;
        private readonly CourseService courseService = new CourseService(new CourseRepository(UniversityContext.Create()));
        public CoursesController()
        {
            //crear mapper
            _mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        #region GET
        /// <summary>
        /// Obtiene los objetos de Cursos
        /// </summary>
        /// <returns>Listado de los objetos de cursos</returns>
        /// <response code="200">Ok. Devuelve el listado de objetos solicitado.</response>
        [HttpGet]
        //[Route("GetAll")]
        [ResponseType(typeof(IEnumerable<CourseDTO>))]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var courses = await courseService.GetAll();
            //AutoMapper
            var coursesDTO = courses.Select(x => _mapper.Map<CourseDTO>(x));
            return Ok(coursesDTO);
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
        /// <returns>Objeto Course</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        [ResponseType(typeof(CourseDTO))]
        public async Task<IHttpActionResult> GetById(int id)//se devuelve un DTO
        {
            var course = await courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseDTO = _mapper.Map<CourseDTO>(course);

            return Ok(courseDTO);
        }
        #endregion

        #region POST
        /// <summary>
        /// Crear un objeto de Curso
        /// </summary>
        /// <param name="courseDTO">Objeto de courseDTO</param>
        /// <returns>Objeto de course</returns>
        /// <response code="200">Ok. Inserta o crea el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Insert(CourseDTO courseDTO)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var course = _mapper.Map<Course>(courseDTO);
                course = await courseService.Insert(course);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region PUT
        /// <summary>
        /// Modifica un objeto de Curso
        /// </summary>
        /// <param name="courseDTO, id">Objeto de courseDTO, id del objeto</param>
        /// <returns>Objeto de course</returns>
        /// <response code="200">Ok. Modifica el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPut]
        public async Task<IHttpActionResult> Edit(CourseDTO courseDTO, int id)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (courseDTO.CourseID != id)
            {
                return BadRequest();
            }

            var flag = await courseService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }

            try
            {
                var course = _mapper.Map<Course>(courseDTO);
                course = await courseService.Update(course);
                return Ok(course);
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
            var flag = await courseService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }
            try
            {
                //si el objeto no esta relacionado en otra entidad, lo puede eliminar.
                if (!await courseService.DeleteCheckOnEntity(id))
                {
                    await courseService.Delete(id);
                    return Ok();
                }
                else
                {
                    throw new Exception("No se puede eliminar el objeto, esta relacionado en otra entidad.");
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }
        #endregion

    }
}
