using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab = null;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Effects")]
    [SerializeField] AudioClip shootSFX = null;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.75f;
    //[SerializeField] AudioClip deathSFX = null;
    //[SerializeField] [Range(0, 1)] float deathSFXVolume = 0.75f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    GameSession gameSession = null;

    void Start () {
        gameSession = FindObjectOfType<GameSession>();
        SetUpMoveBoundaries();
        StartCoroutine(FireContinuously());
    }
 
    void Update () {
        Move();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        float health = gameSession.GetPlayerActualHp() - damageDealer.GetDamage();
        gameSession.SetPlayerActualHp(health);
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            //deathSFX
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

    IEnumerator FireContinuously()
    {
        if (gameSession)
        {
            while (true)
            {
                if (gameSession.CheckIfEnemiesAlive() == true)
                {
                    //Instantiate Shoot
                    GameObject laser = Instantiate(
                            laserPrefab,
                            transform.position,
                            Quaternion.identity) as GameObject;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    laser.GetComponent<DamageDealer>().SetDamage(gameSession.GetPlayerDmg());
                    //Shoot SFX
                    AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
                    //Shoot CD
                    yield return new WaitForSeconds(gameSession.GetPlayerAtkSpeed());
                }
                else
                {
                    Debug.Log("Waiting for Upgrades!!!");
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }

    private void Move()
    {
        if (gameSession)
        {
            if(gameSession.CheckIfEnemiesAlive() == true)
            {
                if (Input.GetMouseButton(0))
                {
                    float step = Time.deltaTime * moveSpeed;

                    Vector3 relativeMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 newMousePosition = new Vector2(relativeMousePosition.x, relativeMousePosition.y);

                    transform.position = Vector2.MoveTowards(transform.position, newMousePosition, step);

                    var clampedXPos = Mathf.Clamp(transform.position.x, xMin, xMax);
                    var clampedYPos = Mathf.Clamp(transform.position.y, yMin, yMax);

                    transform.position = new Vector2(clampedXPos, clampedYPos);
                }
            }
            else
            {
                transform.position = new Vector2(0, yMax/-2);
            }
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
}
