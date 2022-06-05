using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        
    }
}
