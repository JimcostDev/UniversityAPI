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
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
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

    }
}
