using Shared.DataProvider.Commands;

namespace Services.TradeMarket.Commands
{
    public class SaveWalletCommand : SaveDataCommand<int>
    {
        public SaveWalletCommand(int data) : base(data)
        {
        }

        protected override string GetContainerName() => "WalletData.json";
    }
}