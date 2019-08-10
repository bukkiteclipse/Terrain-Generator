using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.UI;

public class SaveLoadSystem : MonoBehaviour {

    List<string> saveFileNameList = new List<string>();
    string[] saveFileNames = new string[0];

    public Dropdown dropDown;
    public InputField inputNameField;
    string saveFileName;
    string selectedName;
    public Text infoText;

    public void Start()
    {
        string path = Application.persistentDataPath + "/saveFileNames.lst";
        if (!File.Exists(path))
        {
            FirstSavePersistentFileNames();
        } else
        {
            LoadPersistentFileNames();
            DropdownIndexChanged(0);
        }
    }

    void convertArrayToList()
    {
        saveFileNameList.Clear();
        saveFileNameList = new List<string>(saveFileNames);
        UpdateDropDown();
    }

    public void UpdateDropDown()
    {
        dropDown.ClearOptions();
        dropDown.AddOptions(saveFileNameList);
        DropdownIndexChanged(0);

        if (saveFileNameList.Count >= 1)
        {
            selectedName = saveFileNameList[dropDown.value];
            infoText.text = Application.persistentDataPath + "/" + selectedName + ".ter";
        }
        else
        {
            selectedName = "";
            infoText.text = "path";
        }
    }

    void FirstSavePersistentFileNames()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveFileNames.lst";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveFileNames);
        stream.Close();
    }

    public void SavePersistentFileNames()
    {
        saveFileNames = saveFileNameList.ToArray();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveFileNames.lst";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveFileNames);
        stream.Close();

    }

    public void LoadPersistentFileNames()
    {
        string path = Application.persistentDataPath + "/saveFileNames.lst";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            saveFileNames = formatter.Deserialize(stream) as string[];
            stream.Close();
        }
        else
        {
            Debug.LogError("(SaveName)Save file not found at: " + path);
        }
        convertArrayToList();
    }

	public void SaveTerrainSettings(TerrainSettings terrainSettings)
    {

        saveFileName = inputNameField.text.ToString();
        if (!saveFileName.Equals(""))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            //string path = Application.persistentDataPath + "/TerrainSettings.fun";
            string path = Application.persistentDataPath + "/" + saveFileName + ".ter";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, terrainSettings);
            stream.Close();
            if (!saveFileNameList.Contains(saveFileName))
            {
                saveFileNameList.Add(saveFileName);
                SavePersistentFileNames();
                UpdateDropDown();
            }
        }
    }

    public TerrainSettings LoadTerrainSettings()
    {
        if (saveFileNameList.Count >= 1)
        {
            string path = Application.persistentDataPath + "/" + selectedName + ".ter";
            //string path = Application.persistentDataPath + "/TerrainSettings.fun";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                TerrainSettings terrainSettings = formatter.Deserialize(stream) as TerrainSettings;
                stream.Close();
                return terrainSettings;
            }
            else
            {
                Debug.LogError("(Terrain)Save file not found at: " + path);
                return null;
            }
        } else
        {
            return null;
        }
    }

    public void DeleteSaveFileName()
    {
        if(saveFileNameList.Contains(selectedName))
        {
            // Delete selectedName from List
            saveFileNameList.Remove(selectedName);
            saveFileNames = saveFileNameList.ToArray();

            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saveFileNames.lst";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, saveFileNames);
            stream.Close();

            // Delete selectedName Terrain Settings
            BinaryFormatter formatter2 = new BinaryFormatter();
            //string path = Application.persistentDataPath + "/TerrainSettings.fun";
            string path2 = Application.persistentDataPath + "/" + selectedName + ".ter";
            File.Delete(path2);

            UpdateDropDown();
        }
    }

    public void DropdownIndexChanged(int index)
    {
        if (saveFileNameList.Count >= 1)
        {
            selectedName = saveFileNameList[dropDown.value];
            infoText.text = Application.persistentDataPath + "/" + selectedName + ".ter";
        } else
        {
            selectedName = "";
            infoText.text = "path";
        }
    }
}
