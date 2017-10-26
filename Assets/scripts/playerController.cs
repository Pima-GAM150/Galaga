using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float moveSpeed = 10.0f;                             // The players movement speed
                                                               
    public GameObject startWeapon;                              // The players initial 'turret' gameobject
                                                              
    public GameObject explosion;                                  // Reference to the Expolsion prefab
    public GameObject playerBullet;                             // Reference to the players bullet prefab
                                                                
    private Rigidbody2D playerRigidbody;                      // The players rigidbody: Required to apply directional force to move the player
    private Renderer playerRenderer;                            // The Renderer for the players ship sprite
    private CircleCollider2D playerCollider;                // The Players ship collider
    public List<GameObject> activePlayerTurrets;
    private GameObject leftBoundary;                   //
    private GameObject rightBoundary;                  // References to the screen bounds: Used to ensure the player
    private GameObject topBoundary;                    // is not able to leave the screen.
    private GameObject bottomBoundary;                 //

    private AudioSource shootSoundFX;                           // The player shooting sound effect


    void Start()
    {
        leftBoundary = gamemanager.SharedInstance.leftBoundary;
        rightBoundary = gamemanager.SharedInstance.rightBoundary;
        topBoundary = gamemanager.SharedInstance.topBoundary;
        bottomBoundary = gamemanager.SharedInstance.bottomBoundary;

        playerCollider = gameObject.GetComponent<CircleCollider2D>();
        playerRenderer = gameObject.GetComponent<Renderer>();
        activePlayerTurrets = new List<GameObject>();
        activePlayerTurrets.Add(startWeapon);
        shootSoundFX = gameObject.GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }
        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");
        playerRigidbody.velocity = new Vector2(xDir * moveSpeed, yDir * moveSpeed);
        playerRigidbody.position = new Vector2
            (
                Mathf.Clamp(playerRigidbody.position.x, leftBoundary.transform.position.x, rightBoundary.transform.position.x),
                Mathf.Clamp(playerRigidbody.position.y, bottomBoundary.transform.position.y, topBoundary.transform.position.y)
            );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Powerup")
        {
        }
        else if (other.gameObject.tag == "Enemy Ship 1" || other.gameObject.tag == "Enemy Ship 2" || other.gameObject.tag == "Enemy Laser")
        {
            gamemanager.SharedInstance.ShowGameOver();  // If the player is hit by an enemy ship or laser it's game over.
            playerRenderer.enabled = false;       // We can't destroy the player game object straight away or any code from this point on will not be executed
            playerCollider.enabled = false;       // We turn off the players renderer so the player is not longer displayed and turn off the players collider
            //playerThrust.Stop();
            Instantiate(explosion, transform.position, transform.rotation);   // Then we Instantiate the explosions... one at the centre and some additional around the players location for a bigger bang!
            for (int i = 0; i < 8; i++)
            {
                Vector3 randomOffset = new Vector3(transform.position.x + Random.Range(-0.6f, 0.6f), transform.position.y + Random.Range(-0.6f, 0.6f), 0.0f);
                Instantiate(explosion, randomOffset, transform.rotation);
            }
            Destroy(gameObject, 1.0f); // The second parameter in Destroy is a delay to make sure we have finished exploding before we remove the player from the scene.
        }
    }

    void Shoot()
    {
        //foreach (GameObject turret in activePlayerTurrets)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet");
            if (bullet != null)
            {
               // bullet.transform.position = turret.transform.position;
                //bullet.transform.rotation = turret.transform.rotation;
                //bullet.SetActive(true);
            }
        }
        shootSoundFX.Play();
    }
    
    IEnumerator ActivateScatterShotTurret()
    {

        // The ScatterShot turret is shot independantly of the spacebar
        // This Coroutine shoots the scatteshot at a reload interval

        while (true)
        {
            //foreach (GameObject turret in scatterShotTurrets)
            {
                GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet");
                if (bullet != null)
                {
                    //bullet.transform.position = turret.transform.position;
                    //bullet.transform.rotation = turret.transform.rotation;
                    bullet.SetActive(true);
                }
            }
            shootSoundFX.Play();
            //yield return new WaitForSeconds(scatterShotTurretReloadTime);
        }
    }
}
