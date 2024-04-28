using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class RuneSaver : MonoBehaviour
{
    public bool isSaveMode = false;
    // make select list at interpreter for monster color "red", "blue", "green", "yellow"
    public enum MonsterColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }
    public MonsterColor monsterColor;
    
    private const string SaveFileName = "PreparedRunes";

    private void Awake()
    {
        LoadRuneData();
    }

    public void SaveRune(List<Vector2> drawnPoints)
    {
        string runeName = monsterColor.ToString();
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
            string file = Resources.Load<TextAsset>(SaveFileName).text;
            RuneDataList runeDataList = JsonUtility.FromJson<RuneDataList>(file);
            runeComparator.preparedRunes = runeDataList.runes;
        }
    }

    private string GetSaveFilePath()
    {
        // Resources 폴더에 저장
        return Path.Combine(Application.dataPath, "Resources", SaveFileName);
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