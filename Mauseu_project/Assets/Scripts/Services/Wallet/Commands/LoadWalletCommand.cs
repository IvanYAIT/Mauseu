using Services.Wallet;
using Shared.DataProvider.Commands;

namespace Services.TradeMarket.Commands
{
    public class LoadWalletCommand : LoadDataCommand<WalletData>
    {
        protected override string GetContainerName() => "WalletData.json";
    }
}