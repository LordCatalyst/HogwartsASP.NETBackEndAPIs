using Microsoft.EntityFrameworkCore;
using HogwartsBackEndAPIs.Models;
using HogwartsBackEndAPIs.Services.Contract;

namespace HogwartsBackEndAPIs.Services.Implementation
{
    public class WizardRequestService : IWizardRequestService
    {
        private DbhogwartsContext _dbContext;
        public WizardRequestService(DbhogwartsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WizardRequest>> GetList()
        {
            try {
                List<WizardRequest> list = new List<WizardRequest>();
                list = await _dbContext.WizardRequests.Include(house => house.House).ToListAsync();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<WizardRequest> Get(int WizardId)
        {
            try { 
                WizardRequest? found = new WizardRequest();
                found = await _dbContext.WizardRequests.Include(house => house.House).Where(e => e.WizardId == WizardId).FirstOrDefaultAsync();

                return found;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<WizardRequest> Add(WizardRequest wizardRequest)
        {
            try {
                _dbContext.WizardRequests.Add(wizardRequest);
                await _dbContext.SaveChangesAsync();
                return wizardRequest;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> Update(WizardRequest wizardRequest)
        {
            try {
                _dbContext.WizardRequests.Update(wizardRequest);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> Delete(WizardRequest wizardRequest)
        {
            try {
                _dbContext.WizardRequests.Remove(wizardRequest);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
