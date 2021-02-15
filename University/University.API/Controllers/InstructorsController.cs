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
    public class InstructorsController : ApiController
    {
        private IMapper _mapper;
        private readonly InstructorService instructorService = new InstructorService(new InstructorRepository(UniversityContext.Create()));
        public InstructorsController()
        {
            //crear mapper
            _mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        #region GET
        /// <summary>
        /// Obtiene los objetos de Instructors
        /// </summary>
        /// <returns>Listado de los objetos de instructors</returns>
        /// <response code="200">Ok. Devuelve el listado de objetos solicitado.</response>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var instructors = await instructorService.GetAll();
            //AutoMapper
            var instructorsDTO = instructors.Select(x => _mapper.Map<InstructorDTO>(x));
            return Ok(instructorsDTO);
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
        /// <returns>Objeto Instructor</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)//se devuelve un DTO
        {
            var instructor = await instructorService.GetById(id);
            if (instructor == null)
            {
                return NotFound();
            }

            var instructorDTO = _mapper.Map<InstructorDTO>(instructor);

            return Ok(instructorDTO);
        }
        #endregion

        #region INSERT
        /// <summary>
        /// Crear un objeto de instructor
        /// </summary>
        /// <param name="instructorDTO">Objeto del instructor</param>
        /// <returns>Objeto de instructor</returns>
        /// <response code="200">Ok. Crea el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Insert(InstructorDTO instructorDTO)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var instructor = _mapper.Map<Instructor>(instructorDTO);
                instructor = await instructorService.Insert(instructor);
                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region PUT
        /// <summary>
        /// Modificar un objeto de Instructors
        /// </summary>
        /// <param name="instructorDTO">Objeto de Instructors</param>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto de enrollment</returns>
        /// <response code="200">Ok. Modifica el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPut]
        public async Task<IHttpActionResult> Edit(InstructorDTO instructorDTO, int id)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (instructorDTO.ID != id)
            {
                return BadRequest();
            }

            var flag = await instructorService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }

            try
            {
                var instructor = _mapper.Map<Instructor>(instructorDTO);
                instructor = await instructorService.Update(instructor);
                return Ok(instructor);
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
            var flag = await instructorService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }
            try
            {
                //si el objeto no esta relacionado en otra entidad, lo puede eliminar.
                if (!await instructorService.DeleteCheckOnEntity(id))
                {
                    await instructorService.Delete(id);
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
