using System;
using System.Text;
using static System.Console;

namespace ClubMembershipAplication
{
    public static class CommonOutputText
    {
        private static string MainHeading
        {
            get
            {
                string heading = "Football Club";
                return $"{heading}{Environment.NewLine}{new string('-', heading.Length)}";
            }
        }

        private static string RegistrationHeading
        {
            get
            {
                string heading = "Register";
                return $"{heading}{Environment.NewLine}{new string('-', heading.Length)}";
            }
        }
        private static string LogHeading
        {
            get
            {
                string heading = "Login";
                return $"{heading}{Environment.NewLine}{new string('-', heading.Length)}";
            }
        }
        public static void WriteMainHeading()
        {
            Clear();
            WriteLine(MainHeading + "\n\n");
        }

        public static void WriteLoginHeading()
        {
            WriteLine(LogHeading + "\n\n");
        }

        public static void WriteRegistrationHeading()
        {
            WriteLine(RegistrationHeading + "\n\n");
        }
        public static void WriteSplitterLine()
        {
            WriteLine("\n-----------------------------------------------------");
        }
    }
}
