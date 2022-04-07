using System.ComponentModel.DataAnnotations;

namespace AdvancedProgramming.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Department Department { get; set; }
    }
}
