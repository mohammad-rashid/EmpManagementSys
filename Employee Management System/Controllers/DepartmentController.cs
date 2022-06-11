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
    public class DepartmentController : Controller
    {
        private readonly IServices _repository;
        public DepartmentController(IServices repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllDepartment());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var keyvalueFromDb = _repository.GetDepartmentById(id);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            return Ok(keyvalueFromDb);
        }

        [HttpPost]
        public IActionResult Post(Department inputData)
        {
            var result = _repository.GetAllDepartment();
           
            if (result.Any(a => a.DepartmentName == inputData.DepartmentName))
            {
                return Conflict();
            }

            try
            {
                _repository.AddDepartment(inputData);
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
            var dbData = _repository.GetDepartmentById(id);
            if (dbData == null)
            {
                return NotFound();
            }

            try
            {
                _repository.PutDepartmentData(inputData);
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
            var dbData = _repository.GetDepartmentById(id);
            if (dbData == null)
            {
                return NotFound();
            }
            try
            {
                _repository.DeleteDepartment(id);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
