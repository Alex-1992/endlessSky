using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy0 : MonoBehaviour {
    public GameObject player;
	//敌人速度
	//public Image handle;
	public float speed = 1f;
	public float HP = 150;
	public float MaxHP = 150;
	public float AtkNum = 30;
	private float shotCD = 0;
	public float CD = 1f;
	public bool CanMove = true;

	public GameObject shotPoint;
	public GameObject enemyBullet;
	public Scrollbar progressBar;
	public ParticleSystem boom;

    // Use this for initialization
    public void SetHP(float factor)
    {
        MaxHP = MaxHP * factor;
        HP = HP * factor;
        //AtkNum = AtkNum * factor;
    }
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	void Update () {
		shotCD -= Time.deltaTime;
		shot ();
		//敌人移动
		// if (GameControl.enemyCanMove)
		// {
		//Debug.Log(Vector3.Distance(transform.position,PlayerControl.playerPosition));
		if (Vector3.Distance (transform.position, PlayerControl.playerPosition) > 6) {
			transform.Translate (Vector3.down * speed * Time.deltaTime);
		}
		// }

		// if (transform.position.y <= -4)
		// {
		//     Destroy(gameObject);
		// }
		if (progressBar.size < 1) {
			progressBar.gameObject.SetActive (true);
			//handle.SetActive(true);
		}

		//Vector3 v = new Vector3(PlayerControl.playerPosition.x, PlayerControl.playerPosition.position.y, PlayerControl.playerPosition.position.z);
		//Debug.DrawLine (PlayerControl.playerPosition, transform.position, Color.yellow);

		// Quaternion rotation = Quaternion.LookRotation(new Vector3(0,0,1), PlayerControl.playerPosition);  //获取目标方向

		// transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);

		//gameObject.transform.LookAt(PlayerControl.playerPosition);
		// gameObject.transform.Rotate(new Vector3(90, 0, 0));
		// gameObject.transform.Rotate(new Vector3(0,0, 180 ));

		//lock at target Player
		// transform.rotation = Quaternion.Slerp(
		//      transform.rotation,
		//      Quaternion.LookRotation(PlayerControl.playerPosition - transform.position),
		//      speed * Time.deltaTime
		// );

		// Quaternion TargetRotation = Quaternion.LookRotation(m_Target.transform.position - transform.position, Vector3.up);
		// transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 2.5f);

		if (PlayerControl.IsBlinkFinished == true) {
			gameObject.transform.Rotate (new Vector3 (180, 0, 0));
			Vector3 targetPosition = PlayerControl.playerPosition;
			targetPosition.z = transform.position.z;
			transform.LookAt (targetPosition);
			transform.rotation *= Quaternion.Euler (new Vector3 (0, 90, -90));
		}

	}
	void shot () {
		//CD结束发送炮弹
		if (shotCD <= 0) {
			//Debug.Log (transform.rotation.z);
			Instantiate (enemyBullet, shotPoint.transform.position, Quaternion.Euler (new Vector3 (0, 0, transform.position.x > PlayerControl.playerPosition.x ? transform.eulerAngles.z : -transform.eulerAngles.z)))
				.GetComponent<Enemy_bullet> ().Damage = AtkNum;
			shotCD = CD + Random.Range (-0.2f, 0.2f);;
		}
	}
	//void OnCollisionEnter2D(Collision2D obj)
	void OnTriggerEnter (Collider obj)

	{
		float damage = 0;
		//Debug.Log(obj.gameObject.name);
		if (obj.gameObject.name == "Player_bullet(Clone)") {
            //计算击中伤害 攻击-弹道-自施放-单次伤害
            damage = player.GetComponent<Skill_jianzaihuopao>().GetSkillDamage();
            HP = HP - damage;
			//销毁子弹
			Destroy (obj.gameObject);
		} else if (obj.gameObject.name == "player") {
			if (PlayerControl.IsBlinkFinished == false) {
				HP = HP - player.GetComponent<Skill_shanxiandaji>().GetSkillDamage();
            } else if (HP < PlayerControl.Current_HP) {
				//表示player与敌人撞击 并且敌人死亡
				PlayerControl.SufferDamage(HP);
				//PlayerControl.Current_HP -= HP;
				HP = 0;
			} else {
				//玩家死亡
				PlayerControl.Current_HP = -10000;
			}
			//damage = PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Auto * PlayerControl.variable_Single;
			//Debug.Log("enter range_energy");
		}

		if (HP <= 0) {
			//Destroy(obj.gameObject);
			//死亡
			Destroy (gameObject);
		} else {
			//血条扣血

			progressBar.size = HP / MaxHP;
		}

		//实例化粒子特效
		//SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
		//spr.sprite = boom;
		//gameObject.GetComponent<SpriteRenderer>().sprite = boom;

		//Destroy(neweffect, 1.0f);
	}
	void OnTriggerStay (Collider obj) {
		//Debug.Log("stay range_energy");
		if (obj.gameObject.name == "range_energy") {
			HP = HP - player.GetComponent<Skill_nengliangchang>().GetSkillDamage();
            if (HP <= 0) {
				//Destroy(obj.gameObject);
				//敌机死亡
				Destroy (gameObject);
			} else {
				//血条扣血
				progressBar.size = HP / MaxHP;
			}
		}

	}

	private void OnDestroy () {
		//boom.Play();
		Instantiate (boom, gameObject.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
		GameObject.Find ("Main Camera").GetComponent<GameControl> ().CountIfDropItem(transform.position);
		//Debug.Log("aaaaaa");
	}
}