namespace N5ChallengeDockerDemo.ViewClasses
{
    public class EmployeePermissionView
    {
        public string EmployeeName { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }


        public EmployeePermissionView()
        {
            EmployeeName = string.Empty;
            PermissionName = string.Empty;
            PermissionDescription = string.Empty;
        }

    }
}
