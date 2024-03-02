using Microsoft.AspNetCore.Mvc;
using N5ChallengeDockerDemo.Interfaces;
using N5ChallengeDockerDemo.Tools;
using Nest;

namespace N5ChallengeDockerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPermissionsController : ControllerBase
    {
        private readonly ILogger<GetPermissionsController> _logger;
        private readonly IPermissionsRepository _permissionRepository;
        private readonly IElasticClient _elasticClient;

        public GetPermissionsController(ILogger<GetPermissionsController> logger, IPermissionsRepository permissionsRepository, IElasticClient elasticClient)
        {

            _logger = logger;
            _permissionRepository = permissionsRepository;
            _elasticClient = elasticClient;
        }

        /// <summary>
        /// Display the permissions an employee has
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet(Name = "EmployeeId")]
        public IActionResult GetPermissions(int employeeId)
        {
            try
            {
                var permissions = _permissionRepository.GetPermissions(employeeId, _logger, _elasticClient);
                _logger.LogInformation(GlobalData.DISPLAY_PERMISSIONS_BY_USER);

                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GlobalData.REQUEST_PERMISSION_ERROR);
                return BadRequest(GlobalData.REQUEST_PERMISSION_ERROR);
            }
        }

    }
}
