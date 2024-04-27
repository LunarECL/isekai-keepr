using UnityEngine;

public class DoorManager : MonoBehaviour
{
    // DoorManager.cs
    public void OpenDoor(int doorIndex, string monsterColor)
    {
        // 문 열기 로직 처리 한 index 에 door 개수는 3개 random 고르기
        
        Debug.Log($"Door {doorIndex} is opening. Monster color: {monsterColor}");
        // 문 열림 애니메이션 재생
        // 문 색상 변경
    }

    public void CloseDoor(int doorIndex)
    {
        // 문 닫기 로직 처리
        Debug.Log($"Door {doorIndex} is closing.");
        // 문 닫힘 애니메이션 재생
        // 문 색상 원래대로 변경
    }
}