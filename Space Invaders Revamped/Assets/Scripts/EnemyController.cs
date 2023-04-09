using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.5f;

    private int speedBoostGot = 0;

    private bool moveRight = true;

    private GameObject gameManager;
    private int initialEnemyCount;

    public List<GameObject> enemyCanFire = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        initialEnemyCount = gameManager.GetComponent<GameManager>().enemies.Count;

    }

    // Update is called once per frame
    void Update()
    {
        CanFire();

        if (moveRight)
        {
            if (transform.position.x >= 23f)
            {
                List<GameObject> enemiesCopy = gameManager.GetComponent<GameManager>().enemies;

                for (int i = 0; i < enemiesCopy.Count; i++)
                {
                    if (enemiesCopy[i] == null) continue;
                    enemiesCopy[i].transform.position = new Vector2(enemiesCopy[i].transform.position.x, enemiesCopy[i].transform.position.y - 1f);
                    enemiesCopy[i].GetComponent<EnemyController>().moveRight = false;
                }

                return;
            }

            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        else
        {
            if (transform.position.x <= -23f)
            {
                List<GameObject> enemiesCopy = gameManager.GetComponent<GameManager>().enemies;

                for (int i = 0; i < enemiesCopy.Count; i++)
                {
                    if (enemiesCopy[i] == null) continue;
                    enemiesCopy[i].transform.position = new Vector2(enemiesCopy[i].transform.position.x, enemiesCopy[i].transform.position.y - 1f);
                    enemiesCopy[i].GetComponent<EnemyController>().moveRight = true;
                }

                return;
            }

            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
        if (speedBoostGot == 0 && gameManager.GetComponent<GameManager>().enemies.Count < Mathf.Floor(initialEnemyCount - (initialEnemyCount * 0.2f)))
        {
            speed = 4;
            speedBoostGot += 1;
        }
        else if (speedBoostGot == 1 && gameManager.GetComponent<GameManager>().enemies.Count < Mathf.Floor(initialEnemyCount - (initialEnemyCount * 0.4f)))
        {
            speed = 6;
            speedBoostGot += 1;
        }
        if (speedBoostGot == 2 && gameManager.GetComponent<GameManager>().enemies.Count < Mathf.Floor(initialEnemyCount - (initialEnemyCount * 0.8f)))
        {
            speed = 10;
        }

        
    }

    void CanFire()
    {
        List<GameObject> enemy = gameManager.GetComponent<GameManager>().enemies;
        int count = gameManager.GetComponent<GameManager>().enemies.Count;

        for (int i = 0; i < count - 30; i++)
        {
            if (enemy[i + 10] == null && !enemyCanFire.Contains(enemy[i])) 
            {
                if (enemy[i + 20] == null)
                {
                    if (enemy[i + 30] == null)
                    {
                        enemyCanFire.Add(enemy[i]);
                    }
                }
            }
        }
        for (int i = 10; i < count - 20; i++)
        {
            if (enemy[i + 10] == null && !enemyCanFire.Contains(enemy[i]))
            {
                if (enemy[i + 20] == null)
                {
                    enemyCanFire.Add(enemy[i]);
                }
            }
        }
        for (int i = 20; i < count - 10; i++)
        {
            if (enemy[i + 10] == null && !enemyCanFire.Contains(enemy[i]))
            {
                enemyCanFire.Add(enemy[i]);
            }
        }
        for (int i = 30; i < count; i++)
        {
            if (enemy[i] != null && !enemyCanFire.Contains(enemy[i]))
            {
                enemyCanFire.Add(enemy[i]);
            }
        }

        for (int i = enemyCanFire.Count - 1; i >= 0; i--)
        {
            if (enemyCanFire[i] == null)
            {
                enemyCanFire.Remove(enemyCanFire[i]);
            }
        }
    }

    void Fire()
    {

    }
}
