using Services.TradeMarket.Commands;

namespace Services.Wallet
{
    public class WalletService : IWalletService
    {
        private int _money;

        public WalletService(int money) => _money = money;

        public int Get() => _money;

        public void Add(int amount)
        {
            _money += amount;
            Save();
        }

        public void Remove(int amount)
        {
            _money -= amount;
            Save();
        }

        private void Save()
        {
            new SaveWalletCommand(_money).Execute();
        }
    }
}