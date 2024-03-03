using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using N5ChallengeDockerDemo.Interfaces;
using N5ChallengeDockerDemo.Services;
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

        //private readonly ProducerService _producerService;

        public GetPermissionsController(ILogger<GetPermissionsController> logger, IPermissionsRepository permissionsRepository, IElasticClient elasticClient/*, ProducerService producerService*/)
        {

            _logger = logger;
            _permissionRepository = permissionsRepository;
            _elasticClient = elasticClient;
            //_producerService = producerService;
        }

        /// <summary>
        /// Display the permissions an employee has
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet(Name = "EmployeeId")]
        //public async Task<IActionResult> GetPermissions([FromBody] int employeeId) // No functional Kafka docker instance available
        public async Task<IActionResult> GetPermissions(int employeeId)
        {
            try
            {
                var permissions = _permissionRepository.GetPermissions(employeeId, _logger, _elasticClient);
                _logger.LogInformation(GlobalData.DISPLAY_PERMISSIONS_BY_USER);

                string GUID = Guid.NewGuid().ToString();

                //await _producerService.ProduceAsync(GUID, ControllerContext.ActionDescriptor.ActionName); //kafka image unavailable

                if (permissions.IsNullOrEmpty())
                {
                    //empty
                    return Ok(GlobalData.EMPTY_RESULT);
                }
                else 
                {
                    //data
                    return Ok(permissions);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GlobalData.REQUEST_PERMISSION_ERROR);
                return BadRequest(GlobalData.REQUEST_PERMISSION_ERROR);
            }
        }

    }
}
