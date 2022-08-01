using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private ParticleSystem explosion;

    [SerializeField]
    private int lives = 3;
    private int score = 0;
    [SerializeField]
    private float respawnInvulnerabilityTime = 3f;
    [SerializeField]
    private float respawnTime = 3f;
    private bool gameOver = false;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Image[] lifeIcons;
    [SerializeField]
    private GameObject gameOverPopup;

    private void Start()
    {
        UpdateScoreText();
        UpdateLifeIcons();
        gameOverPopup.SetActive(false);
    }

    private void Update()
    {
        if (gameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            gameOver = false;
            gameOverPopup.SetActive(false);
            Respawn();
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        if (asteroid.size < 0.75f)
        {
            score += 100;
        }
        else if (asteroid.size < 1.2)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();

        lives--;

        if (lives <= 0)
        {
            UpdateLifeIcons();
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    private void UpdateLifeIcons()
    {
        DisableIcons();

        int activeIcons = Mathf.Min(lives, lifeIcons.Length);

        for (int i = 0; i < activeIcons; i++)
        {
            lifeIcons[i].enabled = true;
        }
    }

    private void DisableIcons()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = false;
        }
    }

    private void Respawn()
    {
        UpdateLifeIcons();
        UpdateScoreText();

        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        lives = 3;
        score = 0;

        gameOverPopup.SetActive(true);
        gameOver = true;
        // Invoke(nameof(Respawn), respawnTime);
    }
}
