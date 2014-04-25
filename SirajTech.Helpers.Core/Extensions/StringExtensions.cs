using System.Text;

namespace SirajTech.Helpers.Core
{
    public static class StringExtensions
    {
        public static string Repeat(this string stringToRepeat, int repeat)
        {
            if (repeat <= 0)
                return stringToRepeat;

            var builder = new StringBuilder(repeat * stringToRepeat.Length);
            for (int i = 0; i < repeat; i++)
            {
                builder.Append(stringToRepeat);
            }
            return builder.ToString();
        }
    }
}
