using Shared.DataProvider.Commands;

namespace Services.TradeMarket.Commands
{
    public class LoadWalletCommand : LoadDataCommand<int>
    {
        protected override int GetDefault() => 100;

        protected override string GetContainerName() => "WalletData.json";
    }
}