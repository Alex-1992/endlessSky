using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Sprite;
//[System.Serializable]

public class BtnBondSkill : MonoBehaviour {
    public Text describeText;
    public void OnMouseEnter()
    {
        print("OnMouseEnter");
    }

    public void OnMouseExit()
    {
        print("OnMouseExit");
    }
}