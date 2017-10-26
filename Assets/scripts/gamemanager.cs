using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gamemanager : MonoBehaviour {


    public static gamemanager SharedInstance;

    public Text scoreLabel;
    private int currentScore = 0;
    public Text gameOverLabel;
    public Button restartGameButton;

    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;


    public float startWait = 1.0f;
    public float waveInterval = 2.0f;
    public float spawnInterval = 0.5f;
    public int enemiesPerWave = 5;

    public GameObject leftBoundary;                   //
    public GameObject rightBoundary;                  // References to the screen bounds: Used to ensure the player
    public GameObject topBoundary;                    // is not able to leave the screen.
    public GameObject bottomBoundary;                 //

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            float waveType = Random.Range(0.0f, 10.0f);
            for (int i = 0; i < enemiesPerWave; i++)
            {
                Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight + 2, 0));
                Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight + 2, 0));
                Vector3 spawnPosition = new Vector3(Random.Range(topLeft.x, topRight.x), topLeft.y, 0);
                Quaternion spawnRotation = Quaternion.Euler(0, 0, 180);
                if (waveType >= 5.0f)
                {
                    GameObject enemy1 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Type 1");
                    if (enemy1 != null)
                    {
                        enemy1.transform.position = spawnPosition;
                        enemy1.transform.rotation = spawnRotation;
                        enemy1.SetActive(true);
                    }

                }
                else
                {
                    GameObject enemy2 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Ship 2");
                    if (enemy2 != null)
                    {
                        enemy2.transform.position = spawnPosition;
                        enemy2.transform.rotation = spawnRotation;
                        enemy2.SetActive(true);
                    }
                }
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(waveInterval);
        }
    }

    public void IncrementScore(int increment)
    {
        currentScore += increment;
        scoreLabel.text = "Score: " + currentScore;
    }

    public void ShowGameOver()
    {
        gameOverLabel.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
        restartGameButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -50, 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Galaga");
    }
}
