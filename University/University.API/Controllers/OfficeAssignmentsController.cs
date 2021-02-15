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
    public class OfficeAssignmentsController : ApiController
    {
        private IMapper _mapper;
        private readonly OfficeAssignmentService officeAssignmentService = new OfficeAssignmentService(new OfficeAssignmentRepository(UniversityContext.Create()));
        public OfficeAssignmentsController()
        {
            //crear mapper
            _mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        #region GET
        /// <summary>
        /// Obtiene los objetos de OfficeAssignments
        /// </summary>
        /// <returns>Listado de los objetos de officeAssignments</returns>
        /// <response code="200">Ok. Devuelve el listado de objetos solicitado.</response>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var officeAssignments = await officeAssignmentService.GetAll();
            //AutoMapper
            var officeAssignmentsDTO = officeAssignments.Select(x => _mapper.Map<OfficeAssignmentDTO>(x));
            return Ok(officeAssignmentsDTO);
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
        /// <returns>Objeto OfficeAssignment</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)//se devuelve un DTO
        {
            var officeAssignment = await officeAssignmentService.GetById(id);
            if (officeAssignment == null)
            {
                return NotFound();
            }

            var officeAssignmentDTO = _mapper.Map<OfficeAssignmentDTO>(officeAssignment);

            return Ok(officeAssignmentDTO);
        }
        #endregion

        #region INSERT
        /// <summary>
        /// Crear un objeto de officeAssignmen
        /// </summary>
        /// <param name="officeAssignmentDTO">Objeto del officeAssignment</param>
        /// <returns>Objeto de officeAssignment</returns>
        /// <response code="200">Ok. Crea el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Insert(OfficeAssignmentDTO officeAssignmentDTO)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var officeAssignment = _mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                officeAssignment = await officeAssignmentService.Insert(officeAssignment);
                return Ok(officeAssignment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region PUT
        /// <summary>
        /// Modificar un objeto de OfficeAssignment
        /// </summary>
        /// <param name="officeAssignmentDTO">Objeto de officeAssignment</param>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto de enrollment</returns>
        /// <response code="200">Ok. Modifica el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPut]
        public async Task<IHttpActionResult> Edit(OfficeAssignmentDTO officeAssignmentDTO, int id)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (officeAssignmentDTO.InstructorID != id)
            {
                return BadRequest();
            }

            var flag = await officeAssignmentService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }

            try
            {
                var officeAssignment = _mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                officeAssignment = await officeAssignmentService.Update(officeAssignment);
                return Ok(officeAssignment);
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
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await officeAssignmentService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }
            try
            {
                await officeAssignmentService.Delete(id);
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
