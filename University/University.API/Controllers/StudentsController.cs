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
        /// <summary>
        /// Obtiene los objetos de Students
        /// </summary>
        /// <returns>Listado de los objetos de students</returns>
        /// <response code="200">Ok. Devuelve el listado de objetos solicitado.</response>
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
        /// <summary>
        /// Obtiene un objeto por su Id.
        /// </summary>
        /// <remarks>
        /// Aquí una descripción mas larga si fuera necesario. Obtiene un objeto por su Id.
        /// </remarks>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto Student</returns>
        /// <response code="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
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
        /// <summary>
        /// Crear un objeto de Student
        /// </summary>
        /// <param name="studentDTO">Objeto del officeAssignment</param>
        /// <returns>Objeto de officeAssignment</returns>
        /// <response code="200">Ok. Crea el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
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

        #region PUT
        /// <summary>
        /// Modificar un objeto de Student
        /// </summary>
        /// <param name="studentDTO">Objeto de officeAssignment</param>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto de enrollment</returns>
        /// <response code="200">Ok. Modifica el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validación del modelo.</response>
        /// <response code="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPut]
        public async Task<IHttpActionResult> Edit(StudentDTO studentDTO, int id)//se devuelve un modelo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (studentDTO.ID != id)
            {
                return BadRequest();
            }

            var flag = await studentService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }

            try
            {
                var student = _mapper.Map<Student>(studentDTO);
                student = await studentService.Update(student);
                return Ok(student);
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
            var flag = await studentService.GetById(id);
            if (flag == null)
            {
                return NotFound();
            }
            try
            {
                //si el objeto no esta relacionado en otra entidad, lo puede eliminar.
                if (!await studentService.DeleteCheckOnEntity(id))
                {
                    await studentService.Delete(id);
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
