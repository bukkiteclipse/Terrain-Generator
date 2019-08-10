using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

	public static void SaveTable (int[] table)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/noise.tab";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, table);
        fileStream.Close();
    }

    public static int[] LoadTable()
    {
        string path = Application.persistentDataPath + "/noise.tab";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            int[] table = formatter.Deserialize(fileStream) as int[];
            fileStream.Close();

            return table;

        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
