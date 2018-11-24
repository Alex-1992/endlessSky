using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using System;
//using UnityEditor.SceneManagement;
public class GameControl : MonoBehaviour
{

    public GameObject player;
    public Button BtnContinueGame;
    public GameObject[] enemy;
    public GameObject EnemyPool;
    public bool enemyCreating = false;
    public static bool enemyCanMove = true;
    public GameObject btnRestart;
    public Text GameOverText;
    public Text NextLevelText;
    public GameObject dropItem;
    public GameObject battleCanvas;

    public static int currentLevel = 1;

    //Dictionary<string, float> dropChance = new Dictionary<string, float> ();
    public List<DropData> dropChance = new List<DropData>() {
        new DropData { name = "skill", chance = 0.2f },
        new DropData { name = "artifact", chance = 0.2f }
    };

    public class DropData
    {
        public string name;
        public float chance;
        //JsonMapper.ToObject 方法要求类不能有构造函数
    }

    List<Vector3> enemyPosList = new List<Vector3>() {
        new Vector3 (-5.2f, 1, 0), new Vector3 (-5.2f, 2, 0), new Vector3 (-5.2f, 3, 0),
        new Vector3 (-1.5f, 4, 0), new Vector3 (0, 4, 0), new Vector3 (1.5f, 4, 0),
        new Vector3 (5.2f, 1, 0), new Vector3 (5.2f, 2, 0), new Vector3 (5.2f, 3, 0)
    };


    private void ShowNextLevelText()
    {
        NextLevelText.text = "Level:" + (currentLevel - 1);
        Sequence s = DOTween.Sequence();
        s.Append(NextLevelText.DOFade(1, 1));
        s.Append(NextLevelText.DOFade(0, 1));
    }
    void Update()
    {

        if (EnemyPool.transform.childCount == 0)
        {
            if (enemyCreating == false)
            {
                //Debug.LogError("enemyCreating!");
                Invoke("CreateEnemy", 3.0f);
                Invoke("ShowNextLevelText", 1.0f);
                currentLevel++;
                enemyCreating = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                BtnContinueGame.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                BtnContinueGame.gameObject.SetActive(false);
            }

        }

    }

    //生成敌人
    void CreateEnemy()
    {
        List<Vector3> tempList = new List<Vector3>(enemyPosList);

        //Vector3 enemyPos = new Vector3 (Random.Range (-2.2f, 2.2f), 4f, 0);
        for (int i = 0; i < enemyPosList.Count; i++)
        {
            //int enemyType = 1;
            int enemyType = Random.Range(0, enemy.Length);
            int RandomIndex = Random.Range(0, tempList.Count);
            //Debug.LogError(enemyPosList.Count);
            //Debug.LogError(i);
            //Vector3 enemyPos = new Vector3 (Random.Range (-2.2f, 2.2f), 4f, 0);
            GameObject go = Instantiate(enemy[enemyType], tempList[RandomIndex], Quaternion.Euler(new Vector3(0, 0, 0)), EnemyPool.transform);
            if (enemyType == 0)
            {
                go.GetComponent<Enemy0>().SetHP(0.8f + 0.2f * currentLevel);
            }
            else
            {
                go.GetComponent<Enemy1>().SetHP(0.8f + 0.2f * currentLevel);
            }


            tempList.Remove(tempList[RandomIndex]);
        }
        enemyCreating = false;
        //Instantiate(enemy[enemyType], enemyPos, Quaternion.Euler(new Vector3(0, 0, 0))).transform.parent = EnemyPool.transform;
        //Invoke("CreateEnemy", 0.2f);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        BtnContinueGame.gameObject.SetActive(false);
    }
    public static void ShakeScreen(float time)
    {
        Camera.main.DOShakePosition(time, new Vector3(0.05f, 0, 0));
        //Camera.main.DOShakePosition(time, new Vector3(0.05f,0,0));
        //Camera.main.transform.position.x += 1f;

    }

    public void ReStartGame()
    {
        //Application.LoadLevel ("SampleScene");
        currentLevel = 1;
        SceneManager.LoadScene("SampleScene");
        //Instantiate (player, new Vector3(0,-2,0), Quaternion.Euler (new Vector3 (0, 0, 0)));
        PlayerControl.Current_HP = PlayerControl.Max_HP;
        GameOverText.text = "";
        btnRestart.SetActive(false);
        player.SetActive(true);
    }

