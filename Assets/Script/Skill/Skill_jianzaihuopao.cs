using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_jianzaihuopao : SkillBase
{

    // public Image imageFilled; //填充背景，及灰色读条
    //public KeyCode skillKey; //对应技能按键
    // public static GameObject currentBtn;//当前绑定的按钮
    // public Text LevelText;

    //private float coldTime = 0.2f; //技能的冷却时间    
    //private float timer = 0; //当前冷却时间    
    //private bool  isCold = false; //是否进入冷却  
    //private float mpCost = 5;
    //   private float SkillDamagePercent = 1;
    //   public int    SkillLevel = 1;

    //public GameObject player;

    //子弹发射点
    GameObject shotPointMiddle;
    GameObject shotPointLeft;
    GameObject shotPointRight;
    GameObject shotPointMiddle_down;
    GameObject shotPointLeft_down;
    GameObject shotPointRight_down;

    GameObject bullet;


    //private void Awake()
    //{
    //    coldTime = 0.2f;
    //    timer = 0; //当前冷却时间 
    //    isCold = false; //是否进入
    //    mpCost = 5;
    //    SkillDamagePercent = 1;
    //    SkillLevel = 1;
    //    print("Awake");
    //    print("SkillDamagePercent:    " + SkillDamagePercent);
    //}
    // Use this for initialization
    void Start()
    {
        coldTime = 0.2f;
        timer = 0; //当前冷却时间 
        isCold = false; //是否进入
        mpCost = 10;
        SkillDamagePercent = 1;
        SkillLevel = 1;
        print("Start");
        print("SkillDamagePercent:    " + SkillDamagePercent);
        //imageFilled.fillAmount = 0;
        shotPointMiddle = GameObject.Find("player/shotPointMiddle");
        shotPointLeft = GameObject.Find("player/shotPointLeft");
        shotPointRight = GameObject.Find("player/shotPointRight");
        shotPointMiddle_down = GameObject.Find("player/shotPointMiddle_down");
        shotPointLeft_down = GameObject.Find("player/shotPointLeft_down");
        shotPointRight_down = GameObject.Find("player/shotPointRight_down");
        //bullet = GameObject.Find ("player/Player_bullet");
        bullet = Resources.Load("Prefabs/Player_bullet") as GameObject;
        //bullet = Resources.Load<GameObject>("Prefabs/Player_bullet")) as GameObject;

        //Debug.Log (bullet);
    }

    // Update is called once per frame
    void Update()
    {
        //hotCD -= Time.deltaTime;
        // if (Input.GetKey (KeyCode.Q)) {
        // 	if (PlayerControl.Current_MP >= 10 && shotCD <= 0) {
        // 		//btn1.transform.DOScale (new Vector3 (1.5f, 1.5f, 1.5f), 0.1f);

        // 		shot ();
        // 	}
        // }

        SkillKeyDown();
        if (isCold == true)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            if (timer > coldTime)
            {
                //冷却完毕，回归默认值               
                isCold = false;
                timer = 0;
                if (imageFilled != null)
                {
                    imageFilled.fillAmount = 0;
                }

            }
            else
            {
                if (imageFilled != null)
                {
                    imageFilled.fillAmount = (coldTime - timer) / coldTime; //冷却比例
                }
            }
        }
    }

    private void SkillKeyDown()
    {
        if (imageFilled != null)
        {
            imageFilled.fillAmount = 0;
        }
        if (Input.GetKey(skillKey) && isCold == false && PlayerControl.Current_MP >= mpCost)
        {
            PlaySkill();
            PlayerControl.Current_MP = PlayerControl.Current_MP - mpCost;
            isCold = true;
            //Debug.Log(isCold);
        }
    }

    //技能释放
    void PlaySkill()
    {

        // Debug.Log (shotPointLeft_down);
        // Debug.Log (shotPointLeft_down.transform.position);
        // Debug.Log (bullet);
        Instantiate(bullet, shotPointMiddle.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Instantiate(bullet, shotPointLeft.transform.position, Quaternion.Euler(new Vector3(0, 0, 60)));
        Instantiate(bullet, shotPointRight.transform.position, Quaternion.Euler(new Vector3(0, 0, -60)));

        Instantiate(bullet, shotPointMiddle_down.transform.position, Quaternion.Euler(new Vector3(-180, 0, 0)));
        Instantiate(bullet, shotPointRight_down.transform.position, Quaternion.Euler(new Vector3(-180, 0, -60)));
        Instantiate(bullet, shotPointLeft_down.transform.position, Quaternion.Euler(new Vector3(-180, 0, 60)));

        //audio.Play();

    }

    // public void SetImg (Image img) {
    // 	imageFilled = img;
    // }

    // public void SetKeyCode (KeyCode k) {
    // 	skillKey = k;
    // }

    // public void SetCurrentBtn (GameObject obj) {
    // 	Debug.Log("SetCurrentBtn" + obj);
    // 	currentBtn = obj;
    // }

    // public void SetSkillLevel (int num) {
    // 	SkillLevel = num;

    // 	//Debug.Log(gameObject);
    // 	Debug.Log(currentBtn);
    // 	Debug.Log(currentBtn.transform.Find("LevelText"));
    // 	Debug.Log(currentBtn.transform.Find("LevelText").GetComponent<Text>().text);
    // 	currentBtn.transform.Find("LevelText").GetComponent<Text>().text = "Lv"+ num;
    // }

    //public float GetSkillDamage () {
    //       print("SkillDamagePercent               " +SkillDamagePercent);
    //       print("this.SkillDamagePercent               " + this.SkillDamagePercent);
    //       Debug.Log(PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float)(this.SkillDamagePercent + this.SkillDamagePercent * (SkillLevel - 1) * 0.2));
    //       return PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float) (SkillDamagePercent + SkillDamagePercent * (SkillLevel - 1) * 0.2);

    //}
    public float GetSkillDamage()
    {
        Debug.Log(PlayerControl.AttackNum * 3 * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float)(SkillDamagePercent + SkillDamagePercent * (SkillLevel - 1) * 0.2));
        return PlayerControl.AttackNum * 3 * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float)(SkillDamagePercent + SkillDamagePercent * (SkillLevel - 1) * 0.2);
    }
}