using System.Collections.Generic;
using System.Text;

namespace SirajTech.Helpers.Core
{
    public static class ViolationExtensions
    {
        public static string JoinViolationMessages(this IEnumerable<Violation> violations)
        {
            var sb = new StringBuilder();
            foreach (var violation in violations)
            {
                sb.AppendLine(string.Format("|*| Code:{0} in {1}", violation.ErrorCode, violation.PropertyName));
                if (!string.IsNullOrWhiteSpace(violation.FriendlyMessage))
                    sb.AppendLine(string.Format("   Message : {0}", violation.FriendlyMessage));
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
