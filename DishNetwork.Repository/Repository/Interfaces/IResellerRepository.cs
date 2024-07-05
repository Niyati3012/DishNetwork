using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;


namespace DishNetwork.Repository.Repository.Interfaces
{
    public interface IResellerRepository
    {
        public string ResellerAddEdit(ResellerDetails resellerDetails);
        public ResellerDetails GetResellerDetail(int? resellerId);
        public List<Reseller> GetAllReseller();
        public String DeleteReseller(int resellerId);
    }
}
