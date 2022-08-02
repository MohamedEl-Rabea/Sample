using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Claims.ClaimProfile.Tabs
{
    public partial class ClaimDashboard
    {
        protected int Index = -1; //default value cannot be 0 -> first selectedindex is 0.
        protected int dataSize = 4;
        protected double[] data = { 77, 25, 20, 5 };
        protected string[] labels = { "Uranium", "Plutonium", "Thorium", "Caesium", "Technetium", "Promethium",
                        "Polonium", "Astatine", "Radon", "Francium", "Radium", "Actinium", "Protactinium",
                        "Neptunium", "Americium", "Curium", "Berkelium", "Californium", "Einsteinium", "Mudblaznium" };

        protected Random random = new Random();

        void RandomizeData()
        {
            var new_data = new double[dataSize];
            for (int i = 0; i < new_data.Length; i++)
                new_data[i] = random.NextDouble() * 100;
            data = new_data;
        }
        protected override async Task OnInitializedAsync()
        {
            RandomizeData();

            await base.OnInitializedAsync();
        }
    }
}
