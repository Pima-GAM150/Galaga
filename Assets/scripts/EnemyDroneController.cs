using UnityEngine;
using System.Collections;

public class EnemyDroneController : MonoBehaviour {

	public GameObject powerUp;
	public GameObject explosion;
	public GameObject smallEnemyBullet;
	public float minReloadTime = 1.0f;
	public float maxReloadTime = 2.0f;

	// Use this for initialization
	void Start () {
    StartCoroutine("Shoot");
	}

  IEnumerator Shoot() {
    yield return new WaitForSeconds((Random.Range(minReloadTime, maxReloadTime)));
    while (true) {
		  Instantiate(smallEnemyBullet, gameObject.transform.position, gameObject.transform.rotation);
      yield return new WaitForSeconds((Random.Range(minReloadTime, maxReloadTime)));
    }
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Boundary" && other.gameObject.name != "Top Boundary") {
      gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player Bullet") {
      GameController.SharedInstance.IncrementScore(100);
			float randomNumber = Random.Range(0.0f, 10.0f);

			Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
      other.gameObject.SetActive(false);
      gameObject.SetActive(false);
		}
	}
}
