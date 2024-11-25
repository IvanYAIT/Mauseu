using FrostLib.Signals.impl;
using Services.Inventory.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TradeMarket.Dialogs.TradeMarket
{
    public class MonsterView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _priceLabel;

        public ItemType MonsterType { get; private set; }

        private string _description;

        public readonly Signal<MonsterView> OnSellClickedSignal = new();

        public void SetData(ItemType monsterType, Sprite icon, string monsterName,
            string monsterDescription, int monsterPrice)
        {
            _icon.sprite = icon;
            _nameLabel.text = monsterName;
            MonsterType = monsterType;
            _description = monsterDescription;
            _priceLabel.text = $"Sell for: {monsterPrice}";
        }

        public void Sell() => OnSellClickedSignal.Dispatch(this);
    }
}