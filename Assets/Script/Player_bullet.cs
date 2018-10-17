using System.Collections;
using UnityEngine;

public class Player_bullet : MonoBehaviour {

    // private AudioSource audio;

    //子弹速度
    public float speed = 10f;
    public float liveTime = 1.0f;

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
}