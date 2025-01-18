using System;
using System.Collections.Generic;
using System.Linq;
using Dependencies.ChaserLib.Dialogs.Events;
using Dependencies.ChaserLib.Tasks;
using Plugins.EventDispatching.Dispatcher;
using UnityEngine;

namespace Dependencies.ChaserLib.Dialogs
{
    public class DialogsLauncher : MonoBehaviour, IDialogsLauncher
    {
        [Serializable]
        private class Dialog
        {
            public DialogType Type;
            public GameObject Prefab;
        }

        [SerializeField] private Dialog[] _dialogs;

        private static ServiceLocator.ServiceLocator Locator => ServiceLocator.ServiceLocator.Instance;
        private static ICancellationTokenFactory TokenFactory =>
            Locator.Get<ICancellationTokenFactory>();
        private static IEventDispatcher EventDispatcher => Locator.Get<IEventDispatcher>();

        private int _currentSortingOrder;

        private readonly Stack<DialogBase> _openDialogs = new();

        public T Show<T>(DialogType dialogType) => Show(dialogType).GetComponent<T>();

        public GameObject Show(DialogType dialogType)
        {
            var prefab = GetPrefab(dialogType);
            var instance = Instantiate(prefab, transform);
            var dialog = instance.GetComponent<DialogBase>();
            var token = TokenFactory.GetDialogClosingToken(dialog.OnClosedSignal);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            dialog.Show();
            dialog.AddCancellationToken(token);
            dialog.GetComponent<Canvas>().sortingOrder = ++_currentSortingOrder;
            dialog.OnClosedSignal.AddOnce(PopDialog);

            _openDialogs.Push(dialog);
            EventDispatcher.Raise(new ShowDialogEvent());

            return dialog.gameObject;
        }

        private void CloseDialog()
        {
            var targetDialog = _openDialogs.Peek();
            targetDialog.Hide();
        }

        private void PopDialog()
        {
            _openDialogs.Pop();
            EventDispatcher.Raise(new HideDialogEvent());
        }

        public bool HasOpenDialog() => _openDialogs.Any();

        private GameObject GetPrefab(DialogType type) => _dialogs.First(d => d.Type == type).Prefab;
    }
}