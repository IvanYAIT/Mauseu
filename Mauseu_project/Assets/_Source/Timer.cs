using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private int startTime;
    [SerializeField] private float timeMultilplier = 5;
    [SerializeField] private TextMeshProUGUI timerText;

    private float _timer;

    void Start()
    {
        _timer = startTime * 3600;
    }

    void Update()
    {
        _timer += Time.deltaTime * timeMultilplier;

        int hours = Mathf.FloorToInt(_timer / 3600f);
        if (hours >= 24)
            _timer = 0;
        int minutes = Mathf.FloorToInt((_timer - hours * 3600f) / 60f);

        timerText.text = $"{hours:D2}:{minutes:D2}";
        if (hours >= 6 && hours < 22)
            ActivateDebuff();
    }

    private void ActivateDebuff()
    {

    }
}