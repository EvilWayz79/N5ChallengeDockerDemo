using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace N5ChallengeDockerDemo.Models
{
    [Table("Employee", Schema = "dbo")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("employeeId")]        
        public int EmployeeId { get; set; }

        [Column("employeeName")]        
        public string EmployeeName { get; set; }

        [Column("permissions")]
        public ICollection<Permission> Permissions { get; set; }
    }
}
