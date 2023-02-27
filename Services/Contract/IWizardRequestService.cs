using HogwartsBackEndAPIs.Models;

namespace HogwartsBackEndAPIs.Services.Contract
{
    public interface IWizardRequestService
    {
        Task<List<WizardRequest>> GetList();
        Task<WizardRequest> Get(int WizardId);
        Task<WizardRequest> Add(WizardRequest wizardRequest);
        Task<bool> Update(WizardRequest wizardRequest);
        Task<bool> Delete(WizardRequest wizardRequest);
    }
}
