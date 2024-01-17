using ClubMembershipAplication.FieldsValidator;

namespace ClubMembershipAplication.View
{
    public class WelcomeUserView : IView
    {
        User _user = null;

        public WelcomeUserView(User user) 
        {
            _user = user;
        }

        public IFieldValidator FieldValidator => null;

        public void RunView()
        {
            Console.Clear();
            CommonOutputText.WriteMainHeading();

            CommonOutputText.WriteSplitterLine();
            CommonOutputFormat.ChangeFrontColor(FrontTheme.Seccess);
            Console.WriteLine($"Hi {_user.FirstName} {_user.LastName}{Environment.NewLine}Welcome to the Football Club");
            CommonOutputFormat.ChangeFrontColor(FrontTheme.Default);
            CommonOutputText.WriteSplitterLine();
            Console.ReadKey();
        }
    }
}
