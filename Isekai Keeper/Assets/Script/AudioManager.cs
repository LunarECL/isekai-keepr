using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources = new AudioSource[4];
    public float duration = 10f;
    public float startVolume = 0.1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleAudioSource(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleAudioSource(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleAudioSource(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ToggleAudioSource(3);
        }
    }

    private void ToggleAudioSource(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            if (audioSources[index].isPlaying)
            {
                TurnOffAudioSource(index);
            }
            else
            {
                StartCoroutine(FadeInAudioSource(index));
            }
        }
    }

    private IEnumerator FadeInAudioSource(int index)
    {
        audioSources[index].volume = startVolume;
        audioSources[index].Play();

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            audioSources[index].volume = Mathf.Lerp(startVolume, 1f, t);
            yield return null;
        }
    }

    public void TurnOnAudioSource(int index)
    {
        if (index >= 0 && index < audioSources.Length && !audioSources[index].isPlaying)
        {
            StartCoroutine(FadeInAudioSource(index));
        }
    }

    public void TurnOffAudioSource(int index)
    {
        if (index >= 0 && index < audioSources.Length && audioSources[index].isPlaying)
        {
            audioSources[index].Stop();
        }
    }
}