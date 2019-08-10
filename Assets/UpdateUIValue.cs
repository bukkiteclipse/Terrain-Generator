using UnityEngine;
using UnityEngine.UI;

public class UpdateUIValue : MonoBehaviour {

    Text valueText;

    // Use this for initialization
    void Start()
    {
        valueText = GetComponent<Text>();
    }

    public void changeValueText(float value)
    {
        valueText.text = value.ToString();
    }
}
