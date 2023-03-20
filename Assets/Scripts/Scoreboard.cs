using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Dialogs;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public int score = 0;
    public GameObject textboard;

    private void Start()
    {
        UpdateText();
    }

    public void hitup()
    {
        score++;
        UpdateText();
    }

    public void UpdateText()
    {
        textboard.GetComponent<TMP_Text>().text = score.ToString();
    }
}