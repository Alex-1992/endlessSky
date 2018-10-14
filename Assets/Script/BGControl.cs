using System.Collections;
using UnityEngine; 
public class BGControl : MonoBehaviour {
 
    //背景滚动速度
    public float speed = 1.5f;
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y <= 1.7) {
            //transform.position += new Vector3(0, 10, 0);
            transform.position += new Vector3(0, 8.4f, 0);
        }
	}
}

