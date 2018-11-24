using System.Collections;
using UnityEngine;


public class Enemy_bullet : MonoBehaviour {

    // private AudioSource audio;

    //子弹速度
    public float speed = 4f;
    public float liveTime = 2.0f;
    public float Damage = 0;
    

    // Use this for initialization
    void Start () {
        //audio = gameObject.GetComponent<AudioSource>();
        //transform.position = Vector3.MoveTowards(transform.position, PlayerControl.playerPosition, 0);
    }

    // Update is called once per frame
    void Update () {
        //炮弹移动
        transform.Translate (Vector3.down * speed * Time.deltaTime);

        //销毁炮弹
        liveTime -= Time.deltaTime;
        if (liveTime <= 0) {
            Destroy (gameObject);
        }
    }
    void OnTriggerEnter(Collider obj){
        if(obj.gameObject.name == "Player_bullet(Clone)"){
            Destroy(obj.gameObject);
            Destroy(this.gameObject);
        }
        if(obj.gameObject.name == "player"){
            Destroy(this.gameObject);
            if (PlayerControl.WUDI == true) return;
            PlayerControl.Current_HP -= Damage;
            //GameControl.ShakeScreen(0.1f);
            PlayerControl.SufferDamage(Damage);
        }
        if (obj.gameObject.name == "range_energy")
        {
            PlayerControl.Current_MP -= Damage;
            Destroy(this.gameObject);
        }
    }
}