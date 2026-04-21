using Unity.VisualScripting;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool isGamePaused { get; private set; } = false;

    public static void isPaused(bool isPaused)
    {
        isGamePaused = isPaused;
    }

    private void Update()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }
}
