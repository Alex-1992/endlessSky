using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{

    //敌人速度
    public Image handle; 
    public float speed = 6f;
    public float HP = 300;
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
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= -4)
        {
            Destroy(gameObject);
        }
        if(progressBar.size < 1){
            progressBar.gameObject.SetActive(true);
            //handle.SetActive(true);
        }

    }

    //void OnCollisionEnter2D(Collision2D obj)
    void OnTriggerEnter(Collider obj)
    
    {
        float damage = 0;
        //Debug.Log(obj.gameObject.name);
        if(obj.gameObject.name == "bullet_common(Clone)"){
            //计算击中伤害 攻击-弹道-自施放-单次伤害
            damage = PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Auto * PlayerControl.variable_Single;
            //Debug.Log(damage);
            //销毁子弹
            Destroy(obj.gameObject);
        }else if(obj.gameObject.name == "range_energy"){
            //damage = PlayerControl.AttackNum * PlayerControl.variable_Attack * PlayerControl.variable_Bullet * PlayerControl.variable_Auto * PlayerControl.variable_Single;
            //Debug.Log("enter range_energy");
        }
        
        HP = HP - damage;
        if (HP <= 0)
        {
            //Destroy(obj.gameObject);
            //敌机死亡
            Destroy(gameObject);
        }else{
            //血条扣血
            
            progressBar.size = HP / 300;
        }
        


        //实例化粒子特效
        //SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        //spr.sprite = boom;
        //gameObject.GetComponent<SpriteRenderer>().sprite = boom;

        //Destroy(neweffect, 1.0f);
    }
    void OnTriggerStay(Collider obj){
        //Debug.Log("stay range_energy");
          HP = HP - 10;
        if (HP <= 0)
        {
            //Destroy(obj.gameObject);
            //敌机死亡
            Destroy(gameObject);
        }else{
            //血条扣血
            progressBar.size = HP / 300;
        }
        
    }


    private void OnDestroy()
    {
        //boom.Play();
        Instantiate(boom, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //Debug.Log("aaaaaa");
    }
}
