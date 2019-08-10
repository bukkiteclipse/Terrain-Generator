using UnityEngine;
using System.Collections;
using UnityEngine.UI;


using System.IO;
public class FileManager : MonoBehaviour {

    string path;
    public Text pathUIText;

    public void OpenExplorer()
    {
        //path = Application.persistentDataPath + "/TerrainSettings.fun"; 
        //path = EditorUtility.OpenFilePanel("Save Terrain Settings", Application.persistentDataPath, "fun");
    }
}
