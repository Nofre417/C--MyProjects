namespace ClubMembershipAplication.Data
{
    public interface ILogin
    {
        User Login(string emailAddress,  string password);
    }
}
