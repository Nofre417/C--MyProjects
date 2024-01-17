
namespace FieldsValidatorAPI
{
    public static class CommonRegularExpressionPatterns
    {
        public const string Email_Address_Regex_Pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //public const string Phone_Address_Regex_Pattern = @"^\+?(\d ?)(\(\d{3}\)|\d{3})( \d+)*$";
        public const string Phone_Address_Regex_Pattern = @"^[2-9]\d{2}-\d{3}-\d{4}$";
        public const string Password_Regex_Pattern = @"^[a-zA-Z]\w{3,14}$";
    }

}
