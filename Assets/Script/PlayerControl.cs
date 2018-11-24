using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using DG.Tweening;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    //玩家移动速度
    public float speed = 5f;
    public static float AttackNum = 60;
    public static float Max_HP = 1000;
    public static float Current_HP = 1000;
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
    public GameObject Button1;
    public GameObject Button3;
    public GameObject ArtifactPanel;
    public GameObject ArtifactPrefab;

    // public Button btn1;
    // public Button btn2;
    // public Button btn3;

    //public GameObject EnemyPool;
    public Scrollbar progressBarHP;
    public Scrollbar progressBarMP;
    public GameObject Range_energy;
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

    public List<SkillData> skillList = new List<SkillData>();
    public List<SkillData> playerSkillList;
    public List<ArtifactData> artifactList = new List<ArtifactData>()
    { new ArtifactData("0","增幅器","带有 范围 词缀的技能的范围效果增加200%（LvMax）","1"),
      new ArtifactData("1","加速器","带有 弹道 词缀的技能的攻击速度增加400%（LvMax）","1"),
      new ArtifactData("2","聚能器","当你在4秒内没有攻击时，将会获得一个持续1秒的伤害buff，提高造成的伤害60%（Lv1）","1"),
      new ArtifactData("3","对撞器","当你对敌人造成伤害时，你距离敌人越近，伤害越高。最高加成40%（Lv1）","1"),
      new ArtifactData("4","重置器","当你消灭敌人时，有10%的几率刷新技能冷却。有3%的几率重置技能冷却。（Lv1）","1")
    };
    public List<ArtifactData> playerArtifactList = new List<ArtifactData>();
    // Use this for initialization
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        //playerPosition = gameObject.transform.position;
        string skillInfo = File.ReadAllText("Assets/Json/Skill.json", Encoding.UTF8);
        skillList = JsonMapper.ToObject<List<SkillData>>(skillInfo);

        playerSkillList = new List<SkillData>();
        playerSkillList.Add(skillList[0]);
        playerSkillList[0].bond_btn = "Button1";
        playerSkillList[0].keycode = "Q";
        Debug.Log(playerSkillList[0].describe);
        Button1.GetComponent<BtnBondSkill>().SetDescribe(playerSkillList[0].describe);

        //Debug.Log (playerSkillList[1].describe);
        //Debug.Log (playerSkillList[2].describe);
        //Debug.Log (Tools.SetSkillLevelByName("Skill_jianzaihuopao", 10));
        //Debug.Log (Tools.SetSkillLevelByName("Skill_shanxiandaji", 10));
        //Debug.Log (Tools.SetSkillLevelByName("Skill_nengliangchang", 10));
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;

        //----------------玩家移动----------------
        //向上移动
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y <= 3.8f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        //向下移动
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= -3.2f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        //向左移动
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -5.2f)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        //向右移动
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 5.2f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        if (Current_HP < Max_HP)
        {
            Current_HP = Current_HP + HP_Recover_Persecond / 30;
            progressBarHP.size = Current_HP / Max_HP;
        }

        if (Current_MP < Max_MP)
        {
            Current_MP = Current_MP + MP_Recover_Persecond / 30;
            progressBarMP.size = Current_MP / Max_MP;
        }

        if (Current_HP <= 0)
        {
            //progressBarMP.size = 0;
            GameObject.Find("Main Camera").GetComponent<GameControl>().GameOver();
            //new GameControl().GameOver ();
            //Destroy(gameObject);
        }
        //obj.gameObject.GetComponent<PlayerControl>().GameOver();

    }

    public static void SufferDamage(float damage)
    {

        if (WUDI == false)
        {
            Current_HP -= damage;
            Sequence s = DOTween.Sequence();
            s.Append(spr.DOColor(Color.red, 0.05f)).Append(spr.DOColor(Color.white, 0.05f));
        }

        //.From();
    }

    public List<SkillData> GetPlayerSkillList()
    {
        return playerSkillList;
    }

    public List<SkillData> GetTotalSkillList()
    {
        return skillList;
    }

    public List<ArtifactData> GetPlayerArtifactList()
    {
        return playerArtifactList;
    }

    public List<ArtifactData> GetTotalArtifactList()
    {
        return artifactList;
    }

    private SkillData FetchSkillByNameTotal(string name)
    {
        foreach (SkillData s in skillList)
        {

            if (s.name == name)
            {
                return s;
            }
        }
        return null;
    }

    private ArtifactData FetchAtByIdTotal(string id)
    {
        foreach (ArtifactData s in artifactList)
        {

            if (s.id == id)
            {
                return s;
            }
        }
        return null;
    }


    private SkillData FetchSkillByName(string name)
    {
        foreach (SkillData s in playerSkillList)
        {

            if (s.name == name)
            {
                return s;
            }
        }
        return null;
    }

    void OnTriggerEnter(Collider obj)
    {
        //Debug.Log(obj.gameObject.GetComponent<EnemyControl>().HP);
        if (obj.gameObject.name == "DropItem" || obj.gameObject.name == "DropItem(Clone)")
        {
            DropItem dropContent = obj.gameObject.GetComponent<DropItem>();
            if (dropContent.Type == "skill")
            {
                Debug.Log("掉落技能！");
                //Debug.Log (dropContent.SkillName);
                //Debug.Log (FetchSkillByName (dropContent.SkillName));
                SkillData skillInBag = FetchSkillByName(dropContent.name);
                if (skillInBag != null)
                {
                    //表示已有该技能,比较等级自动升级或者创建新技能
                    int DropSkillLevel = int.Parse(dropContent.level);
                    Debug.Log("技能名字" + dropContent.name);
                    Debug.Log("技能等级" + DropSkillLevel);
                    if (DropSkillLevel > int.Parse(skillInBag.level))
                    {
                        //表示掉落技能等级比已有技能等级高
                        //问题 怎么根据脚本名字改变对应技能脚本里的level(没挂载在object上) 暂用反射

                        //string currentSkillName = "Skill_jianzaihuopao";
                        string skillScript = skillInBag.script;
                        //string currentSkillName = obj.gameObject.GetComponent<DropItem>().SkillName;
                        print(skillScript);
                        Type t = Type.GetType(skillScript);
                        //删除原来技能脚本
                        //Destroy(gameObject.GetComponent(t));
                        //gameObject.AddComponent(t);
                        print("gameObject.GetComponent(t)" + gameObject.GetComponent(t));
                        SkillBase ct = (SkillBase)gameObject.GetComponent(t);
                        ct.SkillLevel = DropSkillLevel;
                        skillInBag.level = DropSkillLevel + "";
                        //gameObject.GetComponent(t).SkillLevel = DropSkillLevel;
                        // Debug.Log ("311111111111111111111111111111111111111");
                        //// gameObject.AddComponent (t);
                        //string skillScript = skillInBag.script;
                        //// Tools.SetSkillLevelByName("skillScript", int.Parse(skillInBag.level));
                        if(skillInBag.bond_btn == "Button1")
                        {
                            GameObject.FindGameObjectWithTag("BTN1").GetComponent<BtnBondSkill>().SetLevelText("Lv" + DropSkillLevel);
                        }else if (skillInBag.bond_btn == "Button3")
                        {
                            GameObject.FindGameObjectWithTag("BTN3").GetComponent<BtnBondSkill>().SetLevelText("Lv" + DropSkillLevel);
                        }
                        

                        ////Debug.Log ("技能脚本名" + skillScript);
                        ////Type t = Type.GetType (skillScript);

                        ////Debug.Log ("Skill Type:" + t);
                        ////Type tp = typeof(skillScript);
                        //MethodInfo setLevel = t.GetMethod ("SetSkillLevel");
                        //// Debug.Log ("Skill Type:" + t);
                        //object o = t.Assembly.CreateInstance (skillScript);
                        ////object o = Activator.CreateInstance (t);
                        ////Debug.Log ("Activator.CreateInstance (t) object:" + o);
                        //setLevel.Invoke (o, new object[] { DropSkillLevel });
                        //skillInBag.level = DropSkillLevel + "";
                        Destroy(obj.gameObject);
                        //Skill_jianzaihuopao.SetSkillLevel (3);
                    }
                    else
                    {
                        //表示有该技能 但掉落技能等级低于等于现有，销毁掉落技能，回收金币
                        Destroy(obj.gameObject);
                    }
                }
                else
                {
                    //表示没有该技能 新加技能
                    print("新加技能！！！！！");
                    SkillData sd = FetchSkillByNameTotal(dropContent.name);
                    sd.level = dropContent.level + "";
                    playerSkillList.Add(sd);
                    Destroy(obj.gameObject);
                }
            }
            if (dropContent.Type == "artifact")
            {
                if (playerArtifactList.Count == 0)
                {
                    //没有该神器，新加;
                    print("新加神器");
                    print("神器等级:" + dropContent.level);
                    ArtifactData ad = FetchAtByIdTotal(dropContent.id);
                    ad.level = dropContent.level + "";
                    playerArtifactList.Add(ad);
                    GameObject go = Instantiate(ArtifactPrefab, ArtifactPanel.transform);
                    go.GetComponent<Artifact>().SetContent(ad.level, ad.name + "\n" + ad.describe);
                    Destroy(obj.gameObject);
                    return;
                }
                foreach (var at in playerArtifactList)
                {
                    if (at.id == dropContent.id)
                    {
                        //表示已有该神器，判断等级 
                        Destroy(obj.gameObject);
                        return;
                    }
                }
                //没有该神器，新加;

                if (dropContent.id == "0")
                {
                    //范围
                    Range_energy.transform.localScale *= 2f;
                }
                else
                {
                    //弹道
                    gameObject.GetComponent<Skill_jianzaihuopao>().coldTime = 0.05f;
                }

                print("新加神器");
                print("神器等级:" + dropContent.level);
                ArtifactData add = FetchAtByIdTotal(dropContent.id);
                add.level = dropContent.level + "";
                playerArtifactList.Add(add);
                GameObject goo = Instantiate(ArtifactPrefab, ArtifactPanel.transform);
                goo.GetComponent<Artifact>().SetContent(add.level, add.name + "\n" + add.describe);

                Destroy(obj.gameObject);
            }
        }
    }
}