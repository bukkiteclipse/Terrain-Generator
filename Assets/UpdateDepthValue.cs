using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateUIValues : MonoBehaviour {

    Text valueText;


	// Use this for initialization
	void Start () {
            valueText = GetComponent<Text>();
	}
	
	public void changeDepthValueText(float value)
    {
        valueText.text = value.ToString();
    }
}
