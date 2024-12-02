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

    public Guid Id { get; private set; }
    
    public readonly Signal<Guid> OnItemClick = new();

    public void SetData(Sprite icon, string name, int level, Guid id)
    {
        _icon.sprite = icon;
        _name.text = name;
        SetLevel(level);
        Id = id;
    }

    public void SetLevel(int level) => _level.text = level.ToString();

    public void Select() => OnItemClick.Dispatch(Id);
}