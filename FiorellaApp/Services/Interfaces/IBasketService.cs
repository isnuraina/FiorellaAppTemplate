using FiorellaApp.ViewModels;

namespace FiorellaApp.Services.Interfaces
{
    public interface IBasketService
    {
        int GetBasketCount();
        List<BasketVM> GetBasketList();
    }
}
