using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour {

	public static GameController SharedInstance;

	public Text scoreLabel;
	private int currentScore = 0;
	public Text gameOverLabel;
	public Button restartGameButton;

	public GameObject enemyType1;
	public GameObject enemyType2;
    public GameObject enemyType3;
    public GameObject enemyType4;
    public GameObject enemyType5;

    public float startWait = 1.0f;
	public float waveInterval = 2.0f;
	public float spawnInterval = 0.5f;
	public int enemiesPerWave = 5;

  public GameObject leftBoundary;                   
  public GameObject rightBoundary;                  // References to the screen bounds
  public GameObject topBoundary;                    
  public GameObject bottomBoundary;                 

	void Awake () {
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
                if (waveType <= 1.99)
                {
                    GameObject enemy1 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Ship 1");
                    if (enemy1 != null)
                    {
                        enemy1.transform.position = spawnPosition;
                        enemy1.transform.rotation = spawnRotation;
                        enemy1.SetActive(true);
                    }

                }
                else if( waveType <= 3.99f )
                {
                 
                    GameObject enemy2 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Ship 2");
                    if (enemy2 != null)
                    {
                        enemy2.transform.position = spawnPosition;
                        enemy2.transform.rotation = spawnRotation;
                        enemy2.SetActive(true);

                    }
                }
				else if( waveType <= 5.99f )
                {
                 
                    GameObject enemy3 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Ship 3");
                    if (enemy3 != null)
                    {
                        enemy3.transform.position = spawnPosition;
                        enemy3.transform.rotation = spawnRotation;
                        enemy3.SetActive(true);
                    }
                }
                else if (waveType <= 7.99f)
                {

                    GameObject enemy4 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Ship 4");
                    if (enemy4 != null)
                    {
                        enemy4.transform.position = spawnPosition;
                        enemy4.transform.rotation = spawnRotation;
                        enemy4.SetActive(true);
                    }
                }
                else if (waveType <= 5.99f)
                {

                    GameObject enemy5 = ObjectPooler.SharedInstance.GetPooledObject("Enemy Ship 5");
                    if (enemy5 != null)
                    {
                        enemy5.transform.position = spawnPosition;
                        enemy5.transform.rotation = spawnRotation;
                        enemy5.SetActive(true);



                    }
                }
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(waveInterval);
        }
    }

    public void IncrementScore(int increment) {
		currentScore += increment;
		scoreLabel.text = "Score: " + currentScore;
	}

	public void ShowGameOver() {
		gameOverLabel.rectTransform.anchoredPosition3D = new Vector3 (0, 0, 0);
		restartGameButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (0, -50, 0);
	}

	public void RestartGame() {
		SceneManager.LoadScene("GameScene");
	}
}
