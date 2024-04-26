using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Main");
    }

    public void MainScene() {
        SceneManager.LoadScene("into");
    }
    
}
