using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Attributes")]
    [SerializeField] float health = 100;
    [SerializeField] int score = 100;

    [Header("Shoot")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile = null;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Death")]
    [SerializeField] GameObject deathVFX = null;

	void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
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

        //TODO: ShootSFX
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            if (gameSession)
            {
                gameSession.AddScore(score); //Sum the Score
                gameSession.killOneEnemy(); //game recognize that a enemy died
            }

            GameObject deathParticles = Instantiate(
            deathVFX,
            transform.position,
            Quaternion.identity
            ) as GameObject;

            Destroy(deathParticles, 1f);
            Destroy(gameObject);
        }
    }
}
