using ClubMembershipAplication.FieldsValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipAplication.View
{
    public class MainView : IView
    {
        public IFieldValidator FieldValidator => null;

        IView _registerView = null;
        IView _loginView = null;

        public MainView(IView registerView, IView loginView) 
        {
            _loginView = loginView;
            _registerView = registerView;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();

            Console.WriteLine($"Please press 'L' to login");
            Console.WriteLine($"Please press 'R' to register");

            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.L)
            {
                RunLoginView();
            }
            else if(key == ConsoleKey.R)
            {
                RunRegistrationView();
                RunLoginView();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Goodbye");
                Console.ReadKey();
            }
        }

        private void RunRegistrationView()
        {
            _registerView.RunView();
        }

        private void RunLoginView()
        {
            _loginView.RunView();
        }
    }
}
