using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    public Score score;

    private bool gameOver;
    private bool paused;

    void Start()
    {
        Initialise();
        //! Start spawning
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            paused = togglePause();
        }
    }

    public void Initialise()
    {
        //! Not in game over nor paused state
        gameOver = false;
        paused = false;

        //! Time could be stopped due to pause, so start again
        Time.timeScale = 1.0f;
    }

    IEnumerator SpawnWaves()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(startWait);

            for (int i = 0; i < hazardCount; ++i)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];

                //! Define the random X position and spawnPosition
                float randomX = Random.Range(-spawnValues.x, spawnValues.x);
                Vector3 spawnPosition = new Vector3(randomX, spawnValues.y, spawnValues.z);
                //! Define the spawnRotation
                Quaternion spawnRotation = Quaternion.identity;
                //! Instantiate the hazard
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                break;
            }
        }
    }

    public void AddScore(int increment)
    {
        score.AddScore(increment);
    }

    public void GameOver()
    {
        gameOver = true;
        showGameOverMenu();
    }

    private bool togglePause()
    {
        if (paused)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            return false;
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            return true;
        }
    }

    private void showGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

}
