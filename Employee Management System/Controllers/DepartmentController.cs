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
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _repository;
        public DepartmentController(AppDbContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.departments);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var keyvalueFromDb = _repository.departments.Find(id);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            return Ok(keyvalueFromDb);
        }

        [HttpPost]
        public IActionResult Post(Department inputData)
        {
           
            if (_repository.departments.Any(a => a.DepartmentName == inputData.DepartmentName))
            {
                return Conflict();
            }

            try
            {
                _repository.departments.Add(inputData);
                _repository.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Department inputData)
        {
            var dbData = _repository.departments.Find(id);
            if (dbData == null)
            {
                return NotFound();
            }

            try
            {
                dbData.DepartmentName = inputData.DepartmentName;
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
            var dbData = _repository.departments.Find(id);
            if (dbData == null)
            {
                return NotFound();
            }
            try
            {
                _repository.departments.Remove(dbData);
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
