namespace Classes.Response.PermissionResponse
{
    public class Permission
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
