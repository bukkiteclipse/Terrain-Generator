using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ValueUpdater : MonoBehaviour {


    public Slider xSizeSlider;
    public Text xSizeText;
    public Slider zSizeSlider;
    public Text zSizeText;

    public Slider scaleSlider;
    public Text scaleText;
    public Slider depthSlider;
    public Text depthText;

    public Slider xOffsetSlider;
    public Text xOffsetText;
    public Slider zOffsetSlider;
    public Text zOffsetText;

    public void UpdateUIValues(int xSize, int zSize, float scale, float depth, float xOffset, float zOffset)
    {
        xSizeSlider.value = xSize;
        xSizeText.text = xSize.ToString();
        zSizeSlider.value = zSize;
        zSizeText.text = zSize.ToString();

        scaleSlider.value = scale;
        scaleText.text = scale.ToString();
        depthSlider.value = depth;
        depthText.text = depth.ToString();

        xOffsetSlider.value = xOffset;
        xOffsetText.text = xOffset.ToString();
        zOffsetSlider.value = zOffset;
        zOffsetText.text = zOffset.ToString();
    }
}
