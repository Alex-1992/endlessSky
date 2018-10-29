using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DG.Tweening;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    //玩家移动速度
    public float speed = 5f;
    public static float AttackNum = 60;
    public static float Max_HP = 500;
    public static float Current_HP = 500;
    public static float HP_Recover_Persecond = 50;
    public static float Max_MP = 200;
    public static float Current_MP = 200;
    public static float MP_Recover_Persecond = 20;
    public static float Def = 0;
    public static bool WUDI = false;
    public static bool IsBlinkFinished = true;

    public static float variable_Attack = 1;

    public static float variable_Bullet = 1;
    public static float variable_Range = 1;
    public static float variable_Single = 1;
    public static float variable_Continue = 1;

    public static Vector3 playerPosition;

    // public Button btn1;
    // public Button btn2;
    // public Button btn3;

    //public GameObject EnemyPool;
    public Scrollbar progressBarHP;
    public Scrollbar progressBarMP;
    //public GameObject Range_energy;
    // //子弹发射点
    // public GameObject shotPointMiddle;
    // public GameObject shotPointLeft;
    // public GameObject shotPointRight;
    // public GameObject shotPointMiddle_down;
    // public GameObject shotPointLeft_down;
    // public GameObject shotPointRight_down;

    // //玩家发射子弹CD
    // private float shotCD = 0;
    // public float CD = 0.3f;

    public static SpriteRenderer spr;

    //声音资源
    public AudioSource audio;

    public List<SkillData> skillList = new List<SkillData> ();
    public List<SkillData> playerSkillList;

    // Use this for initialization
    void Start () {
        spr = gameObject.GetComponent<SpriteRenderer> ();
        audio = GetComponent<AudioSource> ();
        //playerPosition = gameObject.transform.position;
        string skillInfo = File.ReadAllText ("Assets/Json/Skill.json", Encoding.UTF8);
        skillList = JsonMapper.ToObject<List<SkillData>> (skillInfo);

        playerSkillList = new List<SkillData> (skillList);
        Debug.Log (playerSkillList[0].describe);
        Debug.Log (playerSkillList[1].describe);
        Debug.Log (playerSkillList[2].describe);
    }

    // Update is called once per frame
    void Update () {
        playerPosition = transform.position;

        //----------------玩家移动----------------
        //向上移动
        if (Input.GetKey (KeyCode.UpArrow) && transform.position.y <= 3.8f) {
            transform.Translate (Vector3.up * Time.deltaTime * speed);
        }
        //向下移动
        if (Input.GetKey (KeyCode.DownArrow) && transform.position.y >= -3.2f) {
            transform.Translate (Vector3.down * Time.deltaTime * speed);
        }
        //向左移动
        if (Input.GetKey (KeyCode.LeftArrow) && transform.position.x >= -5.2f) {
            transform.Translate (Vector3.left * Time.deltaTime * speed);
        }
        //向右移动
        if (Input.GetKey (KeyCode.RightArrow) && transform.position.x <= 5.2f) {
            transform.Translate (Vector3.right * Time.deltaTime * speed);
        }

        if (Current_HP < Max_HP) {
            Current_HP = Current_HP + HP_Recover_Persecond / 30;
            progressBarHP.size = Current_HP / Max_HP;
        }

        if (Current_MP < Max_MP) {
            Current_MP = Current_MP + MP_Recover_Persecond / 30;
            progressBarMP.size = Current_MP / Max_MP;
        }

        if (Current_HP <= 0) {
            //progressBarMP.size = 0;
            GameObject.Find ("Main Camera").GetComponent<GameControl> ().GameOver ();
            //new GameControl().GameOver ();
            //Destroy(gameObject);
        }
        //obj.gameObject.GetComponent<PlayerControl>().GameOver();

    }

    public static void SufferDamage (float damage) {

        if (WUDI == false) {
            Current_HP -= damage;
            Sequence s = DOTween.Sequence ();
            s.Append (spr.DOColor (Color.red, 0.05f)).Append (spr.DOColor (Color.white, 0.05f));
        }

        //.From();
    }

    public List<SkillData> GetSkillList () {
        return playerSkillList;
    }

    private SkillData FetchSkillByName (string name) {
        foreach (SkillData s in playerSkillList) {

            if (s.name == name) {
                return s;
            }
        }
        return null;
    }

    void OnTriggerEnter (Collider obj) {
        //Debug.Log(obj.gameObject.GetComponent<EnemyControl>().HP);
        if (obj.gameObject.name == "DropItem" || obj.gameObject.name == "DropItem(Clone)") {
            if (obj.gameObject.GetComponent<DropItem> ().Type == "skill") {
                Debug.Log ("捡到宝了！");
                //Debug.Log (obj.gameObject.GetComponent<DropItem> ().SkillName);
                //Debug.Log (FetchSkillByName (obj.gameObject.GetComponent<DropItem> ().SkillName));
                SkillData skillInBag = FetchSkillByName (obj.gameObject.GetComponent<DropItem> ().SkillName);
                if (skillInBag != null) {
                    //表示已有该技能,比较等级自动升级
                    if (obj.gameObject.GetComponent<DropItem> ().SkillLevel > int.Parse (skillInBag.level)) {
                        //表示掉落技能等级比已有技能等级高
                        //问题 怎么根据脚本名字改变对应技能脚本里的level(没挂载在object上)
                    }
                } else {
                    //表示没有该技能
                }
            }
        }
    }
}