using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameConfigLoader : MonoBehaviour
{
    public string fileName = "levelConfiguration.json";
    public LevelConfig levelConfig;
    public LevelFactory levelFactory;

    private void Start()
    {
        levelFactory = new LevelFactory();
        LoadConfig();
        CreateLevel();
        
    }

    private void LoadConfig()
    {
        string filePath = Path.Combine(Application.dataPath + "/StreamingAssets/" + fileName);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            levelConfig = JsonUtility.FromJson<LevelConfig>(jsonString);
        }
        else
        {
            Debug.LogError("Config file not found at: " + filePath);
        }
    }
    
    private void CreateLevel()
    {
        if (levelConfig != null && levelConfig.levels != null)
        {
            foreach (LevelData data in levelConfig.levels)
            {
                Level level = levelFactory.CreateLevel(data);
                Debug.Log("created level: " + level.LevelFeature);
            }
        }
    }
}

[System.Serializable]
public class LevelConfig
{
    public List<LevelData> levels;
}

[System.Serializable]
public class LevelData
{
    public int pointToPass;
    public int timeLimit;
    public string levelFeature;
}

// фабрика создания уровней 
public class LevelFactory
{
    public Level CreateLevel(LevelData data)
    {
        return new Level
        {
            PointsToPass = data.pointToPass,
            TimeLimit = data.timeLimit,
            LevelFeature = data.levelFeature
        };
    }
}

[System.Serializable]
public class Level
{
    public int PointsToPass { get; set; }
    public int TimeLimit { get; set; }
    public string LevelFeature { get; set; }
}
