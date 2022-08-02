using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Shared.Values
{
    public class Document : ValueObject
    {
        private Document()
        {

        }

        public Guid DocumentId { get; private set; }
        public string DocumentUrl { get; private set; }

        public static Document Create(Guid? documentId, string documentUrl)
        {
            return new Document
            {
                DocumentId = documentId ?? Guid.NewGuid(),
                DocumentUrl = documentUrl
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DocumentId;
            yield return DocumentUrl;
        }
    }
}
