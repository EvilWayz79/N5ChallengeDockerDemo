using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N5ChallengeDockerDemo.Interfaces;
using N5ChallengeDockerDemo.Tools;
using Nest;

namespace N5ChallengeDockerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModifyPermissionController : ControllerBase
    {
        private readonly ILogger<ModifyPermissionController> _logger;
        private readonly IPermissionsRepository _permissionRepository;
        private readonly IElasticClient _elasticClient;

        public ModifyPermissionController(ILogger<ModifyPermissionController> logger, IPermissionsRepository permissionsRepository, IElasticClient elasticClient)
        {
            _logger = logger;
            _permissionRepository = permissionsRepository;
            _elasticClient = elasticClient;

        }

        /// <summary>
        /// Modify permission to a employee, get permission id and Employee Name
        /// </summary>
        /// <param name="permissionId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ModifyPermission(int permissionId, string name)
        {
            try
            {
                string newPermission = _permissionRepository.ModifyPermission(permissionId, name, _logger, _elasticClient);
                _logger.LogInformation(GlobalData.REGISTER_NEW_PERMISSION);

                return Ok(newPermission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GlobalData.REQUEST_PERMISSION_ERROR);
                return BadRequest(GlobalData.REQUEST_PERMISSION_ERROR);
            }
        }
    }
}
