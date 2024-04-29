using UnityEngine;

public class OptionManager : MonoBehaviour
{
    private const float targetAspect = 16f / 9f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return))
        {
            ToggleFullScreen();
        }

        UpdateResolution();
    }

    private void ToggleFullScreen()
    {
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.SetResolution((int)(Screen.currentResolution.height * targetAspect), Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
        }
        else if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            Screen.SetResolution((int)(Screen.currentResolution.height * targetAspect), Screen.currentResolution.height, FullScreenMode.Windowed);
        }
    }

    private void UpdateResolution()
    {
        int width = (int)(Screen.height * targetAspect);
        int height = Screen.height;

        if (Screen.width != width || Screen.height != height)
        {
            Screen.SetResolution(width, height, Screen.fullScreen);
        }
    }
}