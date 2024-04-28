using DialogueEditor;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public GameObject titles;
    public GameObject tutorial;
    public NPCConversation myConv;
    public GameObject force;

    public AudioSource AudioSource;
    public AudioClip[] audios;
    
    public TMP_Text text;
    
    public void Update()
    {
        // rainbow text from Red to Green to Blue to purple
        text.color = new Color(Mathf.Sin(Time.time), Mathf.Sin(Time.time + 2), Mathf.Sin(Time.time + 4), 1);
    }

    public void StartGame()
    {
        // SceneManager.LoadScene("Main");
        titles.SetActive(false);

        tutorial.SetActive(true);
        ConversationManager.Instance.StartConversation(myConv);
    }
    public void ReStartGame()
    {
        LoadMainScene();
    }
       

    public void StartScene(bool a)
    {
        if (a)
        {
            force.SetActive(true);
            PlayAudiosAndLoadScene(0);
        }
        else
        {
            PlayAudiosAndLoadScene(1);
        }
    }

    private void PlayAudiosAndLoadScene(int audioIndex)
    {
        AudioSource.clip = audios[audioIndex];
        AudioSource.Play();
    
        // Wait for the audio to finish playing before loading the scene
        Invoke("LoadMainScene", AudioSource.clip.length);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

}
