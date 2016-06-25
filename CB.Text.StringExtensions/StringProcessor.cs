using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace CB.Text.StringExtensions
{
    public static class StringProcessor
    {
        #region Methods
        public static string Camelize(this string s)
        {
            return PascalizeOrCamelize(s, false);
        }

        public static string ChangeNotationTo(this string s, NotationType notationType)
        {
            switch (notationType)
            {
                case NotationType.Camelize:
                    return Camelize(s);

                case NotationType.Pascalize:
                    return Pascalize(s);

                case NotationType.Underscore:
                    return Underscore(s);

                default:
                    throw new NotImplementedException();
            }
        }

        public static string Pascalize(this string s)
        {
            return PascalizeOrCamelize(s, true);
        }

        public static string RegexReplace(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }

        public static string RegexReplace(this string input, string pattern, MatchEvaluator evaluator)
        {
            return Regex.Replace(input, pattern, evaluator);
        }

        public static string RegexReplace(this string input, string pattern, string replacement, RegexOptions options)
        {
            return Regex.Replace(input, pattern, replacement, options);
        }

        public static string RegexReplace(this string input, string pattern, MatchEvaluator evaluator,
            RegexOptions options)
        {
            return Regex.Replace(input, pattern, evaluator, options);
        }

        public static string RegexReplace(this string input, string pattern, string replacement, RegexOptions options,
            TimeSpan matchTimeout)
        {
            return Regex.Replace(input, pattern, replacement, options, matchTimeout);
        }

        public static string RegexReplace(this string input, string pattern, MatchEvaluator evaluator,
            RegexOptions options,
            TimeSpan matchTimeout)
        {
            return Regex.Replace(input, pattern, evaluator, options, matchTimeout);
        }

        public static string Standardize(this string s)
        {
            return s.Trim()
                    .RegexReplace(@"\s+(\p{P})", m => m.Groups[1].Value)
                    .RegexReplace(@"\s+", " ")
                    .RegexReplace(@"^\w|[.?!] \w", m =>
                    {
                        var match = m.Value;
                        return match.Substring(0, match.Length - 1) + char.ToUpper(match.Last());
                    });
        }

        public static string Underscore(this string s)
        {
            var words = Regex.Split(s, @"\W+").Where(word => word != "");
            var uncapitalizedWords = words.Select(w => Capitalize(w, false));
            return string.Join("_", uncapitalizedWords);
        }
        #endregion


        #region Implementation
        private static string Capitalize(string s, bool capitalizing)
        {
            var firstChar = capitalizing ? char.ToUpper(s[0]) : char.ToLower(s[0]);
            return firstChar + s.Substring(1);
        }

        private static string PascalizeOrCamelize(string s, bool pascalizing)
        {
            var capitalizedTrailing = TrimEnd(s).RegexReplace(@"\W+\w", m => char.ToUpper(m.Value.Last()).ToString());
            return Capitalize(capitalizedTrailing, pascalizing);
        }

        private static string TrimEnd(string s, string pattern = @"\W+")
        {
            return Regex.Replace(s, pattern + "$", "");
        }
        #endregion
    }
}