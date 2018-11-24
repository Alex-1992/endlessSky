using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_nengliangchang : SkillBase {

    // public Image imageFilled; //填充背景，及灰色读条
    // public KeyCode skillKey; //对应技能按键

    //private float coldTime = 0; //技能的冷却时间    
    //private float timer = 0; //当前冷却时间    
    //private bool  isCold = false; //是否进入冷却
    //private float mpCost = 1;
    //private float SkillDamagePercent = 0.2f;
    //public static int SkillLevel = 1;

    private GameObject player;
    private GameObject Range_energy;

    // Use this for initialization
    void Start () {
        coldTime = 0; //技能的冷却时间    
        timer = 0; //当前冷却时间    
        isCold = false; //是否进入冷却
        mpCost = 1;
        SkillDamagePercent = 0.2f;
        player = GameObject.Find ("player");
        Range_energy = player.transform.Find ("range_energy").gameObject;
        // GameObject parentObj = GameObject.Find("AAA");
        // GameObject bbb = parentObj.transform.Find("BBB").gameObject;
        // bbb.SetActive(true);

        //Debug.Log ("Skill_nengliangchang" + player);
        //Debug.Log ("Skill_nengliangchang" + Range_energy);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey (skillKey)) {
            if (PlayerControl.Current_MP >= 0.3 * PlayerControl.Max_MP) {
                if (imageFilled != null)
                {
                    imageFilled.fillAmount = 1;
                }
                
                Range_energy.SetActive (true);
                //btn2.transform.DOScale (new Vector3 (1.5f, 1.5f, 1.5f), 0.1f);

            }
            if (PlayerControl.Current_MP <= 0.1 * PlayerControl.Max_MP) {
                Range_energy.SetActive (false);
            } else {
                PlayerControl.Current_MP = Mathf.Max (PlayerControl.Current_MP - mpCost, 0);
            }
        }

        if (Input.GetKeyUp (skillKey)) {
            Range_energy.SetActive (false);
            if (imageFilled != null)
            {
                imageFilled.fillAmount = 0;
            }
            //btn2.transform.DOScale (new Vector3 (1, 1, 1), 0.1f);
        }
    }

    // public void SetImg (Image img) {
    //     imageFilled = img;
    // }

    // public void SetKeyCode (KeyCode k) {
    //     skillKey = k;
    // }

    // public void SetSkillLevel (int num) {
    //     SkillLevel = num;
    // }

    public float getSkillDamage () {
        Debug.Log(PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float)(SkillDamagePercent + SkillDamagePercent * (SkillLevel - 1) * 0.2));
        return PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float) (SkillDamagePercent + SkillDamagePercent * (SkillLevel - 1) * 0.2);
    }
}