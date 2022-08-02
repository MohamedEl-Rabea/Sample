using System.Threading.Tasks;

namespace Moj.CMS.Integration.Contracts.AlAhli_B2B
{
    public interface IAlahliClient
    {
        Task<string> CreateVIban(VIbanCreationRequest vIbanData); 
    }
}
