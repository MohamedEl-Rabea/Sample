using System.Collections.Generic;

namespace Moj.CMS.Domain.Shared.Values
{
    public class LocalizedText : ValueObject
    {
        public string EN { get; private set; }
        public string AR { get; private set; }

        private LocalizedText()
        {

        }

        public LocalizedText(string en, string ar)
        {
            EN = en;
            AR = ar;
        }

        public static implicit operator string(LocalizedText localizedText)
        {
            if (localizedText == null)
                return null;

            var text = localizedText.CurrentCultureText;
            return text;
        }

        public string CurrentCultureText => (string)GetType()
                .GetProperty(System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper())
                .GetValue(this, null);


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return EN;
            yield return AR;
        }
    }
}
