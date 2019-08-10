using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DriveBy : MonoBehaviour {

    RectTransform rectTransform;
    public float speed = 20;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
        if (rectTransform.position.x > -3000)
        {
            float x = rectTransform.position.x;
            x -= speed;
            rectTransform.position = new Vector3(x, rectTransform.position.y, rectTransform.position.z);
        }
	}
}
