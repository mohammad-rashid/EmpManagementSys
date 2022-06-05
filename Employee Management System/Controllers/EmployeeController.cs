using Employee_Management_System.Context;
using Employee_Management_System.Models;
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
        AppDbContext _repository;
        public EmployeeController(AppDbContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.employees);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var keyvalueFromDb = _repository.employees.Find(id);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            return Ok(keyvalueFromDb);
        }

        [HttpGet("Dep/{id}")]
        public IActionResult GetEmployeeByDepId(int id)
        {
            var keyvalueFromDb = _repository.employees.Where(a=>a.DepartmentId == id);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            return Ok(keyvalueFromDb);
        }

        [HttpPost]
        public IActionResult Post(Employee inputData)
        {
            if (_repository.employees.Any(a => a.Name == inputData.Name && a.Contact == inputData.Contact))
            {
                return Conflict();
            }

            try
            {
                _repository.employees.Add(inputData);
                _repository.SaveChanges();
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
            var dbData = _repository.employees.Find(id);
            if (dbData == null)
            {
                return NotFound();
            }

            try
            {
                dbData.Name = inputData.Name;
                dbData.Surname = inputData.Surname;
                dbData.Address = inputData.Address;
                dbData.Qualification = inputData.Qualification;
                dbData.Contact = inputData.Contact;
                dbData.DepartmentId = inputData.DepartmentId;

                _repository.SaveChanges();
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
            var dbData = _repository.employees.Find(id);
            if (dbData == null)
            {
                return NotFound();
            }
            try
            {
                _repository.employees.Remove(dbData);
                _repository.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
