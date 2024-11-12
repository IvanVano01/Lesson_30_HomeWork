using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTimer;    

    public void SetTimerView(float timer)
    {
        _textTimer.text = timer.ToString();
    } 

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);    
}
