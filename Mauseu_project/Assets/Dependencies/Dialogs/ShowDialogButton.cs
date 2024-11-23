using Dependencies.ChaserLib.Dialogs.Commands;
using UnityEngine;

namespace Dependencies.ChaserLib.Dialogs
{
    public class ShowDialogButton : MonoBehaviour
    {
        [SerializeField] private DialogType _type;

        public void Open()
        {
            new ShowDialogCommand(_type).Execute();
        }
    }
}