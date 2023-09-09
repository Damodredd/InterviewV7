namespace InterviewV7.Tools
{
    internal static class StringTools
    {
        public static string ToSentenceCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}