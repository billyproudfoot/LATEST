using UnityEngine;
using System;
using System.IO;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int health = 100;
    bool died = false;
    public HealthBar healthBar;
    public endScreen endscreen;
    public timer timer;
    public static Player instance;
    public int enemies = 0;
    public Launch launch;
    public int totalEnemies;

    void Start()
    {
        totalEnemies = launch.getEnemies();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0f && died == false)
        {
            died = true;
            die();
        }
    }

    public void killCount()
    {
        enemies++;
        if (enemies == totalEnemies)
        {
            timer.EndTimer();
            File.WriteAllText("gameSettings.txt", timer.getTime());
            StartCoroutine(winMenu());
        }
    }

    IEnumerator winMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void die()
    {
        timer.EndTimer();
        healthBar.lose();
        File.WriteAllText("gameSettings.txt", timer.getTime() + "," + enemies);
        Destroy(gameObject);
    }
}
