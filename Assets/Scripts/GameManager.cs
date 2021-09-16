using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string BestName;
    public int BestScore;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string BestName;
    public int BestScore = 0;
    public string Name;

    public void SaveData()
    {
        var data = new SaveData();
        data.BestName = this.BestName;
        data.BestScore = this.BestScore;

        var jsonString = JsonUtility.ToJson(data);

        File.WriteAllText(
            this.GetSaveDataPath(),
            jsonString);
    }

    public void LoadData()
    {
        var saveDataPath = this.GetSaveDataPath();

        if (File.Exists(saveDataPath))
        {
            var jsonString = File.ReadAllText(
                saveDataPath);

            var saveData = JsonUtility.FromJson<SaveData>(jsonString);

            this.BestName = saveData.BestName;
            this.BestScore = saveData.BestScore;
        }
    }

    public void UpdateBestScore(int lastScore)
    {
        if (this.BestScore < lastScore)
        {
            this.BestScore = lastScore;
            this.BestName = this.Name;
            this.SaveData();
        }
    }

    public string GetBestScoreText()
    {
        return $"Best Score: {GameManager.Instance.BestName} - {GameManager.Instance.BestScore}";
    }

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameManager.Instance = this;
            this.LoadData();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private string GetSaveDataPath()
    {
        return Path.Combine(Application.persistentDataPath, "savefile.json");
    }
}
