using System.Collections.Generic;
using Services.Inventory.Items;
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

        [SerializeField] private ProgressionSlider _damageProgression;
        [SerializeField] private ProgressionSlider _fireRateProgression;
        [SerializeField] private TMP_Text _costLabel;
        [SerializeField] private ResourceAmount _resourcePrefab;
        [SerializeField] private Transform _resourceContainer;
        [SerializeField] private ItemsData _itemsData;
        
        private const float MaxDamage = 10;
        private const float MaxFireRate = 5;

        private readonly List<ResourceAmount> _resourceInstances = new();

        public void SetData(Characteristic currentCharacteristics,
            Characteristic upgradeCharacteristics, UpgradeCost upgradeCost)
        {
            Clear();
            
            _damageProgression.CurrentValue.value = currentCharacteristics.Damage / MaxDamage;
            _damageProgression.UpgradeValue.value = upgradeCharacteristics.Damage / MaxDamage;

            _fireRateProgression.CurrentValue.value = currentCharacteristics.FireRate / MaxFireRate;
            _fireRateProgression.UpgradeValue.value = upgradeCharacteristics.FireRate / MaxFireRate;

            _costLabel.text = upgradeCost.Cost.ToString();
            
            foreach (var resource in upgradeCost.Resources)
            {
                var instance = Instantiate(_resourcePrefab, _resourceContainer);
                var icon = _itemsData.GetItemData(resource.Type).Icon;
                instance.SetData(icon, resource.Amount);
                
                _resourceInstances.Add(instance);
            }
        }

        private void Clear()
        {
            foreach (var instance in _resourceInstances)
            {
                Destroy(instance.gameObject);
            }
            
            _resourceInstances.Clear();
        }
    }
}