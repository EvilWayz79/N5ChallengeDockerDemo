using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace N5ChallengeDockerDemo.Models
{
    [Table("PermissionType", Schema = "dbo")]
    public class PermissionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("permissionTypeid")]
        public int PermissionTypeId { get; set; }

        [Column("permissionTypename")]
        public string PermissionTypeName { get; set; }

        [Column("permissions")]
        public ICollection<Permission> Permissions { get; set; }
    }
}
