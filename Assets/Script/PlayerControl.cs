using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{

    //玩家移动速度
    public float speed = 5f;
    public static float Max_HP = 500;
    public static float Current_HP = 500;
    public static float HP_Recover_Persecond = 50;
    public static float Max_MP = 200;
    public static float Current_MP = 200;
    public static float MP_Recover_Persecond = 20;
    public static float Def = 0;

    public static float AttackNum = 50;
    public static float variable_Attack = 1;
    public static float variable_Bullet = 1;
    public static float variable_Range = 1;
    public static float variable_Auto = 1;
    public static float variable_Hand = 1;
    public static float variable_Single = 1;
    public static float variable_Continue = 1;

    public static bool skillFinished = true;

    public Text GameOverText;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public GameObject EnemyPool;
    public Scrollbar progressBarHP;
    public Scrollbar progressBarMP;
    public GameObject Range_energy;
    //子弹发射点
    public GameObject shotPointMiddle;
    public GameObject shotPointLeft;
    public GameObject shotPointRight;
    //子弹prefab
    public GameObject bullet;
    //玩家发射子弹CD
    private float shotCD = 0;
    public float CD = 0.3f;

    //声音资源
    public AudioSource audio;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //----------------玩家移动----------------
        //向上移动
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y <= 4f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        //向下移动
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= -4f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        //向左移动
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -2.2f)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        //向右移动
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 2.2f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        //发射炮弹
        if (Input.GetKey(KeyCode.Q))
        {
            if (Current_MP >= 10)
            {
                btn1.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f);
                shotCD -= Time.deltaTime;
                shot();
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            btn1.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        }


        if (Input.GetKey(KeyCode.W))
        {
            if (Current_MP >= 0.3 * Max_MP)
            {
                Range_energy.SetActive(true);
                btn2.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f);

            }
            if (Current_MP <= 0.1 * Max_MP)
            {
                Range_energy.SetActive(false);
            }
            else
            {
                Current_MP = Mathf.Max(Current_MP - 1, 0);
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Range_energy.SetActive(false);
            btn2.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (skillFinished == true)
            {
                if(Current_MP>=40){
                    Current_MP = Current_MP - 40;

                    blink();
                }
                
            }

        }
        // if (Input.GetKey(KeyCode.Q))
        // {
        //     Debug.Log("qqqqqqqqqq");

        // }

        if (Current_HP < Max_HP)
        {
            Current_HP = Current_HP + HP_Recover_Persecond / 30;
            progressBarHP.size = Current_HP / Max_HP;
        }

        if (Current_MP < Max_MP)
        {
            Current_MP = Current_MP + MP_Recover_Persecond / 30;
            progressBarMP.size = Current_MP / Max_MP;
        }

    }


    void shot()
    {
        //CD结束发送炮弹
        if (shotCD <= 0)
        {
            Instantiate(bullet, shotPointMiddle.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Instantiate(bullet, shotPointLeft.transform.position, Quaternion.Euler(new Vector3(0, 25, 25)));
            Instantiate(bullet, shotPointRight.transform.position, Quaternion.Euler(new Vector3(0, -25, -25)));
            Current_MP = Current_MP - 5;
            //Instantiate(bullet, shotPoint.transform.position, new Vector3(0, 0, 0));
            //audio.Play();
            //重置CD
            shotCD = CD;
        }
    }
    void blink()
    {
        Sequence s1 = DOTween.Sequence();
        btn3.interactable = false;
        s1.Append(btn3.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f)).Append(btn3.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        //btn3.transform.DOScale(new Vector3(2, 2, 2), 0.1f);
        //btn3.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        float temp = HP_Recover_Persecond;
        HP_Recover_Persecond = 50000;
        skillFinished = false;
        int i = 0;
        List<Vector3> positionList = new List<Vector3>();
        // List<T> mList = new List<T>();   
        foreach (Transform child in EnemyPool.transform)
        {
            if (i >= 4 || !child)
            {
                break;
            }
            positionList.Add(child.position);
            // Debug.Log(child.position + "I:" + i);
            // Debug.Log(positionList[i] + "I:" + i);
            i++;
            
        }
        Sequence s = DOTween.Sequence();
        foreach (Vector3 v in positionList)
        {
            s.Append(transform.DOMove(v, 0.12f));
        }
        s.OnComplete(skillStatusChange);
    }
    void skillStatusChange()
    {
        btn3.interactable = true;
        HP_Recover_Persecond = 50;
        skillFinished = true;
        Current_HP = 0.1f * Max_HP;
    }

    void OnTriggerEnter(Collider obj)
    {
        //Debug.Log(obj.gameObject.GetComponent<EnemyControl>().HP);
        float enemyHP = obj.gameObject.GetComponent<EnemyControl>().HP;
        if (Current_HP > enemyHP)
        {
            Current_HP = Current_HP - enemyHP;
            progressBarHP.size = Current_HP / Max_HP;
            Destroy(obj.gameObject);
        }
        else
        {
            //玩家挂了 游戏结束
            Debug.Log("1111111111111111");
            //GameOverText.SetActive(true);
            progressBarHP.size = 0;
            GameOverText.text = "GAME OVER";
            Destroy(gameObject);
        }
    }

}
