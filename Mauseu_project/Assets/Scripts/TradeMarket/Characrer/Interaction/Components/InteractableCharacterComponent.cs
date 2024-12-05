using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.Dialogs.Commands;
using UnityEngine;

namespace TradeMarket.Characrer
{
    public class InteractableCharacterComponent : InteractableItemComponentBase
    {
        [SerializeField] private DialogType _dialog;

        public override void Interract()
        {
            new ShowDialogCommand(_dialog).Execute();
        }
    }
}