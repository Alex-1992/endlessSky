﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.SceneManagement;
public class GameControl : MonoBehaviour {

    public GameObject player;
    public Button BtnContinueGame;
    public GameObject[] enemy;
    public GameObject EnemyPool;
    public bool enemyCreating = false;
    public static bool enemyCanMove = true;
    public GameObject btnRestart;
    public Text GameOverText;

    List<Vector3> enemyPosList = new List<Vector3> () {
        new Vector3 (-5.2f, 1, 0), new Vector3 (-5.2f, 2, 0), new Vector3 (-5.2f, 3, 0),
        new Vector3 (-1.5f, 4, 0), new Vector3 (0, 4, 0), new Vector3 (1.5f, 4, 0),
        new Vector3 (5.2f, 1, 0), new Vector3 (5.2f, 2, 0), new Vector3 (5.2f, 3, 0)
    };
    // Use this for initialization
    void Start () {
        //Application.LoadLevel ("SampleScene");
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

        if (Input.GetKeyUp (KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space)) {
            if(Time.timeScale > 0)
            {
                Time.timeScale = 0;
                BtnContinueGame.gameObject.SetActive(true);
            }
            else{ 
                Time.timeScale = 1;
                BtnContinueGame.gameObject.SetActive(false);
            }
           
        }

    }

    //生成敌人
    void CreateEnemy () {
        List<Vector3> tempList = new List<Vector3> (enemyPosList);

        //Vector3 enemyPos = new Vector3 (Random.Range (-2.2f, 2.2f), 4f, 0);
        for (int i = 0; i < enemyPosList.Count; i++) {
            //int enemyType = 1;
            int enemyType = Random.Range (0, enemy.Length);
            int RandomIndex = Random.Range (0, tempList.Count);
            //Debug.LogError(enemyPosList.Count);
            //Debug.LogError(i);
            //Vector3 enemyPos = new Vector3 (Random.Range (-2.2f, 2.2f), 4f, 0);
            Instantiate (enemy[enemyType], tempList[RandomIndex], Quaternion.Euler (new Vector3 (0, 0, 0))).transform.parent = EnemyPool.transform;
            tempList.Remove (tempList[RandomIndex]);
        }
        enemyCreating = false;
        //Instantiate(enemy[enemyType], enemyPos, Quaternion.Euler(new Vector3(0, 0, 0))).transform.parent = EnemyPool.transform;
        //Invoke("CreateEnemy", 0.2f);
    }
    public void ContinueGame () {
        Time.timeScale = 1;
        BtnContinueGame.gameObject.SetActive (false);
    }
    public static void ShakeScreen (float time) {
        Camera.main.DOShakePosition (time, new Vector3 (0.05f, 0, 0));
        //Camera.main.DOShakePosition(time, new Vector3(0.05f,0,0));
        //Camera.main.transform.position.x += 1f;

    }

    public void ReStartGame () {
        //Application.LoadLevel ("SampleScene");
        Debug.Log ("qqqqqqqqqqqqqqqqqqqqqqqqq");
        SceneManager.LoadScene ("SampleScene");
        //Instantiate (player, new Vector3(0,-2,0), Quaternion.Euler (new Vector3 (0, 0, 0)));
        PlayerControl.Current_HP = PlayerControl.Max_HP;
        GameOverText.text = "";
        btnRestart.SetActive (false);
        player.SetActive(true);
    }

    public void GameOver () {
        Debug.Log("00000000000000000000");
        player.SetActive(false);
        //progressBarHP.size = 0;
        //gameObject.SetActive (false);
        GameOverText.text = "GAME OVER";
        //Destroy (gameObject);
        btnRestart.SetActive (true);
        //gameObject.GetComponent<SpriteRenderer>().color ="#FF0000";
        //gameObject.GetComponent<SpriteRenderer>().color =Color.red;
    }

}