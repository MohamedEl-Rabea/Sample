using Moj.CMS.Domain.Shared.Enums;
using System;

namespace Moj.CMS.Application.AppServices.Promissory.Dtos
{
    public class PromissoryDto
    {
        public string PromissoryNumber { get; set; }
        public PromissoryTypeEnum PromissoryTypeId { get; set; }
        public DateTime PromissoryIssueDate { get; set; }
    }
}
