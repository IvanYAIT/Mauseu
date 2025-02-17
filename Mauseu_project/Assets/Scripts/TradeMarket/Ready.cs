using UnityEngine;
using TMPro;

public class Ready : MonoBehaviour
{
    public bool value;
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        text.text = $"{value}";
    }
}
