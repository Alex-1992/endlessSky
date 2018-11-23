using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour {
    //public SkillBase _instance;

	public Image imageFilled; //填充背景，及灰色读条
	public KeyCode skillKey; //对应技能按键
	public GameObject currentBtn; //当前绑定的按钮

	private float coldTime; //技能的冷却时间    
	private float timer; //当前冷却时间    
	private bool isCold; //是否进入冷却  
	private float mpCost;
	public static int SkillLevel = 1;
	public Text LevelText;
    //以上变量需要继承;
    //private void Awake()
    //{
    //    _instance = this;
    //}
    public void Reset(){
		Debug.LogError("Reset" + gameObject);
	}

	void Start () {
		imageFilled.fillAmount = 0;
	}

	// Update is called once per frame
	void Update () {
		SkillKeyDown ();
		if (isCold == true) {
			timer += Time.deltaTime;
			//Debug.Log(timer);
			if (timer > coldTime) {
				//冷却完毕，回归默认值               
				isCold = false;
				timer = 0;
				imageFilled.fillAmount = 0;
			} else {
				imageFilled.fillAmount = (coldTime - timer) / coldTime; //冷却比例         
			}
		}
	}

	private void SkillKeyDown () {
		//imageFilled.fillAmount = 0;
		if (Input.GetKey (skillKey) && isCold == false) {
			PlaySkill ();
			PlayerControl.Current_MP = PlayerControl.Current_MP - mpCost;
			isCold = true;
			//Debug.Log(isCold);
		}
	}

	//技能释放
	void PlaySkill () {

	}

	public void SetImg (Image img) {
		Debug.Log ("SkillBase:SetImg");
		imageFilled = img;
	}

	public void SetKeyCode (KeyCode k) {
		Debug.Log ("SkillBase:SetKeyCode");
		skillKey = k;
	}

	public void SetCurrentBtn (GameObject obj) {
		Debug.Log ("SkillBase:SetCurrentBtn");
		Debug.Log ("SetCurrentBtn" + obj);
		currentBtn = obj;
	}

	public static void SetSkillLevel (int num) {
		//Debug.Log ("SkillBase:SetSkillLevel     " + "currentBtn:" + currentBtn);
		SkillLevel = num;
		//if(gameObject != null){
		//	gameObject.transform.Find ("LevelText").GetComponent<Text> ().text = "Lv" + num;
		//}
       
	}

     

	// public static float getSkillDamage () {
	// 	return 0;
	// 	// Debug.Log( PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float) (SkillDamagePercent + SkillDamagePercent * (SkillLevel - 1) * 0.2));
	// 	// return PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float) (SkillDamagePercent + SkillDamagePercent * (SkillLevel - 1) * 0.2);
	// }
}