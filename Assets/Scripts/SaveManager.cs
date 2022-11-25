using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    private static string dataPath = Application.persistentDataPath + "/data.save";

    private static BinaryFormatter binaryFormatter = new BinaryFormatter();

    public static void SaveData(Player player, GameController game)
    {
        SaveData saveData = new SaveData(player, game);
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        binaryFormatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    public static SaveData LoadData()
    {
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            SaveData saveData = (SaveData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return saveData;
        }
        else
        {
            return null;
        }
    }
}
