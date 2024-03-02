using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N5ChallengeDockerDemo.Interfaces;
using N5ChallengeDockerDemo.Tools;
using Nest;

namespace N5ChallengeDockerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestPermissionController : ControllerBase
    {
        private readonly ILogger<RequestPermissionController> _logger;
        private readonly IPermissionsRepository _permissionRepository;
        private readonly IElasticClient _elasticClient;

        public RequestPermissionController(ILogger<RequestPermissionController> logger, IPermissionsRepository permissionsRepository, IElasticClient elasticClient)
        {
            _logger = logger;
            _permissionRepository = permissionsRepository;
            _elasticClient = elasticClient;

        }


        [HttpPost]
        public IActionResult RequestPermission(int employeeId, int permissionTypeId, string name)
        {
            try
            {
                string newPermission = _permissionRepository.RequestPermission(employeeId, permissionTypeId, name, _logger, _elasticClient);
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
