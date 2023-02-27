using Microsoft.EntityFrameworkCore;
using HogwartsBackEndAPIs.Models;
using HogwartsBackEndAPIs.Services.Contract;

namespace HogwartsBackEndAPIs.Services.Implementation
{

    public class HouseService : IHouseService
    {
        private DbhogwartsContext _dbContext;
        public HouseService(DbhogwartsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<House>> GetList()
        {
            try {
                List<House> list = new List<House>();
                list = await _dbContext.Houses.ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
