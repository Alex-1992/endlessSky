using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Sprite;
//[System.Serializable]

public class BtnBondSkill : MonoBehaviour
{
    private GameObject player;
    //public string thisBtnName;
    public GameObject panel;
    public Image imageFilled;
    public Image imageBack;
    //public Image imageFilled;
    public Text describeText;
    public Text levelText;
    public GameObject describePanel;
    public KeyCode skillKey = KeyCode.Q;

    private bool IsSkillShowed = false;
    public string currentSkillName = "";
    //private Sprite SkillIcon;
    private List<SkillData> SkillList = new List<SkillData>();
    // Use this for initialization
    void Start()
    {
        //1.删除之前的技能脚本。  
        //2.在该按钮挂在对应技能脚本名字写入currentSkillName。 
        //3.绑定img和keycode。

        //currentSkillName = "Skill_jianzaihuopao";
        //Type t = Type.GetType(currentSkillName);
        ////AddComponent<ttt> ();
        //print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        //Debug.Log("Btn1  " + gameObject);
        //gameObject.AddComponent(t);
        ////Destroy(gameObject.GetComponent(t));
        //gameObject.SendMessage("SetImg", imageFilled, SendMessageOptions.RequireReceiver);
        //gameObject.SendMessage("SetKeyCode", skillKey, SendMessageOptions.RequireReceiver);
        //gameObject.SendMessage("SetCurrentBtn", gameObject, SendMessageOptions.RequireReceiver);

        //imageFilled.sprite = Instantiate(Resources.Load<Sprite>("Pic/skill/" + currentSkillName.Substring(6)));
        //imageBack.sprite = Instantiate(Resources.Load<Sprite>("Pic/skill/" + currentSkillName.Substring(6)));

        //gameObject.GetComponent<currentSkillName>().SetImg (imageFilled);
        //.GetComponent<脚本名>().函数名();//只能调用public类型函数
        // gameObject.GetComponent (currentSkillName).SetKeyCode (skillKey);
        //.GetComponent<EnemyControl>().HP
        player = GameObject.FindGameObjectWithTag("Player");
        //thisBtnName = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            OnClick();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        if (IsSkillShowed == true)
        {
            int childCount = panel.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(panel.transform.GetChild(i).gameObject);
            }
            panel.SetActive(false);

            IsSkillShowed = false;
            //Time.timeScale = 1;
        }
        else
        {
            //Time.timeScale = 0;
            //得到玩家当前所有技能  显示技能列表  点击替换
            SkillList = player.GetComponent<PlayerControl>().GetPlayerSkillList();
            panel.SetActive(true);
            GameObject btnObj = Resources.Load("Prefabs/BtnShowSkill") as GameObject;
            foreach (var s in SkillList)
            {
                Button btn = Instantiate(btnObj).GetComponent<Button>();
                btn.transform.parent = panel.transform;
                btn.transform.Find("LevelText").GetComponent<Text>().text = "Lv" + s.level;
                //Debug.Log ("Pic/skill/" + s.img);
                Sprite imgSprite = Instantiate(Resources.Load<Sprite>("Pic/skill/" + s.img));
                btn.GetComponent<Image>().sprite = imgSprite;
                btn.onClick.AddListener(delegate ()
                {
                    ChangeSkill(s);
                });
            }
            IsSkillShowed = true;
        }
        //gameObject.GetComponent<SpriteRenderer> ();

    }
    public void ChangeSkill(SkillData s)
    {

        //1.找到之前与该按钮绑定的技能，解除绑定。  
        //Destroy(gameObject.GetComponent(currentSkillName));
        SkillList = player.GetComponent<PlayerControl>().GetPlayerSkillList();
        foreach (var skill in SkillList)
        {
            if (skill.bond_btn == gameObject.name)
            {
                //表示与按钮绑定的是此技能
                //数据层
                skill.bond_btn = "";
                skill.keycode = "";
                Type type = Type.GetType(skill.script);
                //print("gameObject.GetComponent(t)" + gameObject.GetComponent(t));
                SkillBase sb = (SkillBase)player.GetComponent(type);
                //脚本
                sb.skillKey = KeyCode.None;
                sb.imageFilled = null;
            }
            if (skill.name == s.name)
            {
                //需要替换的技能
                skill.bond_btn = gameObject.name;
                skill.keycode = this.skillKey.ToString();
                Type type = Type.GetType(skill.script);
                //print("gameObject.GetComponent(t)" + gameObject.GetComponent(t));
                SkillBase sb = (SkillBase)player.GetComponent(type);
                //脚本
                sb.skillKey = skillKey;
                sb.imageFilled = imageFilled;
            }
        }

        //2.在该按钮写入选择的技能的describe 和 level
        SetDescribe(s.describe);
        SetLevelText("Lv" + s.level);


        //3.在数据层 和脚本上分别绑定img和keycode。
        //Type t = Type.GetType(s.script);
        //SkillBase ct = (SkillBase)gameObject.GetComponent(t);
        ////脚本
        //ct.skillKey = this.skillKey;
        //ct.imageFilled = this.imageFilled;
        //数据层
        //foreach (var skill in player.GetComponent<PlayerControl>().playerSkillList)
        //{

        //}


        //gameObject.SendMessage("SetImg", imageFilled, SendMessageOptions.RequireReceiver);
        //gameObject.SendMessage("SetKeyCode", skillKey, SendMessageOptions.RequireReceiver);
        //Debug.Log ("78" + gameObject);

        //4.更换当前按钮技能icon
        imageFilled.sprite = Instantiate(Resources.Load<Sprite>("Pic/skill/" + s.img));
        imageBack.sprite = Instantiate(Resources.Load<Sprite>("Pic/skill/" + s.img));

        int childCount = panel.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(panel.transform.GetChild(i).gameObject);
        }
        panel.SetActive(false);

        IsSkillShowed = false;
        //Time.timeScale = 1;
    }
    public void OnMouseEnter()
    {
        print("OnMouseEnter");
        if(describeText.text != "")
        {
            describePanel.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        print("OnMouseExit");
        describePanel.SetActive(false);
    }
    public void SetLevelText(string level)
    {
        levelText.text = level + "";
    }

    public void SetDescribe(string des)
    {
        print("SetDescribe");
        describeText.text = des + "";
    }
}
