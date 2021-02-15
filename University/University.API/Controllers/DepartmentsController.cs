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
    public class DepartmentsController : ApiController
    {
        private IMapper _mapper;
        private readonly DepartmentService departmentService = new DepartmentService(new DepartmentRepository(UniversityContext.Create()));
        public DepartmentsController()
        {
            //crear mapper
            _mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        #region GET
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var departments = await departmentService.GetAll();
            //AutoMapper
            var departmentsDTO = departments.Select(x => _mapper.Map<DepartmentDTO>(x));
            return Ok(departmentsDTO);
        }
        #endregion

        #region GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)//se devuelve un DTO
        {
            var department = await departmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }

            var departmentDTO = _mapper.Map<DepartmentDTO>(department);

            return Ok(departmentDTO);
        }
        #endregion

        #region INSERT
        /// <summary>
        /// Crear un objeto de department
        /// </summary>
        /// <param name="departmentDTO">Objeto del department</param>
        /// <returns>Objeto de department</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Insert(DepartmentDTO departmentDTO)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var department = _mapper.Map<Department>(departmentDTO);
                department = await departmentService.Insert(department);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region PUT
        /// <summary>
        /// Modificar un objeto de department
        /// </summary>
        /// <param name="departmentDTO">Objeto del department</param>
        /// <returns>Objeto de department</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPut]
        public async Task<IHttpActionResult> Edit(DepartmentDTO departmentDTO, int id)//se devuelve un modelo
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (departmentDTO.DepartmentID != id)
                {
                    return BadRequest();
                }

                var flag = await departmentService.GetById(id);
                if (flag == null)
                {
                    return NotFound();
                }


                var department = _mapper.Map<Department>(departmentDTO);
                department = await departmentService.Update(department);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await departmentService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }
            try
            {
                await departmentService.Delete(id);
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
