using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelManager levelManager;
    public UIManager uiManager;

    private void Start()
    {
        LoadLevelDesign();
        StartGame();
    }

    // GameManager.cs
    private void LoadLevelDesign()
    {
        string json = Resources.Load<TextAsset>("LevelDesign").text;
        LevelDataWrapper levelDataWrapper = JsonUtility.FromJson<LevelDataWrapper>(json);
        levelManager.SetLevelDatas(levelDataWrapper.levels);
    }

    private void StartGame()
    {
        // 게임 시작 로직 처리
        levelManager.StartLevel();
    }

    public void GameOver()
    {
        // 게임 오버 로직 처리
        uiManager.ShowGameOverUI();
    }
    
    public void NextLevel()
    {
        // 다음 레벨 로직 처리
        levelManager.StartNextLevel();
    }
}