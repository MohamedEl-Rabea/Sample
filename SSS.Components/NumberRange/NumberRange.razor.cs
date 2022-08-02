using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace SSS.Components.NumberRange
{
    public partial class NumberRange
    {
        [Parameter] public DecimalRange Range { get; set; }
        [Parameter] public string MinLabel { get; set; } = "Min Value";
        [Parameter] public string MaxLabel { get; set; } = "Max Value";
        [Parameter] public string Format { get; set; }
        [Parameter] public Variant Variant { get; set; } = Variant.Text;
        [Parameter] public ValueTypeEnum ValueType { get; set; }
        [Parameter] public EventCallback<DecimalRange> RangeChanged { get; set; }

        public string Icon { get; set; }

        private decimal? _start = null, _end;

        private DecimalRange _range = new DecimalRange();

        protected override void OnInitialized()
        {
            Icon = ValueType == ValueTypeEnum.Money
                 ? Icons.Material.Filled.AttachMoney
                 : "";

            base.OnInitialized();
        }

        private async Task HandleMinValueChanged(decimal? value)
        {
            _start = value;
            _range.Start = _start;
            await RangeChanged.InvokeAsync(_range);
        }

        private async Task HandleMaxValueChanged(decimal? value)
        {
            _end = value;
            _range.End = _end;
            await RangeChanged.InvokeAsync(_range);
        }
        public void Clear()
        {
            _range = null;
            _start = _end = null;
            RangeChanged.InvokeAsync(_range).GetAwaiter().GetResult();
        }
    }
}