using Velox.Api.Middleware.Enum;

namespace Velox.Api.Infrastructure.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public string Nationality { get; set; }
        public string Speciality {  get; set; }
        public string Speciality2 { get; set; }
        public string SportPriority1 { get; set; }
        public string SportPriority2 { get; set; }
        public string SportPriority3 { get; set; }
        public string InstitutionName { get; set; }
        public string InstitutionAddress { get; set; }
        public string InstitutionPinCode { get; set; }
    }
}
