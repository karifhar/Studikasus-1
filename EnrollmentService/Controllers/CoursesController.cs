using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Data.DTO;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private ICourse _course;
        private IMapper _mapper;

        public CoursesController(ICourse course, IMapper mapper)
        {
            _course = course;
            _mapper = mapper;
        }
        // GET: CoursesController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseView>>> Index()
        {
            var results = await _course.GetAll();
            var data = _mapper.Map<IEnumerable<CourseView>>(results);
            return Ok(data);
        }

        // GET: CoursesController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseView>> Details(int id)
        {
            var result = await _course.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            var data = _mapper.Map<CourseView>(result);
            return Ok(data);
        }

        // GET: CoursesController/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CourseView>> Create([FromBody] CreateCourseDto entity)
        {
            try
            {
                var dto = _mapper.Map<Course>(entity);
                var result = await _course.Insert(dto);
                var data = _mapper.Map<CourseView>(result);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CourseView>> Update(int id, [FromBody] CreateCourseDto entity)
        {
            try
            {
                var dto = _mapper.Map<Course>(entity);
                var result = await _course.Update(id, dto);
                var data = _mapper.Map<CourseView>(result);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                await _course.Delete(id);
                return Ok(new
                {
                    Message = "Berhasil menghapus data"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
