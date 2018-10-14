using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{

    public GameObject[] enemy;
    public GameObject EnemyPool;
    // Use this for initialization
    void Start()
    {
        Invoke("CreateEnemy", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //生成敌人
    void CreateEnemy()
    {
        int enemyType = Random.Range(0, enemy.Length);
        Vector3 enemyPos = new Vector3(Random.Range(-2.2f, 2.2f), 4f, 0);
        Instantiate(enemy[enemyType], enemyPos, Quaternion.Euler(new Vector3(0, 0, 0))).transform.parent = EnemyPool.transform;
        Invoke("CreateEnemy", 0.2f);
    }
}
