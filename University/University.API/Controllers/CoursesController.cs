﻿using AutoMapper;
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
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()//se devuelve un DTO
        {
            var courses = await courseService.GetAll();
            //AutoMapper
            var coursesDTO = courses.Select(x => _mapper.Map<CourseDTO>(x));
            return Ok(coursesDTO);
        }
        #endregion

        #region GET BY ID
        [HttpGet]
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
