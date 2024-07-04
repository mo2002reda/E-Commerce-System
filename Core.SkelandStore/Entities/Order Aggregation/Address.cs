namespace SkelandStore.Core.Entities.Order_Aggregation
{
    public class Address
    {//this will be Address of The Order that different from Identity Address(Address of User)
        public Address()
        {

        }
        public Address(string lastName, string firstName, string city, string street, string country)
        {
            LastName = lastName;
            FirstName = firstName;
            City = city;
            Street = street;
            Country = country;
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }

    }
}
