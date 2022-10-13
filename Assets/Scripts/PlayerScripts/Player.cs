using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public float health;
    public float damage;

    private void Start()
    {
        health = maxHealth;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) StartCoroutine(playerDie());
    }

    public bool isAlive()
    {
        return health > 0;
    }

    IEnumerator playerDie()
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(2);
        //SceneManager.LoadScene(0);
    }
}
