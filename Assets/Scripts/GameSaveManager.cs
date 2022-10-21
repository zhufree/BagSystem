using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour
{
    public Inventory inventory;
    string savePath = "";

    void Awake() {
        savePath = Application.persistentDataPath + "/game_SaveData/";
    }
    public void SaveGame() {
        // Debug.Log(Application.persistentDataPath);
        if (!Directory.Exists(savePath)) {
            Directory.CreateDirectory(savePath);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream file = File.Create(savePath + "inventory.txt");

        var json = JsonUtility.ToJson(inventory);
        
        formatter.Serialize(file, json);

        file.Close();
    }
    public void LoadGame() {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(savePath + "inventory.txt")) {
            Debug.Log("load");
            FileStream file = File.Open(savePath + "inventory.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), inventory);

            file.Close();
        }
    }
}
