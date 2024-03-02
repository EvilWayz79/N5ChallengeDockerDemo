using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using N5ChallengeDockerDemo.Controllers;
using N5ChallengeDockerDemo.Interfaces;
using N5ChallengeDockerDemo.Tools;
using N5ChallengeDockerDemo.ViewClasses;
using Nest;
using System.Data;

namespace N5ChallengeDockerDemo.Data
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public string ModifyPermission(int permissionId, string name, ILogger<ModifyPermissionController> _logger,
            IElasticClient elasticClient)
        {
            string returnString = GlobalData.TRANSACT_ERROR;

            try
            {
                /*
                 	@PermissionId int,
	                @Name nvarchar(50)
                 */
                string storedProcedure = "ModifyPermission";
                string parameterNameA = "@PermissionID";
                string parameterNameB = "@Name";

                var connection = _context.Database.GetConnectionString();

                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(parameterNameA, SqlDbType.Int).Value = permissionId;
                        cmd.Parameters.Add(parameterNameB, SqlDbType.NVarChar).Value = name;

                        con.Open();

                        int conteoId = cmd.ExecuteNonQuery();

                        if (conteoId > 0)
                            returnString = GlobalData.TRANSACT_OK;
                        elasticClient.IndexDocument(_context.Permissions.Find(permissionId));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GlobalData.DATABASE_ERROR);
                returnString = GlobalData.DATABASE_ERROR;
            }


            return returnString;
        }

        /// <summary>
        /// Creates a new permission for an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="permissionTypeId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string RequestPermission(int employeeId, int permissionTypeId, string name, ILogger<RequestPermissionController> _logger,
            IElasticClient elasticClient)
        {
            string returnString = GlobalData.TRANSACT_ERROR;

            try
            {
                string storedProcedure = "RequestPermission";
                string parameterNameA = "@EmployeeId";
                string parameterNameB = "@PermissionID";
                string parameterNameC = "@Name";

                var connection = _context.Database.GetConnectionString();

                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(parameterNameA, SqlDbType.Int).Value = employeeId;
                        cmd.Parameters.Add(parameterNameB, SqlDbType.Int).Value = permissionTypeId;
                        cmd.Parameters.Add(parameterNameC, SqlDbType.NVarChar).Value = name;

                        con.Open();

                        int newId = cmd.ExecuteNonQuery();

                        if (newId != 0)
                        {
                            elasticClient.IndexDocument(_context.Permissions.Find(newId));
                            returnString = GlobalData.TRANSACT_OK;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GlobalData.DATABASE_ERROR);
                returnString = GlobalData.DATABASE_ERROR;
            }


            return returnString;
        }

        /// <summary>
        /// Get an Employee's permissions
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<EmployeePermissionView> GetPermissions(int employeeId, ILogger<GetPermissionsController> _logger, IElasticClient elasticClient)
        {
            List<EmployeePermissionView> lEmpView = new List<EmployeePermissionView>();

            try
            {

                string storedProcedure = "GetPermissions";
                string parameterNameA = "@EmployeeId";

                var connection = _context.Database.GetConnectionString();

                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(parameterNameA, SqlDbType.Int).Value = employeeId;

                        con.Open();

                        SqlDataReader idr = cmd.ExecuteReader();

                        if (idr.HasRows)
                        {
                            while (idr.Read())
                            {
                                EmployeePermissionView epv = new EmployeePermissionView
                                {
                                    PermissionName = idr["perName"].ToString(),
                                    EmployeeName = idr["empName"].ToString(),
                                    PermissionDescription = idr["ptName"].ToString()
                                };
                                lEmpView.Add(epv);

                                //SELECT e.name as empName, p.name as perName, pt.name as ptName, p.PermissionId, e.EmployeeId, pt.PermissionTypeId
                                int permissionId = int.Parse(idr["PermissionId"].ToString());

                                elasticClient.IndexDocument(_context.Permissions.Find(permissionId));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex, GlobalData.DATABASE_ERROR);
            }


            return lEmpView;
        }
    }
}
