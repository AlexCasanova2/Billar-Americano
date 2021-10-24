using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public string player1Name;
    public int player1Points;
    public string player2Name;
    public int player2Points;
    public float musicVolumeValue;
    public float soundsVolumeValue;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            GameData_Storage data = (GameData_Storage)bf.Deserialize(file);

            musicVolumeValue = data.musicVolumeValue;
            soundsVolumeValue = data.soundsVolumeValue;

            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameData.dat");
        GameData_Storage data = new GameData_Storage();

        data.musicVolumeValue = musicVolumeValue;
        data.soundsVolumeValue = soundsVolumeValue;
        
        bf.Serialize(file, data);
        file.Close();
    }
}


[Serializable]

class GameData_Storage
{
    public string player1Name;
    public int player1Points;
    public string player2Name;
    public int player2Points;
    public float musicVolumeValue;
    public float soundsVolumeValue;
}
