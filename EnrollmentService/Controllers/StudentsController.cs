using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Data.DTO;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudent _student;
        private IMapper _mapper;

        public StudentsController(IStudent student, IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentView>>> GetAll()
        {
            var results = await _student.GetAll();
            var data = _mapper.Map<IEnumerable<StudentView>>(results);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentView>> GetById(int id)
        {
            var result = await _student.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            var data = _mapper.Map<StudentView>(result);
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult<StudentView>> Add([FromBody]CreateStudentDto entity) 
        {
            try
            {
                var dto = _mapper.Map<Student>(entity);
                var result = await _student.Insert(dto);
                var data = _mapper.Map<StudentView> (result);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentView>> Update(int id, [FromBody]CreateStudentDto entity)
        {
            try
            {
                var dto = _mapper.Map<Student>(entity);
                var result = await _student.Update(id, dto);
                var data = _mapper.Map<StudentView>(result);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                await _student.Delete(id);
                return Ok(new
                {
                    Message = "Berhasil menghapus data"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
