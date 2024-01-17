using ClubMembershipAplication.Data;
using ClubMembershipAplication.FieldsValidator;
using ClubMembershipAplication.View;

namespace ClubMembershipAplication
{
    public static class Factory
    {
        public static IView GetMainViewObject() 
        {
            ILogin login = new LoginUser();

            IRegister register = new RegisterUser();

            IFieldValidator userRegistrationValidation = new UserRegistrationValidator(register);
            userRegistrationValidation.InitialiseValidatorDelegates();

            IView registerView = new UserRegistrationView(register, userRegistrationValidation);
            IView loginView = new UserLoginView(login);
            IView mainView = new MainView(registerView, loginView);

            return mainView;
        }   
    }
}
