using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace N5ChallengeDockerDemo.Models
{
    [Table("Permission", Schema = "dbo")]
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("permissionid")]        
        public int PermissionId { get; set; }

        [Column("permissionName")]
        public string PermissionName { get; set; }

        [Column("permissionType")]
        public PermissionType PermissionType { get; set; }

        [Column("employee")]
        public Employee Employee { get; set; }
    }
}
