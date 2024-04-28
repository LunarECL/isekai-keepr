using DialogueEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject titles;
    public GameObject tutorial;
    public NPCConversation myConv;
    public GameObject force;

    public AudioSource AudioSource;
    public AudioClip[] audios;

    public void StartGame()
    {
        // SceneManager.LoadScene("Main");
        titles.SetActive(false);

        tutorial.SetActive(true);
        ConversationManager.Instance.StartConversation(myConv);
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
