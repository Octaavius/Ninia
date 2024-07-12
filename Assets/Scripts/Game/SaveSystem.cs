using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem {
    
    private static string Path = Application.persistentDataPath + "player.stats"; 
    public static void SavePlayer (Player player) {
        BinaryFormatter formatter = new();
        FileStream stream = new(Path, FileMode.Create);

        PlayerData data = new(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveDefaultPlayer()
    {
        BinaryFormatter formatter = new();
        FileStream stream = File.Create(Path);
        
        PlayerData defaultStats = new();

        formatter.Serialize(stream, defaultStats);
        stream.Close();
    }

    public static PlayerData LoadPlayer() {
        if(File.Exists(Path)){
            Debug.Log(Path);
            BinaryFormatter formatter = new();
            FileStream stream = new(Path, FileMode.Open);

            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + Path);
            SaveDefaultPlayer();
            
            return LoadPlayer();
        }
    }

}
