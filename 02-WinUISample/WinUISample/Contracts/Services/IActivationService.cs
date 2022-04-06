using System.Threading.Tasks;

namespace WinUISample.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(object activationArgs);
    }
}
