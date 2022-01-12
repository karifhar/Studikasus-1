﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Data.Dtos;
using PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentService.Controllers
{
    [Route("api/p/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private IPayment _payment;
        private IMapper _mapper;

        public EnrollmentController(IPayment payment, IMapper mapper)
        {
            _payment = payment;
            _mapper = mapper;
        }
        // GET: api/<EnrollmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> Get()
        {
            var results = await _payment.GetAll();
            return Ok(results);
        }

        // GET api/<EnrollmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Get(int id)
        {
            var result = await _payment.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<EnrollmentController>
        [HttpPost]
        public ActionResult<string> Post()
        {
            Console.WriteLine("----Inbound-------");
            return Ok("test inbound");
            /*try
            {
                var dto = _mapper.Map<Payment>(entity);
                var result = await _payment.Insert(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }*/
        }

        // PUT api/<EnrollmentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Payment>> Put(int id, [FromBody] CreatePaymentDto entity)
        {
            try
            {
                var dto = _mapper.Map<Payment>(entity);
                var result = await _payment.Update(id, dto);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE api/<EnrollmentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                await _payment.Delete(id);
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
