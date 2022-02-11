using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public Player player;
    bool died = false;

    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f && died == false)
        {
            died = true;
            player.killCount();
            destroy();
        }
    }

        void destroy()
    {
        Destroy(gameObject);
    }

}