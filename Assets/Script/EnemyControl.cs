using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{

    //敌人速度
    public Image handle;
    public float speed = 2f;
    public float HP = 300;
    public bool CanMove = true;
    public Scrollbar progressBar;
    public ParticleSystem boom;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //敌人移动
        // if (GameControl.enemyCanMove)
        // {
        //     transform.Translate(Vector3.down * speed * Time.deltaTime);
        // }

        if (transform.position.y <= -4)
        {
            Destroy(gameObject);
        }
        if (progressBar.size < 1)
        {
            progressBar.gameObject.SetActive(true);
            //handle.SetActive(true);
        }


        //Vector3 v = new Vector3(PlayerControl.playerPosition.x, PlayerControl.playerPosition.position.y, PlayerControl.playerPosition.position.z);
        // Debug.DrawLine (PlayerControl.playerPosition, transform.position, Color.yellow);
        // Quaternion rotation = Quaternion.LookRotation(PlayerControl.playerPosition - transform.position, Vector3.forward);  //获取目标方向

        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);

        gameObject.transform.LookAt(PlayerControl.playerPosition);
        gameObject.transform.Rotate(new Vector3(90, 0, 0));
        gameObject.transform.Rotate(new Vector3(0, -180, -180 - transform.rotation.z));


        //lock at target Player
        // transform.rotation = Quaternion.Slerp(
        //      transform.rotation,
        //      Quaternion.LookRotation(PlayerControl.playerPosition - transform.position),
        //      speed * Time.deltaTime
        // );



        // Quaternion TargetRotation = Quaternion.LookRotation(m_Target.transform.position - transform.position, Vector3.up);
        // transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 2.5f);

    }

    //void OnCollisionEnter2D(Collision2D obj)
    void OnTriggerEnter(Collider obj)

    {
        float damage = 0;
        //Debug.Log(obj.gameObject.name);
        if (obj.gameObject.name == "bullet_common(Clone)")
        {
            //计算击中伤害 攻击-弹道-自施放-单次伤害
            damage = PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Auto * PlayerControl.variable_Single;
            HP = HP - damage;
            //Debug.Log(damage);
            //销毁子弹
            Destroy(obj.gameObject);
        }
        else if (obj.gameObject.name == "player")
        {
            if (PlayerControl.skillFinished == false)
            {
                HP = HP - PlayerControl.AttackNum * 3;
            }
            else if (HP < PlayerControl.Current_HP)
            {
                //表示player与敌人撞击 并且敌人死亡
                PlayerControl.Current_HP -= HP;
                HP = 0;
            }
            else
            {
                //玩家死亡
                PlayerControl.Current_HP = 0;
                obj.gameObject.GetComponent<PlayerControl>().GameOver();
            }
            //damage = PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Auto * PlayerControl.variable_Single;
            //Debug.Log("enter range_energy");
        }


        if (HP <= 0)
        {
            //Destroy(obj.gameObject);
            //死亡
            Destroy(gameObject);
        }
        else
        {
            //血条扣血

            progressBar.size = HP / 300;
        }

        //实例化粒子特效
        //SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        //spr.sprite = boom;
        //gameObject.GetComponent<SpriteRenderer>().sprite = boom;

        //Destroy(neweffect, 1.0f);
    }
    void OnTriggerStay(Collider obj)
    {
        //Debug.Log("stay range_energy");
        if (obj.gameObject.name == "range_energy")
        {
            HP = HP - 10;
            if (HP <= 0)
            {
                //Destroy(obj.gameObject);
                //敌机死亡
                Destroy(gameObject);
            }
            else
            {
                //血条扣血
                progressBar.size = HP / 300;
            }
        }

    }

    private void OnDestroy()
    {
        //boom.Play();
        Instantiate(boom, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //Debug.Log("aaaaaa");
    }
}