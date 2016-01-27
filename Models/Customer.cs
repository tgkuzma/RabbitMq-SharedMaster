namespace Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
    }
}