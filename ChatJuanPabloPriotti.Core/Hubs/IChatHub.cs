using System.Threading.Tasks;

namespace ChatJuanPabloPriotti.Core.Hubs
{
    public interface IChatHub
    {
        Task SendMessage(string user, string message);
        bool CheckLogin();
        string GetCurrentUser();
        void ReadCSV(string stock_code);
        void ReadCSVCoupled(string stock_code);

    }

}
