using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void ShowGameOverUI()
    {
        // 게임 오버 UI 표시 로직 처리
        gameOverUI.SetActive(true);
    }
}