using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour {
    public GameObject describePanel;
    public Text levelText;
    public Text describeText;
    public void SetContent(string level, string des)
    {
        levelText.text = "Lv"+level;
        describeText.text = des;
    }
    private void OnMouseEnter()
    {
        describePanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        describePanel.SetActive(false);
    }
}
