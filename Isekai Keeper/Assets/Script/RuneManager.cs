using UnityEngine;

public class RuneManager : MonoBehaviour
{
    public LevelManager levelManager;
    public DoorManager doorManager;
    public RuneComparator runeComparator;
    public DrawRune drawRune;
    public RoomManager roomManager;

    private void Update()
    {
        // 마법진 그리기 입력 처리
        if (Input.GetMouseButtonUp(0))
        {
            CompareRune(drawRune.GetDrawnPoints());
            drawRune.ClearDrawnPoints();
        }
    }

    private void CompareRune(System.Collections.Generic.List<Vector2> drawnPoints)
    {
        int currentWallIndex = roomManager.GetCurrentWallIndex();
        Debug.Log($"Current wall index: {currentWallIndex}");
        int currentDoorMin = currentWallIndex * 3;
        int currentDoorMax = currentDoorMin + 3;
        
        for (int i = currentDoorMin; i < currentDoorMax; i++)
        {
            string monsterColor = GetMonsterColorByDoorIndex(i);
            if (monsterColor == null)
            {
                continue;
            }
            
            foreach (RuneData rune in runeComparator.preparedRunes)
            {
            
                if (rune.name == monsterColor)
                {
                    bool isMatched = runeComparator.CompareRunePoints(drawnPoints, rune.points);
                    if (isMatched)
                    {
                        Debug.Log($"Rune matched: {rune.name}");
                        levelManager.OnMonsterDefeated(i);
                        break;
                    }
                }
            }
        }
        
    }

    private string GetMonsterColorByDoorIndex(int doorIndex)
    {
        return levelManager.GetMonsterColorByDoorIndex(doorIndex);
    }
}