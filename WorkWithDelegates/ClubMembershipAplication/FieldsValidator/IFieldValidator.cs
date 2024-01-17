
namespace ClubMembershipAplication.FieldsValidator
{
    public delegate bool FieldValidatorDel(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMassage);

    public interface IFieldValidator
    {
        void InitialiseValidatorDelegates();

        string[] FieldArray { get; }

        FieldValidatorDel ValidatorDel { get; }
    }
}
