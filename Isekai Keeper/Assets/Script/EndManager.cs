using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public GameObject demon;
    // Start is called before the first frame update
    void Update()
    {
        if (demon.gameObject.activeSelf == false)
        {
            LoadRetry();
        }
    }
    private void LoadRetry()
    {
        SceneManager.LoadScene("gameover");
    }
}
