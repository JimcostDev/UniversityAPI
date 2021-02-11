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
    public class EnrollmentsController : ApiController
    {
        private IMapper _mapper;
        private readonly EnrollmentService enrollmentService = new EnrollmentService(new EnrollmentRepository(UniversityContext.Create()));
        public EnrollmentsController()
        {
            //crear mapper
            _mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        #region GET
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var enrollments = await enrollmentService.GetAll();
            //AutoMapper
            var enrollmentsDTO = enrollments.Select(x => _mapper.Map<EnrollmentDTO>(x));
            return Ok(enrollmentsDTO);
        }
        #endregion

        #region GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)//se devuelve un DTO
        {
            var enrollment = await enrollmentService.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentDTO = _mapper.Map<EnrollmentDTO>(enrollment);

            return Ok(enrollmentDTO);
        }
        #endregion

        #region INSERT
        /// <summary>
        /// Crear un objeto de enrollment
        /// </summary>
        /// <param name="instruenrollmentDTOctorDTO">Objeto del enrollment</param>
        /// <returns>Objeto de enrollment</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Insert(EnrollmentDTO enrollmentDTO)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentDTO);
                enrollment = await enrollmentService.Insert(enrollment);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region PUT
        [HttpPut]
        public async Task<IHttpActionResult> Edit(EnrollmentDTO enrollmentDTO, int id)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (enrollmentDTO.EnrollmentID != id)
            {
                return BadRequest();
            }
            var flag = await enrollmentService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }

            try
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentDTO);
                enrollment = await enrollmentService.Update(enrollment);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)//se devuelve un DTO
        {
            var flag = await enrollmentService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }
            try
            {
                await enrollmentService.Delete(id);
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
