using System.Text.RegularExpressions;

namespace Tremplin.Core.Helpers
{
    public static class SocialSecurityNumberHelper
    {
        public static string AddBlankSpacesInSocialSecurityNumber(string socialSecurityNumber)
        {
            socialSecurityNumber = Regex.Replace(socialSecurityNumber, @"(\w{1})(\w{2})(\w{2})(\w{2})(\w{3})(\w{3})(\w{2})", @"$1 $2 $3 $4 $5 $6 $7");

            return socialSecurityNumber;
        }

        public static string RemoveBlankSpacesInSocialSecurityNumber(string socialSecurityNumber)
        {
            socialSecurityNumber = Regex.Replace(socialSecurityNumber, @"\s", "");

            return socialSecurityNumber;
        }
    }
}