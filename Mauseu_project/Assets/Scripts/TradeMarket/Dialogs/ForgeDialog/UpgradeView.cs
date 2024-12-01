using Codice.Client.BaseCommands.Merge.Xml;
using Services.Weapons.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TradeMarket.Dialogs.ForgeDialog
{
    public class UpgradeView : MonoBehaviour
    {
        [System.Serializable]
        private struct ProgressionSlider
        {
            public Slider CurrentValue;
            public Slider UpgradeValue;
        }

        [System.Serializable]
        private struct ResourceCost
        {
            public ItemType ResourceType;
            public TMP_Text AmountLabel;
        }

        [SerializeField] private ProgressionSlider _damageProgression;
        [SerializeField] private ProgressionSlider _fireRateProgression;
        [SerializeField] private TMP_Text _costLabel;
        [SerializeField] private ResourceCost[] _resourceCosts;

        private const float MaxDamage = 10;
        private const float MaxFireRate = 5;

        public void SetData(Characteristic currentCharacteristics,
            Characteristic upgradeCharacteristics, UpgradeCost upgradeCost)
        {
            _damageProgression.CurrentValue.value = currentCharacteristics.Damage / MaxDamage;
            _damageProgression.UpgradeValue.value = upgradeCharacteristics.Damage / MaxDamage;

            _fireRateProgression.CurrentValue.value = currentCharacteristics.FireRate / MaxFireRate;
            _fireRateProgression.UpgradeValue.value = upgradeCharacteristics.FireRate / MaxFireRate;

            _costLabel.text = upgradeCost.Cost.ToString();
            
        }
    }
}