namespace Moj.CMS.Shared.Helpers
{
    public static class RegularExpressions
    {
        public const string ArabicLettersOnly = "^[\u0621-\u0652 _-]+$";
        public const string DigitsOnly = @"^\d+$";
        public const string LettersAndNumbersOnly = @"^[a-zA-Z0-9]+$";
    }
}
