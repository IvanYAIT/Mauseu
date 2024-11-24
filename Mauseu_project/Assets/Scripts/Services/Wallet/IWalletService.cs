namespace Services.Wallet
{
    public interface IWalletService
    {
        public int Get();
        public void Add(int amount);
        public void Remove(int amount);
    }
}
