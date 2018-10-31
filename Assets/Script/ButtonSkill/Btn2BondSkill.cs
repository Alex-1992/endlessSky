using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Sprite;
//[System.Serializable]

public class Btn2BondSkill : MonoBehaviour {
    public GameObject panel;
    public Image imageFilled;
    public Image imageBack;
    //public Image imageFilled;

    public KeyCode skillKey = KeyCode.W;
    private bool IsSkillShowed = false;
    public string currentSkillName = "";
    //private Sprite SkillIcon;
    private List<SkillData> SkillList = new List<SkillData> ();
    // Use this for initialization
    void Start () {
        //1.删除之前的技能脚本。  
        //2.在该按钮挂在对应技能脚本名字写入currentSkillName。 
        //3.绑定img和keycode。
        currentSkillName = "Skill_nengliangchang";
        Type t = Type.GetType (currentSkillName);
        //AddComponent<ttt> ();
        Debug.Log ("Btn2  "+gameObject);
        gameObject.AddComponent (t);
        gameObject.SendMessage ("SetImg", imageFilled, SendMessageOptions.RequireReceiver);
        gameObject.SendMessage ("SetKeyCode", skillKey, SendMessageOptions.RequireReceiver);
        gameObject.SendMessage ("SetCurrentBtn", gameObject, SendMessageOptions.RequireReceiver);

        imageFilled.sprite = Instantiate (Resources.Load<Sprite> ("Pic/skill/" + currentSkillName.Substring (6)));
        imageBack.sprite = Instantiate (Resources.Load<Sprite> ("Pic/skill/" + currentSkillName.Substring (6)));
        //gameObject.GetComponent<currentSkillName>().SetImg (imageFilled);
        //.GetComponent<脚本名>().函数名();//只能调用public类型函数
        // gameObject.GetComponent (currentSkillName).SetKeyCode (skillKey);
        //.GetComponent<EnemyControl>().HP
        gameObject.GetComponent<Button> ().onClick.AddListener (delegate () {
            OnClick ();
        });
    }

    // Update is called once per frame
    void Update () {

    }
    public void OnClick () {
        if (IsSkillShowed == true) {
            int childCount = panel.transform.childCount;
            for (int i = 0; i < childCount; i++) {
                Destroy (panel.transform.GetChild (i).gameObject);
            }
            panel.SetActive (false);

            IsSkillShowed = false;
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
            //得到玩家当前所有技能  显示技能列表  点击替换
            SkillList = GameObject.Find ("player").GetComponent<PlayerControl> ().GetSkillList ();
            panel.SetActive (true);
            GameObject btnObj = Resources.Load ("Prefabs/BtnShowSkill") as GameObject;
            foreach (var s in SkillList) {
                Button btn = Instantiate (btnObj).GetComponent<Button> ();
                btn.transform.parent = panel.transform;

                //Debug.Log ("Pic/skill/" + s.img);
                Sprite imgSprite = Instantiate (Resources.Load<Sprite> ("Pic/skill/" + s.img));
                btn.GetComponent<Image> ().sprite = imgSprite;
                btn.onClick.AddListener (delegate () {
                    ChangeSkill (s);
                });
            }
            IsSkillShowed = true;
        }
        //gameObject.GetComponent<SpriteRenderer> ();

    }
    public void ChangeSkill (SkillData s) {

        Debug.Log ("1111111");
        //1.删除之前的技能脚本。  
        Destroy (gameObject.GetComponent (currentSkillName));

        //2.在该按钮挂在对应技能脚本名字写入currentSkillName。 
        Debug.Log (s.script);
        currentSkillName = s.script;
        Type t = Type.GetType (currentSkillName);
        gameObject.AddComponent (t);

        //3.绑定img和keycode。

        gameObject.SendMessage ("SetImg", imageFilled, SendMessageOptions.RequireReceiver);
        gameObject.SendMessage ("SetKeyCode", skillKey, SendMessageOptions.RequireReceiver);
        Debug.Log ("78" + gameObject);

        //4.更换当前按钮技能icon
        imageFilled.sprite = Instantiate (Resources.Load<Sprite> ("Pic/skill/" + s.img));
        imageBack.sprite = Instantiate (Resources.Load<Sprite> ("Pic/skill/" + s.img));

        int childCount = panel.transform.childCount;
        for (int i = 0; i < childCount; i++) {
            Destroy (panel.transform.GetChild (i).gameObject);
        }
        panel.SetActive (false);

        IsSkillShowed = false;
        Time.timeScale = 1;
    }
}