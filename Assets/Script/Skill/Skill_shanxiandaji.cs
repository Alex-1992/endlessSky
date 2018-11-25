using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Skill_shanxiandaji : SkillBase {

	// public Image imageFilled; //填充背景，及灰色读条
	// public KeyCode skillKey; //对应技能按键

	//private float coldTime = 2f; //技能的冷却时间    
	//private float timer = 0; //当前冷却时间    
	//private bool  isCold = false; //是否进入冷却    
	//private float mpCost = 40;
 //   private float SkillDamagePercent = 2;
    //public static int SkillLevel = 1; 
    //public GameObject player;

    private Sprite CircleSprite;
	private Sprite OriginSprite;
    

    GameObject player;
	GameObject enemyPool;

	// Use this for initialization
	void Start () {
        coldTime = 4f; //技能的冷却时间  
        timer = 0; //当前冷却时间    
        isCold = false; //是否进入冷却   
        mpCost = 50;
        SkillDamagePercent = 2;
        //imageFilled.fillAmount = 0;
        OriginSprite = Instantiate (Resources.Load<Sprite> ("Pic/craft"));
		CircleSprite = Instantiate (Resources.Load<Sprite> ("Pic/circle"));
		player = GameObject.Find ("player");
		enemyPool = GameObject.Find ("EnemyPool");
		//bullet = Resources.Load<GameObject>("Prefabs/Player_bullet")) as GameObject;

		//Debug.Log (bullet);
	}

	// Update is called once per frame
	void Update () {
		// 	if (Input.GetKey (KeyCode.E)) {
		// 		if (skillFinished == true) {
		// 			if (Current_MP >= 40) {
		// 				Current_MP = Current_MP - 40;

		// 				blink ();
		// 			}

		// 		}

		// 	}

		//hotCD -= Time.deltaTime;
		// if (Input.GetKey (KeyCode.Q)) {
		// 	if (PlayerControl.Current_MP >= 10 && shotCD <= 0) {
		// 		//btn1.transform.DOScale (new Vector3 (1.5f, 1.5f, 1.5f), 0.1f);

		// 		shot ();
		// 	}

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
                if (imageFilled != null)
                {
                    imageFilled.fillAmount = (coldTime - timer) / coldTime; //冷却比例
                }
			}
		}
	}

	private void SkillKeyDown () {
        if (imageFilled != null)
        {
            imageFilled.fillAmount = 0;
        }
		
		if (Input.GetKey (skillKey) && isCold == false && PlayerControl.Current_MP >= 40) {
			blink ();
			PlayerControl.Current_MP = PlayerControl.Current_MP - mpCost;
			isCold = true;
			//Debug.Log(isCold);
		}
	}

	void blink () {
		PlayerControl.IsBlinkFinished = false;
		PlayerControl.WUDI = true;
		((SpriteRenderer) player.GetComponent<Renderer> ()).sprite = CircleSprite;

		//Sequence s1 = DOTween.Sequence ();
		Vector3 originPos = player.transform.position;
		//s1.Append (btn3.transform.DOScale (new Vector3 (1.5f, 1.5f, 1.5f), 0.1f)).Append (btn3.transform.DOScale (new Vector3 (1, 1, 1), 0.1f));
		//btn3.transform.DOScale(new Vector3(2, 2, 2), 0.1f);
		//btn3.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
		float temp = PlayerControl.HP_Recover_Persecond;
		//PlayerControl.HP_Recover_Persecond = 50000;
		//int i = 0;
		//List<Vector3> positionList = new List<Vector3> ();
		// List<T> mList = new List<T>();  
		GameControl.enemyCanMove = false;
		Sequence s = DOTween.Sequence ();
		foreach (Transform child in enemyPool.transform) {
			if (!child) {
				break;
			}
			//child.gameObject.GetComponent<EnemyControl> ().CanMove = false;
			//positionList.Add (child.position);
			s.Append (player.transform.DOMove (child.position, 0.1f));
			// Debug.Log(child.position + "I:" + i);
			// Debug.Log(positionList[i] + "I:" + i);
			//i++;

		}

		// foreach (Vector3 v in positionList) {
		//     s.Append (transform.DOMove (v, 0.1f));
		// }
		s.Append (player.transform.DOMove (originPos, 0.1f));
		s.OnComplete (skillFinish);
	}
	void skillFinish () {
		//btn3.transform.localScale = new Vector3 (1, 1, 1);
		GameControl.enemyCanMove = true;
		//PlayerControl.HP_Recover_Persecond = 50;
		((SpriteRenderer) player.GetComponent<Renderer> ()).sprite = OriginSprite;
		PlayerControl.IsBlinkFinished = true;
		PlayerControl.WUDI = false;
		//Current_HP = 0.1f * Max_HP;
	}

	// public void SetImg (Image img) {
	// 	imageFilled = img;
	// }

	// public void SetKeyCode (KeyCode k) {
	// 	skillKey = k;
	// }

	// public void SetSkillLevel(int num){
	// 	SkillLevel = num;
	// }

	public float GetSkillDamage () {
        Debug.Log((PlayerControl.AttackNum+ PlayerControl.Max_HP *0.1f) * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float)(SkillDamagePercent + SkillDamagePercent * (SkillLevel - 1) * 0.2));
        return PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Single * (float)(SkillDamagePercent + SkillDamagePercent * (SkillLevel -1) * 0.2);
    }
}