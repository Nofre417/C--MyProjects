using ClubMembershipAplication.Data;
using FieldsValidatorAPI;

namespace ClubMembershipAplication.FieldsValidator
{
    public class UserRegistrationValidator : IFieldValidator
    {
        const int FirstName_Min_Length = 2;
        const int FirstName_Max_Length = 100;
        const int LastName_Min_Length = 2;
        const int LastName_Max_Length = 100;

        delegate bool EmailExistDel(string emailAddress);

        FieldValidatorDel _fieldValidatorDel = null;

        RequiredValidDel _requiredValidDel = null;
        StringLenValidDel _stringLenValidDel = null;
        DateValidDel _dateValidDel = null;
        PatternMatchDel _patternMathDel = null;
        CompareFieldsValidDel _compareFieldsValidDel = null;

        EmailExistDel _emailExistDel = null;

        string[] _fieldArray = null;
        IRegister _register = null;

        public string[] FieldArray
        {
            get
            {
                if (_fieldArray == null)
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];
                return _fieldArray;
            }
        }

        public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

        public UserRegistrationValidator(IRegister register)
        {
            _register = register;
        }

        public void InitialiseValidatorDelegates()
        {
            _fieldValidatorDel = new FieldValidatorDel(ValidField);
            _emailExistDel = new EmailExistDel(_register.EmailExists); 

            _requiredValidDel = CommonFieldsValidationFunctions.RequiredFieldValidDel;
            _stringLenValidDel = CommonFieldsValidationFunctions.StringLengthValidDel;
            _dateValidDel = CommonFieldsValidationFunctions.DateFieldValidDel;
            _patternMathDel = CommonFieldsValidationFunctions.PatternMatchFieldValidDel;
            _compareFieldsValidDel = CommonFieldsValidationFunctions.CompareFieldsValidDel;
        }

        private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMassage)
        {
            fieldInvalidMassage = "";

            FieldConstants.UserRegistrationField userRegistrationField = (FieldConstants.UserRegistrationField) fieldIndex;

            switch (userRegistrationField)
            {
                case FieldConstants.UserRegistrationField.EmailAddress:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField),userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMassage = (fieldInvalidMassage == "" && !_patternMathDel(fieldValue, CommonRegularExpressionPatterns.Email_Address_Regex_Pattern)) ? $"You must enter a valid email address{Environment.NewLine}" : fieldInvalidMassage;
                    fieldInvalidMassage = (fieldInvalidMassage == "" && _emailExistDel(fieldValue)) ? $"User with the same email address already exists{Environment.NewLine}" : fieldInvalidMassage;

                    break;
                case FieldConstants.UserRegistrationField.FirstName:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMassage = (fieldInvalidMassage == "" && !_stringLenValidDel(fieldValue, FirstName_Min_Length, FirstName_Max_Length)) ? $"The length for field {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {FirstName_Min_Length} and {FirstName_Max_Length}{Environment.NewLine}" : fieldInvalidMassage;
                    break;
                case FieldConstants.UserRegistrationField.LastName:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMassage = (fieldInvalidMassage == "" && !_stringLenValidDel(fieldValue, LastName_Min_Length, LastName_Max_Length)) ? $"The length for field {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {LastName_Min_Length} and {LastName_Max_Length}{Environment.NewLine}" : fieldInvalidMassage;
                    break;
                case FieldConstants.UserRegistrationField.Password:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMassage = (fieldInvalidMassage == "" && !_patternMathDel(fieldValue, CommonRegularExpressionPatterns.Password_Regex_Pattern)) ? $"Your password must have at least 4 characters and no more then 15 characters and no characters other than letters, numbers and the underscore{Environment.NewLine}" : fieldInvalidMassage;
                    break;
                case FieldConstants.UserRegistrationField.PasswordCompare:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMassage = (fieldInvalidMassage == "" && !_compareFieldsValidDel(fieldValue, fieldArray[(int)FieldConstants.UserRegistrationField.Password])) ? $"Your entry password did not match your password{Environment.NewLine}" : fieldInvalidMassage;
                    break;
                case FieldConstants.UserRegistrationField.DateOfBirth:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMassage = (fieldInvalidMassage == "" && !_dateValidDel(fieldValue, out DateTime validDateTime)) ? $"You did not enter valid date{Environment.NewLine}" : fieldInvalidMassage;
                    break;
                case FieldConstants.UserRegistrationField.PhoneNumber:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMassage = (fieldInvalidMassage == "" && !_patternMathDel(fieldValue, CommonRegularExpressionPatterns.Phone_Address_Regex_Pattern)) ? $"Your entry phone number incorrect{Environment.NewLine}" : fieldInvalidMassage;
                    break;
                case FieldConstants.UserRegistrationField.Address:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    break;
                case FieldConstants.UserRegistrationField.City:
                    fieldInvalidMassage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    break;
                default:
                    throw new ArgumentException("This field does not exists !");
                    
            }

            return (fieldInvalidMassage == "");
        }
    }

}       