﻿using System.Collections.Generic;
using System.Linq;
using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Items;
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
        [SerializeField] private ItemsData _itemsData;

        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();
        private static ITradeService TradeService => Locator.Get<ITradeService>();
        private static IWalletService WalletService => Locator.Get<IWalletService>();

        private readonly List<MonsterView> _instances = new();

        public override void Show()
        {
            UpdateWallet();
       
            var monstersData = _itemsData.GetAllItemsInCategory(ItemCategory.Monsters);
            var allUniqItems = InventoryService.GetAllUniqItems();
            
            foreach (var monster in monstersData)
            {
                var amountInInventory = allUniqItems.Count(i => i.Type == monster.Type);
                
                if (amountInInventory == 0)
                    continue;

                for (var i = 0; i < amountInInventory; i++)
                {
                    var view = Instantiate(_prefab, _container);
                    var price = TradeService.GetPrice(monster.Type);
                    
                    view.SetData(monster.Type, monster.Icon, monster.Name, monster.Description, price);
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

            Destroy(view.gameObject);
            _instances.Remove(view);
            
            UpdateWallet();
            UpdatePrices();
        }

        private void UpdateWallet() => _wallet.text = WalletService.Get().ToString();

        private void UpdatePrices()
        {
            foreach (var instance in _instances)
            {
                var newPrice = TradeService.GetPrice(instance.MonsterType);
                instance.SetPrice(newPrice);
            }
        }
    }
}