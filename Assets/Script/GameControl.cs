using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameControl : MonoBehaviour {

    public Button BtnContinueGame;
    public GameObject[] enemy;
    public GameObject EnemyPool;
    public bool enemyCreating = false;
    public static bool enemyCanMove = true;
    List<Vector3> enemyPosList = new List<Vector3>(){new Vector3(-2.2f,1,0),new Vector3(-2.2f,2,0),new Vector3(-2.2f,3,0),
                                                     new Vector3(-1.5f,4,0),new Vector3(0,4,0),new Vector3(1.5f,4,0),
                                                     new Vector3(2.2f,1,0),new Vector3(2.2f,2,0),new Vector3(2.2f,3,0)};
    // Use this for initialization
    void Start () {
        
        //Invoke ("CreateEnemy", 1.0f);
        //enemyPosList = 
    }

    // Update is called once per frame
    void Update () {

        if (EnemyPool.transform.childCount == 0) {
            if (enemyCreating == false) {
                //Debug.LogError("enemyCreating!");
                Invoke ("CreateEnemy", 1.0f);
                enemyCreating = true;
            }
        }

        if(Time.timeScale > 0 && (Input.GetKey(KeyCode.Escape) ||Input.GetKey(KeyCode.Space))){
            Time.timeScale = 0;
            BtnContinueGame.gameObject.SetActive(true);
        }
    }

    //生成敌人
    void CreateEnemy () {
        List<Vector3> tempList = new List<Vector3>(enemyPosList);
        int enemyType = Random.Range (0, enemy.Length);
        //Vector3 enemyPos = new Vector3 (Random.Range (-2.2f, 2.2f), 4f, 0);
        for (int i = 0; i < enemyPosList.Count; i++) {
            int RandomIndex = Random.Range(0,tempList.Count);
            //Debug.LogError(enemyPosList.Count);
            //Debug.LogError(i);
            //Vector3 enemyPos = new Vector3 (Random.Range (-2.2f, 2.2f), 4f, 0);
            Instantiate (enemy[enemyType], tempList[RandomIndex], Quaternion.Euler (new Vector3 (0, 0, 0))).transform.parent = EnemyPool.transform;
            tempList.Remove(tempList[RandomIndex]);
        }
        enemyCreating = false;
        //Instantiate(enemy[enemyType], enemyPos, Quaternion.Euler(new Vector3(0, 0, 0))).transform.parent = EnemyPool.transform;
        //Invoke("CreateEnemy", 0.2f);
    }
    public void ContinueGame(){
        Time.timeScale = 1;
        BtnContinueGame.gameObject.SetActive(false);
    }
}