using ClubMembershipAplication.Data;
using ClubMembershipAplication.FieldsValidator;

namespace ClubMembershipAplication.View
{
    public class UserRegistrationView : IView
    {
        IFieldValidator _fieldValidator = null;

        IRegister _register = null;

        public IFieldValidator FieldValidator { get => _fieldValidator; }

        public UserRegistrationView(IRegister register, IFieldValidator fieldValidator)
        {
            _fieldValidator = fieldValidator;
            _register = register;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();
            CommonOutputText.WriteRegistrationHeading(); 

            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.EmailAddress] = GetInputFromUser(FieldConstants.UserRegistrationField.EmailAddress, "Please enter email:");
            CommonOutputText.WriteSplitterLine();
            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.FirstName] = GetInputFromUser(FieldConstants.UserRegistrationField.FirstName, "Please enter first name:");
            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.LastName] = GetInputFromUser(FieldConstants.UserRegistrationField.LastName, "Please enter last name:");
            CommonOutputText.WriteSplitterLine();
            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.Password] = GetInputFromUser(FieldConstants.UserRegistrationField.Password, $"Password first character must be a letter{Environment.NewLine}It must contain at least 4 characters and no more than 15 characters{Environment.NewLine}No characters other than letters, numbers and the underscore{Environment.NewLine}Please enter password:");
            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.PasswordCompare] = GetInputFromUser(FieldConstants.UserRegistrationField.PasswordCompare, $"{Environment.NewLine}Please reenter password:");
            CommonOutputText.WriteSplitterLine();
            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.DateOfBirth] = GetInputFromUser(FieldConstants.UserRegistrationField.DateOfBirth, "Enter date of birth:");
            CommonOutputText.WriteSplitterLine();
            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.PhoneNumber] = GetInputFromUser(FieldConstants.UserRegistrationField.PhoneNumber, "Enter phone number:");
            CommonOutputText.WriteSplitterLine();
            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.City] = GetInputFromUser(FieldConstants.UserRegistrationField.City, "Enter city:");
            _fieldValidator.FieldArray[(int) FieldConstants.UserRegistrationField.Address] = GetInputFromUser(FieldConstants.UserRegistrationField.Address, "Enter address:");

            RegisterUser();
            Console.Write("Press key to login ");
            Console.ReadKey();

        }

        private void RegisterUser()
        {
            _register.Register(_fieldValidator.FieldArray);

            CommonOutputText.WriteSplitterLine();
            CommonOutputFormat.ChangeFrontColor(FrontTheme.Seccess);
            Console.WriteLine("You have successfully registered. Please press any key to login");
            CommonOutputFormat.ChangeFrontColor(FrontTheme.Default);
        }

        private string GetInputFromUser(FieldConstants.UserRegistrationField field, string promptText)
        {
            string fieldVal = "";

            do
            {
                Console.Write(promptText + " ");
                fieldVal = Console.ReadLine();
            } while (!FieldValid(field, fieldVal));

            return fieldVal;
        } 

        private bool FieldValid(FieldConstants.UserRegistrationField field, string fieldValue)
        {
            if (!_fieldValidator.ValidatorDel((int) field, fieldValue, _fieldValidator.FieldArray, out string invalidMassage)) 
            {
                CommonOutputFormat.ChangeFrontColor(FrontTheme.Danger);

                Console.WriteLine(invalidMassage);

                CommonOutputFormat.ChangeFrontColor(FrontTheme.Default);

                return false;
            }


            return true;
        }
    }
}
