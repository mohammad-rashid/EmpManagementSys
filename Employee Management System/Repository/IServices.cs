using Employee_Management_System.Models;
using System.Collections.Generic;

namespace Employee_Management_System.Repository
{
    public interface IServices
    {
        public IEnumerable<Employee> GetAllEmployees();
        public Employee GetEmployeeById(int id);
        public Employee GetEmployeeByDepId(int DepId);
        public void AddEmployee(Employee employee);
        public void DeleteEmployee(int id);
        public void PutEmployeeData(Employee employee);
        public IEnumerable<Department> GetAllDepartment();
        public Department GetDepartmentById(int id);
        public void AddDepartment(Department department);
        public void PutDepartmentData(Department department);
        public void DeleteDepartment(int id);


    }
}
