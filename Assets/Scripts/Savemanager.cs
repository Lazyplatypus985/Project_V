using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Savemanager : MonoBehaviour
{
    [System.Serializable] 
    public class UpgradeLevelData 
    {
        public string upgradeName; 
        public float level; 
    }
    [System.Serializable]
    public class Gamedata
    {
        public float coins;
        public List<UpgradeLevelData> upgradeLevels = new List<UpgradeLevelData>();
    }

    const string SAVE_FILE_NAME = "SaveData.json";

    public static Gamedata lastLoadedGameData;

    public static Gamedata lastLoadedGame
    {
        get
        {
            if (lastLoadedGameData == null) Load();
            return lastLoadedGameData;
        }
    }

    public static string GetSavePath()
    {
        return string.Format("{0}/{1}", Application.persistentDataPath, SAVE_FILE_NAME);
    }

    public static void Save(Gamedata data = null)
    {
        if (data == null)
        {
            if (lastLoadedGameData == null) Load();
            data = lastLoadedGameData;
        }

        File.WriteAllText(GetSavePath(), JsonUtility.ToJson(data));
    }

    public static Gamedata Load(bool usePreviousLoadIfAvailable = false)
    {
        if (usePreviousLoadIfAvailable && lastLoadedGameData != null)
        {
            return lastLoadedGameData;
        }

        string path = GetSavePath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            lastLoadedGameData = JsonUtility.FromJson<Gamedata>(json);

            if (lastLoadedGameData == null)
            {
                lastLoadedGameData = new Gamedata();
            }
        }
        else
        {
            lastLoadedGameData = new Gamedata();
        }

        return lastLoadedGameData;
    }
}
