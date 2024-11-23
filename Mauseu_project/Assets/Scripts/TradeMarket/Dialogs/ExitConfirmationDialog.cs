using Dependencies.ChaserLib.Dialogs;
using UnityEngine;

public class ExitConfirmationDialog : DialogBase
{
    public void Confirm() => Application.Quit();

    public void Cancel() => Hide();
}
