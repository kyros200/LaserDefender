using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Attributes")]
    [SerializeField] float health = 100;
    [SerializeField] float score = 100;

    [Header("Shoot")]
    [SerializeField] float shootDmg = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile = null;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Effects")]
    [SerializeField] AudioClip deathSFX = null;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.75f;
    [SerializeField] GameObject deathVFX = null;

    GameSession gameSession = null;

	void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        //Set Stats based on Wave Number
        gameSession = FindObjectOfType<GameSession>();
        health = health * Mathf.Pow(1.03f, gameSession.GetEnemyWave() - 1);
        shootDmg = shootDmg * Mathf.Pow(1.05f, gameSession.GetEnemyWave() - 1);
        score = score * Mathf.Pow(1.005f, gameSession.GetEnemyWave() - 1);
    }
	
	void Update () {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        laser.GetComponent<DamageDealer>().SetDamage(shootDmg);
        //TODO: ShootSFX
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
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            //death logic
            GameSession gameSession = FindObjectOfType<GameSession>();
            if (gameSession)
            {
                gameSession.AddScore(score); //Sum the Score
                gameSession.killOneEnemy(); //game recognize that a enemy died
            }
            //deathVFX
            GameObject deathParticles = Instantiate(
            deathVFX,
            transform.position,
            Quaternion.identity
            ) as GameObject;
            //deathSFX
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
            //death Destroy Objects
            Destroy(deathParticles, 1f);
            Destroy(gameObject);
        }
    }
}
