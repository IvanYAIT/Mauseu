using System;
using FrostLib.Signals.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _level;

    private Guid _id;

    public readonly Signal<Guid> OnItemClick = new();

    public void SetData(Sprite icon, string name, int level, Guid id)
    {
        _icon.sprite = icon;
        _name.text = name;
        _level.text = level.ToString();
        _id = id;
    }

    public void Select() => OnItemClick.Dispatch(_id);
}