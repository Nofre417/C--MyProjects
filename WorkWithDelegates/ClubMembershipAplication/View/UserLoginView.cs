using ClubMembershipAplication.Data;
using ClubMembershipAplication.FieldsValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipAplication.View
{
    public class UserLoginView : IView
    {
        public IFieldValidator FieldValidator => null;
        ILogin _loginUser = null;

        public UserLoginView(ILogin login)
        {
            _loginUser = login;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();
            CommonOutputText.WriteLoginHeading();

            Console.Write("Please enter your email address: ");
            string emailAddress = Console.ReadLine();

            Console.Write("Please enter your password: ");
            string password = Console.ReadLine();

            User user = _loginUser.Login(emailAddress, password);

            if(user != null)
            {
                WelcomeUserView _welcomeUserView = new WelcomeUserView(user);
                _welcomeUserView.RunView();
            }
            else
            {
                Console.Clear();
                CommonOutputFormat.ChangeFrontColor(FrontTheme.Danger);
                Console.WriteLine("The credentials that you entered do not match any records");
                CommonOutputFormat.ChangeFrontColor(FrontTheme.Default);
                Console.ReadKey(); 
            }
        }
    }
}
