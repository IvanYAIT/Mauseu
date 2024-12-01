using Services.Inventory.Items;
using TMPro;
using UnityEngine.UI;

namespace TradeMarket.Dialogs.ForgeDialog
{
    [System.Serializable]
    public struct ResourceView
    {
        public ItemType Type;
        public TMP_Text AmountLabel;
        public Image Icon;
    }
}