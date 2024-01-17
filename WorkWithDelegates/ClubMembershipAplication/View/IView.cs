using ClubMembershipAplication.FieldsValidator;

namespace ClubMembershipAplication.View
{
    public interface IView
    {
        void RunView();
        IFieldValidator FieldValidator { get; }
    }
}
