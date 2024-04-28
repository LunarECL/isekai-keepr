using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{   
    public static ScoreManager Instance;
    public int score = 0;
    
    public void AddScore(int score)
    {
        this.score += score;
    }
    
    public int GetScore()
    {
        return score;
    }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
