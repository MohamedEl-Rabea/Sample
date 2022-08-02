using System.Threading.Tasks;
using Moj.CMS.Shared.Settings;

namespace Moj.CMS.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();
    }
}
