using Employee_Management_System.Context;
using Employee_Management_System.Models;
using Employee_Management_System.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EmployeeController : Controller
    {
        private readonly IServices _repository;
        public EmployeeController(IServices repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var keyvalueFromDb = _repository.GetEmployeeById(id);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            return Ok(keyvalueFromDb);
        }

        [HttpGet("Dep/{id}")]
        public IActionResult GetEmployeeByDepId(int id)
        {
            var keyvalueFromDb = _repository.GetEmployeeByDepId(id);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            return Ok(keyvalueFromDb);
        }

        [HttpPost]
        public IActionResult Post(Employee inputData)
        {
           
            var allEmp = _repository.GetAllEmployees();
            if (allEmp.Any(a => a.Name == inputData.Name && a.Contact == inputData.Contact))
            {
                return Conflict();
            }

            try
            {
                _repository.AddEmployee(inputData);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Employee inputData)
        {
            var dbData = _repository.GetEmployeeById(id);
            if (dbData == null)
            {
                return NotFound();
            }

            try
            {
                _repository.PutEmployeeData(inputData);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dbData = _repository.GetEmployeeById(id);
            if (dbData == null)
            {
                return NotFound();
            }
            try
            {
                _repository.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
