
using System.Text.RegularExpressions;

namespace FieldsValidatorAPI
{
    public delegate bool RequiredValidDel(string fieldVal);
    public delegate bool StringLenValidDel(string fieldVal, int min, int max);
    public delegate bool DateValidDel(string fieldVal, out DateTime validDateTime);
    public delegate bool PatternMatchDel(string fieldVal, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare);
    public class CommonFieldsValidationFunctions
    {
        private static RequiredValidDel _requiredValidDel = null;
        private static StringLenValidDel _stringLenValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchDel _patternMathDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;

        public static RequiredValidDel RequiredFieldValidDel
        {
            get
            {
                if(_requiredValidDel == null )
                    _requiredValidDel  = new RequiredValidDel(RequiredFieldValid);
                return _requiredValidDel;
            }
        }
        public static StringLenValidDel StringLengthValidDel
        {
            get
            {
                if (_stringLenValidDel == null)
                    _stringLenValidDel = new StringLenValidDel(StringLenFieldValid);
                return _stringLenValidDel;
            }
        }
        public static DateValidDel DateFieldValidDel
        {
            get
            {
                if (_dateValidDel == null)
                    _dateValidDel = new DateValidDel(DateFieldValid);
                return _dateValidDel;
            }
        }
        public static PatternMatchDel PatternMatchFieldValidDel
        {
            get
            {
                if (_patternMathDel == null)
                    _patternMathDel = new PatternMatchDel(FieldPatterValid);
                return _patternMathDel;
            }
        }
        public static CompareFieldsValidDel CompareFieldsValidDel
        {
            get
            {
                if (_compareFieldsValidDel == null)
                    _compareFieldsValidDel = new CompareFieldsValidDel(FieldCompressionValid);
                return _compareFieldsValidDel;
            }
        }


        private static bool RequiredFieldValid(string fieldVal)
        {
            if(!string.IsNullOrEmpty(fieldVal))
            {
                return true;
            }
            return false;
        }

        private static bool StringLenFieldValid(string fieldVal, int min, int max)
        {
            if(fieldVal.Length >= min && fieldVal.Length <= max){
                return true;
            }
            return false;
        }
        private static bool DateFieldValid(string dateTime, out DateTime validDateTime)
        {
            if(DateTime.TryParse(dateTime, out validDateTime))
            {
                return true;
            }
            return false;
        }
        private static bool FieldPatterValid(string fieldVal, string patternExpressionPattern)
        {
            Regex regex = new(patternExpressionPattern);

            if(regex.IsMatch(fieldVal))
            {
                return true;
            }
            return false;
        }

        private static bool FieldCompressionValid(string fieldVal, string fieldValCompare)
        {
            if (fieldVal.Equals(fieldValCompare))
            {
                return true;
            }
            return false;
        }
    }
}
