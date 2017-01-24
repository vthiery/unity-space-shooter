using UnityEngine;
using System.Collections;

public class UnPauseOnClick : MonoBehaviour
{
    public GameObject pauseMenu;

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
