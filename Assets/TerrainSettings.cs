using UnityEngine;
using System.Collections;

[System.Serializable]
public class TerrainSettings {

    public int xSize;
    public int zSize;
    public float scale;
    public float depth;
    public float xOffset;
    public float zOffset;
    
    public TerrainSettings(int xSize, int zSize, float scale, float depth, float xOffset, float zOffset)
    {
        this.xSize = xSize;
        this.zSize = zSize;
        this.scale = scale;
        this.depth = depth;
        this.xOffset = xOffset;
        this.zOffset = zOffset;
    }
}
