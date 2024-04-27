using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public DoorManager doorManager;
    public RuneManager runeManager;
    public SoundManager soundManager;
    public GameManager gameManager;

    private LevelData[] levelDatas;
    private int currentLevelIndex = 0;
    private float levelTimer;
    private float monsterSpawnTimer;

    private Dictionary<int, string> openedDoors = new Dictionary<int, string>();

    public void SetLevelDatas(LevelData[] levelDatas)
    {
        this.levelDatas = levelDatas;
    }

    public void StartLevel()
    {
        // 레벨 시작 로직 처리
        if (currentLevelIndex < levelDatas.Length)
        {
            LevelData currentLevelData = levelDatas[currentLevelIndex];
            levelTimer = 0f;
            monsterSpawnTimer = 0f;
            SpawnMonster();
        }
        else
        {
            Debug.Log("All levels completed!");
            // 게임 클리어 로직 처리
        }
    }
    
    public void StartNextLevel()
    {
        currentLevelIndex++;
        StartLevel();
    }

    private void Update()
    {
        // 레벨 진행 로직 처리
        if (currentLevelIndex < levelDatas.Length)
        {
            LevelData currentLevelData = levelDatas[currentLevelIndex];
            
            levelTimer += Time.deltaTime;
            if (levelTimer >= currentLevelData.timeLimit)
            {
                if (currentLevelData.isInfinite)
                {
                    levelTimer = 0f; // 무한 모드에서는 타이머 초기화
                }
                else
                {
                    gameManager.NextLevel();
                }
            }
            
            monsterSpawnTimer += Time.deltaTime;
            if (monsterSpawnTimer >= GetNextMonsterSpawnDelay())
            {
                SpawnMonster();
                monsterSpawnTimer = 0f;
            }
        }
    }

    private void SpawnMonster()
    {
        if (currentLevelIndex < levelDatas.Length)
        {
            LevelData currentLevelData = levelDatas[currentLevelIndex];
        
            int doorIndex = GetRandomDoorIndex(currentLevelData.allowedDoorDirections);
            if (doorIndex == -1)
            {
                return;
            }
            string monsterColor = GetRandomMonsterColor();
            
            openedDoors[doorIndex] = monsterColor;
        
            doorManager.OpenDoor(doorIndex, monsterColor);
            soundManager.PlayHeartbeatSound(doorIndex);
        }
    }
    
    private float GetNextMonsterSpawnDelay()
    {
        if (currentLevelIndex < levelDatas.Length)
        {
            LevelData currentLevelData = levelDatas[currentLevelIndex];
            int monsterIndex = UnityEngine.Random.Range(0, currentLevelData.monsters.Length);
            return currentLevelData.monsters[monsterIndex].spawnDelay;
        }
        
        return 0f;
    }
    
    private int GetRandomDoorIndex(DoorDirection allowedDirections)
    {
        int minWallIndex = 0;
        int maxWallIndex = 0;
    
        switch (allowedDirections)
        {
            case DoorDirection.Front:
                maxWallIndex = 0;
                break;
            case DoorDirection.FrontAndSide:
                maxWallIndex = 2;
                break;
            case DoorDirection.All:
                maxWallIndex = 3;
                break;
        }
    
        int wallIndex = UnityEngine.Random.Range(minWallIndex, maxWallIndex);
        Debug.Log($"Min: {minWallIndex}, Max: {maxWallIndex}, Wall: {wallIndex}");
        
        // 중복된 문이 열리지 않도록 처리
        int DoorIndex = wallIndex * 3 + UnityEngine.Random.Range(0, 3);
        
        if (openedDoors.ContainsKey(DoorIndex))
        {
            return -1; // GetRandomDoorIndex(allowedDirections);
        }
        
        return DoorIndex;
    }
    
    private string GetRandomMonsterColor()
    {
        string[] monsterColors = { "Red", "Blue", "Green", "Yellow" };
        int colorIndex = UnityEngine.Random.Range(0, monsterColors.Length);
        return monsterColors[colorIndex];
    }
    
    public string GetMonsterColorByDoorIndex(int doorIndex)
    {
        if (openedDoors.ContainsKey(doorIndex))
        {
            return openedDoors[doorIndex];
        }
        return string.Empty;
    }

    public void OnMonsterDefeated(int doorIndex)
    {
        // 몬스터 처치 시 호출되는 메서드
        if (openedDoors.ContainsKey(doorIndex))
        {
            string monsterColor = openedDoors[doorIndex];
            openedDoors.Remove(doorIndex);
            doorManager.CloseDoor(doorIndex);
            soundManager.StopHeartbeatSound();
        
            if (!IsLevelInfinite())
            {
                Debug.Log($"Monster {monsterColor} defeated at door {doorIndex}!");
                // 레벨 클리어 조건 체크
                if (IsLevelCleared())
                {
                    Debug.Log("Level Clear!");
                    gameManager.NextLevel();
                }
            }
        }
    }
    
    private bool IsLevelInfinite()
    {
        if (currentLevelIndex < levelDatas.Length)
        {
            LevelData currentLevelData = levelDatas[currentLevelIndex];
            return currentLevelData.isInfinite;
        }
        
        return false;
    }
    
    private bool IsLevelCleared()
    {
        // 레벨 클리어 조건 체크 로직 구현
        // 현재는 항상 true 반환하도록 임시 구현
        return false;
    }
}
