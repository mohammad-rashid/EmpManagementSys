using Employee_Management_System.Context;
using Employee_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystemTest
{

    [TestClass]
    public class EmployeeUnitTest
    {

        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "SampleEmployeeDb")
            .Options;

        AppDbContext context;

        //This Runs before test
        [TestInitialize]
        public void Setup()
        {
             

        context=new AppDbContext(dbContextOptions);
        context.Database.EnsureCreated();

            SeedDatabase();
        }
        [TestCleanup]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            context.departments.Add(new Department { DepartmentId = 1, DepartmentName = "CEO" });
            context.departments.Add(new Department { DepartmentId = 2, DepartmentName = "HR" });
            context.departments.Add(new Department { DepartmentId = 3, DepartmentName = "DEV" });
            context.employees.Add(new Employee { Id = 1, Name = "Bruce Wayne", Surname = "Bruce", Address = "Gotham", Qualification = "Billionaire", Contact = "1234567890", DepartmentId = 3 });
            context.employees.Add(new Employee { Id = 2, Name = "Jason Bourne", Surname = "Bourne", Address = "Paris", Qualification = "Blackbriar", Contact = "9876543210", DepartmentId = 1 });
            context.employees.Add(new Employee { Id = 3, Name = "John Wick", Surname = "John", Address = "Brooklyn", Qualification = "Baba Yaga", Contact = "9191919191", DepartmentId = 1 });
            context.SaveChanges();
        }
        private void EnterEmpData(Employee employee)
        {
            context = new AppDbContext(dbContextOptions);
            context.employees.Add(employee);
            if (employee.DepartmentId == 0)
            {
                throw new AssertFailedException("Department ID is required!");
            }
            context.SaveChanges();
        }

        //Test Methods:
        [TestMethod]
        public void Total_Number_of_Employees()
        {
            context = new AppDbContext(dbContextOptions);
            var totalEmployees = context.employees.Count();
            Assert.AreEqual(3, totalEmployees);
        }
        [TestMethod]
        public void Total_No_Of_Emp_By_Department()
        {
            context = new AppDbContext(dbContextOptions);
            var totalEmpByDep = context.employees.Count(a => a.DepartmentId == 1);
            Assert.AreEqual(2, totalEmpByDep);
        }
        [TestMethod]
        public void Get_Employee_Data_By_Id()
        {
            context = new AppDbContext(dbContextOptions);
            var emplyees = context.employees;
            var departments = context.departments;
            var joinData = emplyees.Join(
                departments,
                employee => employee.DepartmentId,
                department => department.DepartmentId,
                (emp,dep)=> new
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Surname = emp.Surname,
                    Contact = emp.Contact,
                    Department = emp.Department.DepartmentName
                } ).Where(a=>a.Id==1);
            Assert.IsNotNull(joinData);
        }
       
        [TestMethod]
        public void Add_Valid_Employee_Data()
        {
            EnterEmpData(new Employee { Id = 4, Name = "Testing", Surname = "test", Address = "qwerty", Contact = "123", Qualification = "xyz" });
            var count = context.employees.Count();
            Assert.AreEqual(4, count);
        }

    }
}
