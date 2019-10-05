using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLoader
{

    const string c_DataPath = "data.json";

    GameData m_GameData;

    public DataLoader(MainLogic logic){

        LoadGameData();
    }

    void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, c_DataPath);

        if(File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath); 

            // Debug.LogError("dataAsJson" + dataAsJson);

            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            m_GameData = JsonUtility.FromJson<GameData>(dataAsJson);
            // m_GameData.Print();

            m_GameData.Map();
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    // return JSON data from file 
    public GameData GetData(){
        return m_GameData;
    }
   
}
