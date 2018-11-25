using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    public GameObject AtPanel;
    public GameObject describePanel;
    public GameObject player;
    public Text levelText;
    public Text describeText;
    public Image Image;
    public GameObject Range_energy;
    //public List<string> AtIdList;

    private bool isAtShowed;
    private void Start()
    {
        AtPanel = GameObject.FindGameObjectWithTag("ck").transform.Find("CheckPanel").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        Range_energy = player.transform.Find("range_energy").gameObject;
        gameObject.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            OnClick();
        });
    }

    public void SetContent(string level, string des)
    {
        levelText.text = "Lv" + level;
        describeText.text = des;
    }

    public void SetImage(string img)
    {
        Image.sprite = Instantiate(Resources.Load<Sprite>("Pic/at/" + img));
    }
    private void OnMouseEnter()
    {
        describePanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        describePanel.SetActive(false);
    }


    public void OnClick()
    {
        print("OnClick");
        if (isAtShowed == true)
        {
            int childCount = AtPanel.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(AtPanel.transform.GetChild(i).gameObject);
            }
            AtPanel.SetActive(false);

            isAtShowed = false;
            //Time.timeScale = 1;
        }
        else
        {
            //Time.timeScale = 0;
            //得到玩家当前所有技能  显示技能列表  点击替换
            List<ArtifactData> atList = player.GetComponent<PlayerControl>().GetPlayerArtifactList();
            AtPanel.SetActive(true);
            GameObject btnObj = Resources.Load("Prefabs/BtnShowAt") as GameObject;
            foreach (var s in atList)
            {

                Button btn = Instantiate(btnObj).GetComponent<Button>();
                btn.transform.parent = AtPanel.transform;
                //btn.transform.Find("LevelText").GetComponent<Text>().text = "Lv" + s.level;
                //Debug.Log ("Pic/skill/" + s.img);
                Sprite imgSprite = Instantiate(Resources.Load<Sprite>("Pic/at/" + s.img));
                btn.GetComponent<Image>().sprite = imgSprite;
                btn.onClick.AddListener(delegate ()
                {
                    ChangeAt(s);
                });
            }
            isAtShowed = true;
        }
        //gameObject.GetComponent<SpriteRenderer> ();


    }
    public void ChangeAt(ArtifactData s)
    {
        print("ChangeAt");
        //1.找到之前与该按钮绑定的技能，取消效果  
        //2.在该按钮写入选择的技能的describe 和 level
        SetContent(s.level, s.describe);
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
        if (s.id == "0")
        {
            //范围
            Range_energy.transform.localScale *= 1f + int.Parse(s.level) * 0.1f;
            SetContent(s.level, s.name + "\n" + s.describe + (int.Parse(s.level) * 0.1f + 1f) * 100 + "%（Lv" + s.level + "）");

        }
        else if (s.id == "1")
        {
            //弹道
            player.GetComponent<Skill_jianzaihuopao>().coldTime = 0.2f - 0.015f * int.Parse(s.level);
            SetContent(s.level, s.name + "\n" + s.describe + (int.Parse(s.level) * 0.3f + 1f) * 100 + "%（Lv" + s.level + "）");

        }
        else
        {
            SetContent(s.level, s.name + "\n" + s.describe);
        }

        //gameObject.SendMessage("SetImg", imageFilled, SendMessageOptions.RequireReceiver);
        //gameObject.SendMessage("SetKeyCode", skillKey, SendMessageOptions.RequireReceiver);
        //Debug.Log ("78" + gameObject);

        //4.更换当前按钮技能icon
        Image.sprite = Instantiate(Resources.Load<Sprite>("Pic/at/" + s.img));

        int childCount = AtPanel.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(AtPanel.transform.GetChild(i).gameObject);
        }
        print(" panel.SetActive(false);");
        AtPanel.SetActive(false);

        isAtShowed = false;
        //Time.timeScale = 1;
    }
}