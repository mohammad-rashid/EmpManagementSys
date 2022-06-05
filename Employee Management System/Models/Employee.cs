using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string Contact { get; set; }
        [Required(ErrorMessage ="Department ID is Required!")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
