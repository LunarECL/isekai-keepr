using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class RuneSaver : MonoBehaviour
{
    public bool isSaveMode = false;
    private const string SaveFileName = "PreparedRunes.json";

    private void Awake()
    {
        LoadRuneData();
    }

    public void SaveRune(List<Vector2> drawnPoints)
    {
        string runeName = "Rune_" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
        RuneData newRune = new RuneData();
        newRune.name = runeName;
        newRune.points = new List<Vector2>(drawnPoints);
        AddPreparedRune(newRune);
        SaveRuneData();
        Debug.Log("Rune saved: " + runeName);
    }

    private void AddPreparedRune(RuneData rune)
    {
        RuneComparator runeComparator = GetComponent<RuneComparator>();
        if (runeComparator != null)
        {
            runeComparator.preparedRunes.Add(rune);
        }
    }

    private void SaveRuneData()
    {
        RuneComparator runeComparator = GetComponent<RuneComparator>();
        if (runeComparator != null)
        {
            string filePath = GetSaveFilePath();
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        
            string json = JsonUtility.ToJson(new RuneDataList(runeComparator.preparedRunes));
            File.WriteAllText(filePath, json);
        }
    }

    private void LoadRuneData()
    {
        RuneComparator runeComparator = GetComponent<RuneComparator>();
        if (runeComparator != null)
        {
            string filePath = GetSaveFilePath();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                RuneDataList loadedData = JsonUtility.FromJson<RuneDataList>(json);
                runeComparator.preparedRunes = loadedData.runes;
            }
        }
    }

    private string GetSaveFilePath()
    {
        return Path.Combine(Application.dataPath, "Data", SaveFileName);
    }
}

[System.Serializable]
public class RuneDataList
{
    public List<RuneData> runes;

    public RuneDataList(List<RuneData> runes)
    {
        this.runes = runes;
    }
}