using System.Collections.Generic;
using System.Linq;
using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Items;
using Services.Inventory.Monsters;
using Services.TradeMarket;
using Services.Wallet;
using TMPro;
using UnityEngine;

namespace TradeMarket.Dialogs.TradeMarket
{
    public class TradeMarketDialog : DialogBase
    {
        [SerializeField] private TMP_Text _wallet;
        [SerializeField] private MonsterView _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private MonstersData _data;

        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();
        private static ITradeService TradeService => Locator.Get<ITradeService>();
        private static IWalletService WalletService => Locator.Get<IWalletService>();

        private readonly List<MonsterView> _instances = new();

        public override void Show()
        {
            UpdateWallet();

            //Replace item type with flags
            var capturedMonsters = InventoryService.GetAllItems()
                .Where(i => i.Key is ItemType.TestMonster1 or ItemType.TestMonster2);

            foreach (var monster in capturedMonsters)
            {
                if (monster.Value == 0)
                    continue;

                for (var i = 0; i < monster.Value; i++)
                {
                    var data = _data.GetDataByType(monster.Key);
                    var view = Instantiate(_prefab, _container);
                    var price = TradeService.GetPrice(data.ItemType);
                    view.SetData(data.ItemType, data.Icon, data.Name, data.Description, price);
                    view.OnSellClickedSignal.AddListener(SellMonster);
                    _instances.Add(view);
                }
            }

            base.Show();
        }

        private void SellMonster(MonsterView view)
        {
            var type = view.MonsterType;
            TradeService.SellItem(type);
            UpdateWallet();

            Destroy(view.gameObject);
            _instances.Remove(view);
        }

        private void UpdateWallet()
        {
            _wallet.text = WalletService.Get().ToString();
        }
    }
}