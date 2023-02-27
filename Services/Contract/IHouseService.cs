using HogwartsBackEndAPIs.Models;

namespace HogwartsBackEndAPIs.Services.Contract
{
    public interface IHouseService
    {
        Task<List<House>> GetList();
    }
}
