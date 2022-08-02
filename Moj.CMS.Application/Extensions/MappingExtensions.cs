using Moj.CMS.Application.Dtos;
using System.Collections.Generic;

namespace Moj.CMS.Application.Extensions
{
    public static class MappingExtensions
    {
        public static ResourceCreatedDto MapToResourceCreatedDto(this IEnumerable<int> input)
        {
            return new ResourceCreatedDto
            {
                CraetedIds = input
            };
        }
    }
}
