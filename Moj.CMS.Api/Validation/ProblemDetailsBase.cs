using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Filters;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Api.Validation
{
    public class ProblemDetailsBase : ProblemDetails, IHasIgnoredProperties
    {
        public ProblemDetailsBase()
        {
            Errors = new List<string>();
            Messages = new List<string>();
        }

        public ProblemDetailsBase(Guid requestId) : this()
        {
            RequestId = requestId;
        }

        public Guid RequestId { get; set; }
        public IList<string> Errors { get; set; }
        public IList<string> Messages { get; set; }
        public IDictionary<string, object> Data => new Dictionary<string, object>
        {
            { nameof(Title).ToLower(), Title },
            { nameof(Type).ToLower(), Type },
            { nameof(Status).ToLower(), Status },
            { nameof(Detail).ToLower(), Detail }
        };

        public bool succeeded => false;

        public IEnumerable<string> GetIgnoredProperties()
        {
            return new List<string>
            {
                nameof(Type),
                nameof(Title),
                nameof(Status),
                nameof(Detail),
                nameof(Instance)
            };
        }
    }
}