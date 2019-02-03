namespace Api.Identity.Domain
{
    public class AddressEntity
    {
        public int AddressId { get; set; } // primary key
        public int UserId { get; set; }    // foreign key
        public string PostalCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}
