using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    Mesh mesh;
    public float[,] heightMap;
    public NoiseImageFieldGenerator noiseImageFieldGenerator;
    TerrainSettings terrainSettings;
    public SaveLoadSystem saveLoadSystem;

    Vector3[] vertices;
    int[] triangles;
    float meshPositionOffsetX;
    float meshPositionOffsetZ;

    
    public int xSize = 50;
    public int zSize = 50;
    public float scale = 7f;
    public float depth = 5f;
    public float pNoiseOffsetX = 0;
    public float pNoiseOffsetZ = 0;

    PerlinNoise2D perlinNoise2D;

    bool valuesUIChanged = false;
    bool sizeUIChanged = false;

    public ValueUpdater valueUpdater;

    public Color[] vertexColors;
    public Color[,] vertexColors2D;
    public Gradient gradient;
    

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        perlinNoise2D = new PerlinNoise2D();

        CreateMeshShape();
        UpdateMesh();
    }

    private void Update()
    {
        if(sizeUIChanged)
        {
            CreateMeshShape();
            UpdateMesh();
            sizeUIChanged = false;
        }
        if (valuesUIChanged)
        {
            UpdateHeightMapOfMeshVertices();
            UpdateMesh();
            valuesUIChanged = false;
        }
    }

    void UpdateHeightMapOfMeshVertices()
    {
        int index = 0;

        vertexColors = new Color[(xSize + 1) * (zSize + 1)];
        vertexColors2D = new Color[xSize + 1, zSize + 1];

        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                float yHeight = CalculatePerlinNoiseHeight(x, z);
                heightMap[x, z] = yHeight;
                vertices[index].y = yHeight;

                float gradientHeight = Mathf.InverseLerp(-30, 30, yHeight);
                Color heightColor = gradient.Evaluate(gradientHeight);
                vertexColors[index] = heightColor;
                vertexColors2D[x, z] = heightColor;


                index++;
            }
        }
    }

    IEnumerator CreateMeshShape_CoRoutine()
    {
        // Create Vertex Field
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int index = 0;
        for(int z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2;
                vertices[index] = new Vector3(x, y, z);
                index++;
            }
        }

        // Create Triangle Field

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for(int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;

                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;


                yield return new WaitForSeconds(0.02f);
            }
            vert++;
        }     
    }

    void CreateMeshShape()
    {
        // Update MeshOffsets to draw Mesh centered in Worldspace
        meshPositionOffsetX = (float)xSize / 2;
        meshPositionOffsetZ = (float)zSize / 2;

        heightMap = new float[xSize+1, zSize+1];
        // Create Vertex Field
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        vertexColors = new Color[(xSize + 1) * (zSize + 1)];
        vertexColors2D = new Color[xSize+1, zSize+1];

        int index = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float yHeight = CalculatePerlinNoiseHeight(x, z);
                heightMap[x, z] = yHeight;
                vertices[index] = new Vector3(x - meshPositionOffsetX, yHeight, z - meshPositionOffsetZ);
                float gradientHeight = Mathf.InverseLerp(-30, 30, yHeight);
                Color heightColor = gradient.Evaluate(gradientHeight);
                vertexColors[index] = heightColor;
                vertexColors2D[x, z] = heightColor;
                index++;
            }
        }

        // Create Triangle Field

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // Triangle at each Quads bottom left
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                // Triangle at each Quads top right
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = vertexColors;
        mesh.RecalculateNormals();

        noiseImageFieldGenerator.updateOverviewHeightMap();
        updateUIValues();
    }

    public void saveTerrainSettings()
    {
        terrainSettings = new TerrainSettings(xSize, zSize, scale, depth, pNoiseOffsetX, pNoiseOffsetZ);
        saveLoadSystem.SaveTerrainSettings(terrainSettings);
    }

    public void loadTerrainSettings()
    {
        terrainSettings = saveLoadSystem.LoadTerrainSettings();
        if (terrainSettings != null)
        {
            xSize = terrainSettings.xSize;
            zSize = terrainSettings.zSize;
            scale = terrainSettings.scale;
            depth = terrainSettings.depth;
            pNoiseOffsetX = terrainSettings.xOffset;
            pNoiseOffsetZ = terrainSettings.zOffset;
            CreateMeshShape();
            UpdateMesh();
            updateUIValues();
        }

    }

    float CalculatePerlinNoiseHeight(int x, int z)
    {
        float xCoordValue = (((float)x + pNoiseOffsetX) / xSize) * scale;
        float zCoordValue = (((float)z + pNoiseOffsetZ) / zSize) * scale;

        float perlinNoiseHeight = perlinNoise2D.Noise(xCoordValue, zCoordValue) * depth;
        return perlinNoiseHeight;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        else
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], 0.1f);
            }
        }
    }

    public void changeXSizeValue(float value)
    {
        xSize = (int)value;
        valuesUIChanged = true;
        sizeUIChanged = true;
    }

    public void changeZSizeValue(float value)
    {
        zSize = (int)value;
        valuesUIChanged = true;
        sizeUIChanged = true;
    }

    public void changeScaleValue(float value)
    {
        scale = value;
        valuesUIChanged = true;
    }

    public void changeDepthValue(float value)
    {
        depth = value;
        valuesUIChanged = true;
    }

    public void changeXOffsetValue(float value)
    {
        pNoiseOffsetX = value;
        valuesUIChanged = true;
    }

    public void changeZOffsetValue(float value)
    {
        pNoiseOffsetZ = value;
        valuesUIChanged = true;
    }

    public void updateUIValues()
    {
        valueUpdater.UpdateUIValues(xSize, zSize, scale, depth, pNoiseOffsetX, pNoiseOffsetZ);
    }


}
