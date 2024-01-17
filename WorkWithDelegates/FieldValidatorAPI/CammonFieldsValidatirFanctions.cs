using System.Reflection.Metadata;

namespace FieldValidatorAPI
{
    public delegate bool RequiredValidDel(string fieldVal);
    public delegate bool StringLengValidDel(string fieldVal, int min, int max);
    public delegate bool DateValidDel(string fieldVal, out DateTime validDateTime);
    public delegate bool PatternMathDel(string fieldVal, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldVal, string filedValCompare);

    public class CammonFieldsValidatirFanctions
    {
        public static RequiredValidDel _requiredValidDel = null;
        public static StringLengValidDel _stringLengValidDel = null;
        public static DateValidDel _dateValidDel = null;
        public static PatternMathDel _patternMathDel = null;
        public static CompareFieldsValidDel(string fieldVal, string filedValCompare);

    }
}
