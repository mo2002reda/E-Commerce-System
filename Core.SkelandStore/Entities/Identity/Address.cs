namespace SkelandStore.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string AppUserId { get; set; }//The Id in IdentityUser Table Is string(Guid)
        //FK For AppUser Cause The Relation Between AppUser & Address =>1 To 1 and mandatory From Address Side 
        public AppUser UserId { get; set; }
    }
}