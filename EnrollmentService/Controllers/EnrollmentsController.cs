using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Data.DTO;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatformService.SyncDataServices.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private IEnrollment _enroll;
        private IMapper _mapper;
        private IPaymentDataClient _paymentDataClient;

        public EnrollmentsController(IEnrollment enrollment, IMapper mapper, IPaymentDataClient paymentDataClient)
        {
            _enroll = enrollment;
            _mapper = mapper;
            _paymentDataClient = paymentDataClient;
        }
        // GET: api/<EnrollmentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> Get()
        {
            var results = await _enroll.GetAll();
            return Ok(results);
        }

        // GET api/<EnrollmentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> Get(int id)
        {
            var result = await _enroll.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<EnrollmentsController>
        [HttpPost]
        public async Task<ActionResult<Enrollment>> Post([FromBody] CreateEnrollDto entity)
        {
            try
            {
                var dto = _mapper.Map<Enrollment>(entity);
                var result = await _enroll.Insert(dto);
                await _paymentDataClient.SendPlatformToCommand(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT api/<EnrollmentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Enrollment>> Put(int id, [FromBody] CreateEnrollDto entity)
        {
            try
            {
                var dto = _mapper.Map<Enrollment>(entity);
                var result = await _enroll.Update(id, dto);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE api/<EnrollmentsController>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                await _enroll.Delete(id);
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
