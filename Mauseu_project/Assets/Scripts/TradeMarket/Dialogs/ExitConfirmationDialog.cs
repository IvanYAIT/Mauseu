using Dependencies.ChaserLib.Dialogs;
using UnityEngine;

namespace TradeMarket.Dialogs
{
    public class ExitConfirmationDialog : DialogBase
    {
        public void Confirm() => Application.Quit();

        public void Cancel() => Hide();
    }
}
