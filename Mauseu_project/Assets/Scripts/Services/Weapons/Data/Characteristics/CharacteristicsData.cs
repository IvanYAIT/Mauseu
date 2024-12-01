using UnityEngine;

namespace Services.Weapons.Data
{
    [CreateAssetMenu(fileName = "Characteristics", menuName = "Data/Weapon/Characteristics", order = 1)]
    public class CharacteristicsData : ScriptableObject
    {
        [SerializeField] private Characteristic[] _levels;

        public Characteristic GetByLevel(int level) => _levels[level];

        public int GetLevelsAmount() => _levels.Length;
    }
}