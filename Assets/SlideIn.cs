using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlideIn : MonoBehaviour {

    public GameObject panel;
    public bool showSideMenu = false;

	// Use this for initialization
	void Start () {
        if (!showSideMenu)
        {
            panel.SetActive(false);
        } else
        {
            panel.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.M) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && !showSideMenu)
        {
            showSideMenu = true;
            panel.SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.M) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && showSideMenu)
        {
            showSideMenu = false;
            panel.SetActive(false);
        }
	}

    public void OpenMenu()
    {
        panel.SetActive(true);
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
    }
}
