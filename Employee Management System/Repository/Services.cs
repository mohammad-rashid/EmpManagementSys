using Employee_Management_System.Context;
using Employee_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.Repository
{
    public class Services:IServices
    {
        private readonly AppDbContext _repository;
        public Services(AppDbContext repository)
        {
            _repository = repository;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            var result = _repository.employees;
            return result;
        }
        public Employee GetEmployeeById(int id)
        {
            var result = _repository.employees.Find(id);
            return result;
        }
        public Employee GetEmployeeByDepId(int DepId)
        {
            var result = _repository.employees.FirstOrDefault(a => a.DepartmentId == DepId);
            return result;
        }
        public void AddEmployee(Employee employee)
        {
            _repository.employees.Add(employee);
            _repository.SaveChanges();
        }
        public void DeleteEmployee(int id)
        {
            var empData = _repository.employees.Find(id);
            _repository.employees.Remove(empData);
            _repository.SaveChanges();
        }
        public void PutEmployeeData(Employee employee)
        {
            var result = _repository.employees.Find(employee.Id);
            result.Name = employee.Name;
            result.Surname = employee.Surname;
            result.Address = employee.Address;
            result.Qualification = employee.Qualification;
            result.Contact = employee.Contact;
            result.DepartmentId = employee.DepartmentId;
            _repository.SaveChanges();
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            var result = _repository.departments;
            return result;
        }

        public Department GetDepartmentById(int id)
        {
            var result = _repository.departments.Find(id);
            return result;
        }

        public void AddDepartment(Department department)
        {
            _repository.Add(department);
            _repository.SaveChanges(true);
        }

        public void PutDepartmentData(Department department)
        {
            var result = _repository.departments.Find(department.DepartmentId);
            result.DepartmentName = department.DepartmentName;
            _repository.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }
    }
}
