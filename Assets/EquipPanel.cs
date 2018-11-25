using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : MonoBehaviour {
    private GameObject player;
    public GameObject panel;
    public Image image;
    public Text text;
    public GameObject describePanel;
    private bool IsEqupShowed = false;
    private string currentEquipId;

    //private float AddAtkNum;
    //private float AddHPMPNum;
    //private float AddMaxHpNum;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //thisBtnName = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            OnClick();
        });
        ////AddAtkNum = 
    }
    public void OnClick()
    {
        print("OnClick");
        if (IsEqupShowed == true)
        {
            int childCount = panel.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(panel.transform.GetChild(i).gameObject);
            }
            panel.SetActive(false);

            IsEqupShowed = false;
            //Time.timeScale = 1;
        }
        else
        {
            //Time.timeScale = 0;
            //得到玩家当前所有技能  显示技能列表  点击替换
            List<EquipData> equipList = player.GetComponent<PlayerControl>().GetPlayerEquipList();
            panel.SetActive(true);
            GameObject btnObj = Resources.Load("Prefabs/BtnShowEquip") as GameObject;
            foreach (var s in equipList)
            {
                Button btn = Instantiate(btnObj).GetComponent<Button>();
                btn.transform.parent = panel.transform;
                //btn.transform.Find("LevelText").GetComponent<Text>().text = "Lv" + s.level;
                //Debug.Log ("Pic/skill/" + s.img);
                Sprite imgSprite = Instantiate(Resources.Load<Sprite>("Pic/equip/" + s.img));
                btn.GetComponent<Image>().sprite = imgSprite;
                btn.onClick.AddListener(delegate ()
                {
                    ChangeEquip(s);
                });
            }
            IsEqupShowed = true;
        }
        //gameObject.GetComponent<SpriteRenderer> ();

    }

    public void ChangeEquip(EquipData s)
    {
        print("ChangeEquip");
        //1.找到之前与该按钮绑定的技能，取消效果  
        //Destroy(gameObject.GetComponent(currentSkillName));
        if (currentEquipId == "1")
        {
            print("before: PlayerControl.AttackNum:" + PlayerControl.AttackNum);
            PlayerControl.AttackNum = PlayerControl.AttackNum / (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.AttackNum:" + PlayerControl.AttackNum);
        }
        else if(currentEquipId == "2")
        {
            print("before: PlayerControl.HP_Recover_Persecond:" + PlayerControl.HP_Recover_Persecond);
            PlayerControl.HP_Recover_Persecond = PlayerControl.HP_Recover_Persecond / (1 + int.Parse(s.value) / 100f);
            PlayerControl.MP_Recover_Persecond = PlayerControl.MP_Recover_Persecond / (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.HP_Recover_Persecond:" + PlayerControl.HP_Recover_Persecond);
        }
        else if(currentEquipId == "3")
        {
            print("before: PlayerControl.Max_HP:" + PlayerControl.Max_HP);
            PlayerControl.Max_HP = PlayerControl.Max_HP / (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.Max_HP:" + PlayerControl.Max_HP);
        }
        //写入新效果
        if (s.id == "1")
        {
            print("before: PlayerControl.AttackNum:" + PlayerControl.AttackNum);
            PlayerControl.AttackNum *=  (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.AttackNum:" + PlayerControl.AttackNum);
        }
        else if (s.id == "2")
        {
            print("before: PlayerControl.HP_Recover_Persecond:" + PlayerControl.HP_Recover_Persecond);
            PlayerControl.HP_Recover_Persecond *= (1 + int.Parse(s.value) / 100f);
            PlayerControl.MP_Recover_Persecond *= (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.HP_Recover_Persecond:" + PlayerControl.HP_Recover_Persecond);
        }
        else if (s.id == "3")
        {
            print("before: PlayerControl.Max_HP:" + PlayerControl.Max_HP);
            PlayerControl.Max_HP *=  (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.Max_HP:" + PlayerControl.Max_HP);
        }
        currentEquipId = s.id;
        //2.在该按钮写入选择的技能的describe 和 level
        SetDescribe(s.name+"\n"+ s.describe);
        //SetLevelText("Lv" + s.level);


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
        image.sprite = Instantiate(Resources.Load<Sprite>("Pic/equip/" + s.img));

        int childCount = panel.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(panel.transform.GetChild(i).gameObject);
        }
        print(" panel.SetActive(false);");
        panel.SetActive(false);

        IsEqupShowed = false;
        //Time.timeScale = 1;
    }

    private void SetDescribe(string describe)
    {
        text.text = describe;
    }
    public void OnMouseEnter()
    {
        if(text.text != "")
        {
            describePanel.SetActive(true);
        }
        print("OnMouseEnter");
        
    }

    public void OnMouseExit()
    {
        print("OnMouseExit");
        describePanel.SetActive(false);
    }

    public void AutoEquip(EquipData s)
    {
        if (currentEquipId == "1")
        {
            print("before: PlayerControl.AttackNum:" + PlayerControl.AttackNum);
            PlayerControl.AttackNum = PlayerControl.AttackNum / (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.AttackNum:" + PlayerControl.AttackNum);
        }
        else if (currentEquipId == "2")
        {
            print("before: PlayerControl.HP_Recover_Persecond:" + PlayerControl.HP_Recover_Persecond);
            PlayerControl.HP_Recover_Persecond = PlayerControl.HP_Recover_Persecond / (1 + int.Parse(s.value) / 100f);
            PlayerControl.MP_Recover_Persecond = PlayerControl.MP_Recover_Persecond / (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.HP_Recover_Persecond:" + PlayerControl.HP_Recover_Persecond);
        }
        else if (currentEquipId == "3")
        {
            print("before: PlayerControl.Max_HP:" + PlayerControl.Max_HP);
            PlayerControl.Max_HP = PlayerControl.Max_HP / (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.Max_HP:" + PlayerControl.Max_HP);
        }
        //写入新效果
        if (s.id == "1")
        {
            print("before: PlayerControl.AttackNum:" + PlayerControl.AttackNum);
            PlayerControl.AttackNum *= (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.AttackNum:" + PlayerControl.AttackNum);
        }
        else if (s.id == "2")
        {
            print("before: PlayerControl.HP_Recover_Persecond:" + PlayerControl.HP_Recover_Persecond);
            PlayerControl.HP_Recover_Persecond *= (1 + int.Parse(s.value) / 100f);
            PlayerControl.MP_Recover_Persecond *= (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.HP_Recover_Persecond:" + PlayerControl.HP_Recover_Persecond);
        }
        else if (s.id == "3")
        {
            print("before: PlayerControl.Max_HP:" + PlayerControl.Max_HP);
            PlayerControl.Max_HP *= (1 + int.Parse(s.value) / 100f);
            print("after: PlayerControl.Max_HP:" + PlayerControl.Max_HP);
        }
        currentEquipId = s.id;
        //2.在该按钮写入选择的技能的describe 和 level
        SetDescribe(s.name + "\n" + s.describe);
        //SetLevelText("Lv" + s.level);


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
        image.sprite = Instantiate(Resources.Load<Sprite>("Pic/equip/" + s.img));
    }
}
