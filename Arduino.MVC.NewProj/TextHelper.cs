using System.Text.RegularExpressions;

namespace Arduino.MVC.NewProj
{
    internal class TextHelper
    {

        internal static void CheckValidViewName(string viewType, string viewName, int minViewNameLength)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentException($"'{nameof(viewName)}' cannot be null or whitespace.", nameof(viewName));
            }

            //int viewNamelength = 3;
            if (viewName.Length < minViewNameLength)
            {
                throw new Exception($"{viewType} names must be longer than {minViewNameLength - 1} characters.");
            }

            if (!char.IsLetter(viewName.ToCharArray()[0]))
            {
                throw new Exception($"{viewType} names must start with a letter character.");
            }
            
            if (!Regex.IsMatch(viewName, "[a-zA-Z][_a-zA-Z0-9]"))
            {
                throw new Exception($"Invalid {viewType} name {viewName}.");
            }
        }

        internal static string StartWithLCase(string stringValue)
        {
            if (stringValue.Length == 1)
            {
                stringValue = stringValue.ToLower();
            }
            else if (stringValue.Length > 1)
            {
                stringValue = stringValue.Substring(0, 1).ToLower() + stringValue.Substring(1);
            }

            return stringValue;
        }

        internal static string StartWithUCase(string stringValue)
        {
            if (stringValue.Length == 1)
            {
                stringValue = stringValue.ToUpper();
            }
            else if (stringValue.Length > 1)
            {
                stringValue = stringValue.Substring(0, 1).ToUpper() + stringValue.Substring(1);
            }

            return stringValue;
        }

        internal static string StripSuffix(string str, string suffix)
        {
            if (str.ToLower().EndsWith(suffix.ToLower()))
            {
                return str.Remove(str.Length - suffix.Length);
            }
            return str;
        }
    }
}
