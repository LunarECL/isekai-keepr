using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] audioSources = new AudioSource[4];
    public float duration = 10f;
    public float startVolume = 0.1f;

    public void PlayHeartbeatSound(int doorIndex)
    {
        if (doorIndex >= 0 && doorIndex < audioSources.Length)
        {
            StartCoroutine(FadeInAudioSource(doorIndex));
        }
    }

    public void StopHeartbeatSound()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            TurnOffAudioSource(i);
        }
    }

    private System.Collections.IEnumerator FadeInAudioSource(int index)
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

    private void TurnOffAudioSource(int index)
    {
        if (index >= 0 && index < audioSources.Length && audioSources[index].isPlaying)
        {
            audioSources[index].Stop();
        }
    }
}