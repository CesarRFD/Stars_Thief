using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SaveConfigData(MenuPausa mp)
    {
        PlayerData configData = new PlayerData(mp);
        string dataPath = Application.persistentDataPath + "/config.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, configData);
        fileStream.Close();
    }
    public static PlayerData LoadConfigData()
    {
        string dataPath = Application.persistentDataPath + "/config.save";

        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            PlayerData configData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return configData;
        }
        else
        {
            Debug.LogError("No se encontró el archivo");
            return null;
        }
    }
    //////////////////////////////////////////////////////////////////////////////
    public static void SaveDificultData(EnemysSave enemysSave)
    {
        PlayerData dificultData = new PlayerData(enemysSave);
        string dataDPath = Application.persistentDataPath + "/dificult.save";
        FileStream fileStream = new FileStream(dataDPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, dificultData);
        fileStream.Close();
    }
    public static PlayerData LoadDificultData()
    {
        string dataDPath = Application.persistentDataPath + "/dificult.save";

        if (File.Exists(dataDPath))
        {
            FileStream fileStream = new FileStream(dataDPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            PlayerData dificultData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return dificultData;
        }
        else
        {
            Debug.LogError("No se encontró el archivo");
            return null;
        }
    }
    //////////////////////////////////////////////////////////////////////////////
    public static void SaveControlData(Movement movement)
    {
        PlayerData controlData = new PlayerData(movement);
        string dataDPath = Application.persistentDataPath + "/control.save";
        FileStream fileStream = new FileStream(dataDPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, controlData);
        fileStream.Close();
    }
    public static PlayerData LoadControlData()
    {
        string controlPath = Application.persistentDataPath + "/control.save";

        if (File.Exists(controlPath))
        {
            FileStream fileStream = new FileStream(controlPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            PlayerData controlData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return controlData;
        }
        else
        {
            Debug.LogError("No se encontró el archivo");
            return null;
        }
    }
}