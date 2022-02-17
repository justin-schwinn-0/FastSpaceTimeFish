using UnityEngine;
using UnityEngine.UI;

public class slowPowerUI : MonoBehaviour
{
    public TimeManipHandler t;
    public Text powerText;
    private string text;
    // Update is called once per frame
    void Update()
    {
        text ="Slow power: " + t.getslowPower() +  "/" + t.SlowDurration; 
        text +="\nStop power: " + t.getStopPower() +  "/" + t.StopDurration; 
        powerText.text = text;
    }
}
