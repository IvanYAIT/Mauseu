using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TradeMarket.Dialogs.ForgeDialog
{
    public class ResourceAmount : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amount;

        public void SetData(Sprite icon, int amount)
        {
            _icon.sprite = icon;
            _amount.text = amount.ToString();
        }
    }
}