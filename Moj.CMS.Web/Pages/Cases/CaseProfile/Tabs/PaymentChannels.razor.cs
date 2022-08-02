using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public class PaymentChannelsDto
    {
        public string ClaimNumber { get; set; }
        public int CreditorPayOrder { get; set; }
        public int CreditorCollection { get; set; }

        public int BankAccount { get; set; }
        public int NominalTransfer { get; set; }
        public int Check { get; set; }

        public int PayRight { get; set; }

        public int Report { get; set; }
    }

    public partial class PaymentChannels
    {
        public List<PaymentChannelsDto> PaymentChannelsDtoList { get; set; } = new List<PaymentChannelsDto>();

        protected override async Task OnInitializedAsync()
        {
            PaymentChannelsDtoList.AddRange(new List<PaymentChannelsDto>
            {
                new PaymentChannelsDto
                {
                    ClaimNumber = "مطالبه 1",
                },
                new PaymentChannelsDto
                {
                    ClaimNumber = "مطالبه 2",
                },
                new PaymentChannelsDto
                {
                    ClaimNumber = "مطالبه 3",
                }
            });
            await base.OnInitializedAsync();
        }
    }
}
