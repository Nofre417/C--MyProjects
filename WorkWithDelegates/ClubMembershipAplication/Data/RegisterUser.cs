using ClubMembershipAplication.FieldsValidator;

namespace ClubMembershipAplication.Data
{
    public class RegisterUser : IRegister
    {
        public bool EmailExists(string emailAddress)
        {
            bool emailExists = false;

            using(var dbContext = new ClubMembershipDBContext())
            {
                emailExists = dbContext.Users.Any(u => u.EmailAddress.ToLower().Trim() == emailAddress.Trim().ToLower());
            }
            return emailExists;
        }

        public bool Register(string[] fields)
        {
            using(var dbContext = new ClubMembershipDBContext())
            {
                User user = new User
                {
                    EmailAddress = fields[(int)FieldConstants.UserRegistrationField.EmailAddress],
                    FirstName = fields[(int)FieldConstants.UserRegistrationField.FirstName],
                    LastName = fields[(int)FieldConstants.UserRegistrationField.LastName],
                    DateOfBirth = DateTime.Parse(fields[(int)FieldConstants.UserRegistrationField.DateOfBirth]),
                    PhoneNumber = fields[(int)FieldConstants.UserRegistrationField.PhoneNumber],
                    Password = fields[(int)FieldConstants.UserRegistrationField.Password],
                    Address = fields[(int)FieldConstants.UserRegistrationField.Address],
                    City = fields[(int)FieldConstants.UserRegistrationField.City]
                };

                dbContext.Users.Add(user);

                dbContext.SaveChanges();
            }
            return true;
        }
    }
}