    public void GameOver()
    {
        player.SetActive(false);
        //progressBarHP.size = 0;
        //gameObject.SetActive (false);
        GameOverText.text = "GAME OVER";
        //Destroy (gameObject);
        btnRestart.SetActive(true);
        //gameObject.GetComponent<SpriteRenderer>().color ="#FF0000";
        //gameObject.GetComponent<SpriteRenderer>().color =Color.red;
    }

    public void CountIfDropItem(Vector3 position)
    {
        //判断掉落什么
        // Random rd = new Random ();
        // int a = rd.Next(100);
        // float f = (float) (a * 0.01);

        float dropRuler = Random.Range(0, 1f);
        // for (int i = 0; i < dropChance.Count; i++) {
        //     if()
        // }
        //Debug.Log ("dropRuler" + dropRuler);
        foreach (DropData data in dropChance)
        {
            if (data.chance >= dropRuler)
            {
                //掉落相关物品
                CreatDropItem(data.name, position);
                break;
            }
            else
            {
                dropRuler -= data.chance;
            }
        }
        //判断掉率物品的数值
    }
    private void CreatDropItem(string itemName, Vector3 position)
    {
        //Debug.Log ("掉落物品" + itemName);
        if (itemName == "skill")
        {
            //技能类别 技能等级
            List<SkillData> SkillList = player.GetComponent<PlayerControl>().GetTotalSkillList();
            int randomIndex = Random.Range(0, SkillList.Count);
            string skillName = SkillList[randomIndex].name;
            int skillLevel = GetItemLevel();
            string imgName = SkillList[randomIndex].img;
            GameObject dropObj = Instantiate(dropItem, position, Quaternion.Euler(new Vector3(0, 0, 0)));
            dropObj.transform.parent = battleCanvas.transform;
            dropObj.GetComponent<DropItem>().level = skillLevel+"";
            dropObj.GetComponent<DropItem>().name = skillName;
            dropObj.GetComponent<DropItem>().Type = "skill";
            print("Pic/skill/" + imgName);
            dropObj.GetComponent<Image>().sprite = Instantiate(Resources.Load<Sprite>("Pic/skill/" + imgName));

            Text skillText = dropObj.transform.Find("Text").GetComponent<Text>();
            skillText.text = "Lv" + skillLevel;

            if (skillLevel == currentLevel + 3)
            {
                skillText.color = Color.red;
            }
            else if (skillLevel > currentLevel)
            {
                skillText.color = Color.yellow;
            }
            else
            {
                skillText.color = Color.green;
            }

            // Debug.Log (SkillList[Random.Range (0, SkillList.Count)].name);
            // Debug.Log (Random.Range (0, SkillList.Count));
        }
        else if (itemName == "artifact")
        {
            List<ArtifactData> artifactList = player.GetComponent<PlayerControl>().GetTotalArtifactList();
            int randomIndex = Random.Range(0, artifactList.Count);
            //string artifact = artifactList[randomIndex].name;
            int artifactLevel = GetItemLevel();
            //string imgName = SkillList[randomIndex].img;
            GameObject dropObj = Instantiate(dropItem, position, Quaternion.Euler(new Vector3(0, 0, 0)));
            dropObj.transform.parent = battleCanvas.transform;
            DropItem drop = new DropItem(artifactList[randomIndex]);
            //dropObj.GetComponent<DropItem>()();
            dropObj.GetComponent<DropItem>().Type = "artifact";
            dropObj.GetComponent<DropItem>().id = drop.id;
            dropObj.GetComponent<DropItem>().name = drop.name;
            dropObj.GetComponent<DropItem>().describe = drop.describe;
            dropObj.GetComponent<DropItem>().level = artifactLevel+"";
            //dropObj.GetComponent<DropItem>().Type = "artifact";
            //print("Pic/skill/" + imgName);
            //dropObj.GetComponent<Image>().sprite = Instantiate(Resources.Load<Sprite>("Pic/skill/" + imgName));

            Text levelText = dropObj.transform.Find("Text").GetComponent<Text>();
            levelText.text = "Lv" + artifactLevel;

            if (artifactLevel == currentLevel + 3)
            {
                levelText.color = Color.red;
            }
            else if (artifactLevel > currentLevel)
            {
                levelText.color = Color.yellow;
            }
            else
            {
                levelText.color = Color.green;
            }
        }
    }

    private int GetItemLevel()
    {
        return Random.Range(currentLevel - 3 > 0 ? (currentLevel - 3) : 1, currentLevel + 4);
    }

}