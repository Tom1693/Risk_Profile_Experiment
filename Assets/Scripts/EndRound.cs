using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRound : MonoBehaviour
{
    [SerializeField] UIController UI;
    [SerializeField] RobotController robot;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject scoreText;
    [SerializeField] DataLogger data;

    bool isScoreWritten = false;


    void Update()
    {
        if(UI.gateCounter > 9)
        {
            endScreen.SetActive(true);
            scoreText.SetActive(true);
            SendScore();
        }
        else
        {
            endScreen.SetActive(false);
        }
    }


    void SendScore()
    {
        if (!isScoreWritten)
        {
            data.SendMessage("WriteScore");
            isScoreWritten = true;
        }
    }
}
