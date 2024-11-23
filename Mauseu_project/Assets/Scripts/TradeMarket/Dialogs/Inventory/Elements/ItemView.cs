using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TradeMarket.Dialogs.Inventory.Elements
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _countLabel;

        private string _name;
        private string _description;

        public void SetData(string name, string description, Sprite icon, int amount)
        {
            _name = name;
            _description = description;
            _icon.sprite = icon;
            _countLabel.gameObject.SetActive(amount != 1);
            _countLabel.text = amount.ToString();
        }
    }
}