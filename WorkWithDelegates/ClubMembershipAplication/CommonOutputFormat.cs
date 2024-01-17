
namespace ClubMembershipAplication
{
    public enum FrontTheme
    {
        Default,
        Danger,
        Seccess
    }

    public static class CommonOutputFormat
    {
        public static void ChangeFrontColor(FrontTheme frontTheme)
        {
            if(frontTheme == FrontTheme.Danger)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            if (frontTheme == FrontTheme.Seccess)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else
            {
                Console.ResetColor();
            }
        }
    }
}
