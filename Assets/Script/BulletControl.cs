using System.Collections;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    // private AudioSource audio;

    //子弹速度
    public float speed = 10f;
    public float liveTime = 1.0f;

    //爆炸特效
    //public GameObject effect;
    public Sprite boom;
    // Use this for initialization
    void Start () {
        //audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        //炮弹移动
        transform.Translate (Vector3.up * speed * Time.deltaTime);

        //销毁炮弹
        liveTime -= Time.deltaTime;
        if (liveTime <= 0) {
            Destroy (gameObject);
        }
    }

    // void OnCollisionEnter2D(Collision2D obj) {

    // 	//实例化粒子特效

    // 	//SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
    // 	//spr.sprite = boom;
    // 	gameObject.GetComponent<SpriteRenderer> ().sprite = boom; 
    //     Destroy(obj.gameObject);
    //     Destroy(gameObject,2.0f);
    //     //Destroy(neweffect, 1.0f);
    // }
}