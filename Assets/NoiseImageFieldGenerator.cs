using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoiseImageFieldGenerator : MonoBehaviour {

    public MeshGenerator meshGenerator;
    bool updated = false;
    RawImage rawImage;

	// Use this for initialization
	void Start () {
        rawImage = GetComponent<RawImage>();
	}

    private void LateUpdate()
    {
        if(!updated)
        {
            rawImage.texture = GenerateTexture();
            updated = true;
        } else
        {

        }
    }

    Texture2D GenerateTexture()
    {
        int heightMapXSize = meshGenerator.heightMap.GetLength(0);
        int heightMapZSize = meshGenerator.heightMap.GetLength(1);
        Texture2D texture = new Texture2D(heightMapXSize, heightMapZSize);

        int index = 0;
        for(int x = 0; x < heightMapXSize; x++)
        {
            for(int z = 0; z < heightMapZSize; z++)
            {
                meshGenerator.heightMap.GetLength(0);
                float height = (meshGenerator.heightMap[x, z]+30)/60f;
                //Color color = new Color(height, height, height);
                Color color = meshGenerator.vertexColors2D[x, z];
                texture.SetPixel(x, z, color);
                index++;
            }
        }
        texture.Apply();

        return texture;
    }

    public void updateOverviewHeightMap()
    {
        updated = false;
    }
}
