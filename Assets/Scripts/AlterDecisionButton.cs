using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlterDecisionButton : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] UIController gateCount;
    [SerializeField] GameObject robot;
    [SerializeField] ScoreKeeper scoreKeeper;

    //bool isPushed = false;
    int currentGate = 0;
    int currentGateRotation = 0;
    int gateCounter = 0;
    public string currentButtonText;

    string[] gateResponses =
{
        "0Alt",
        "1Alt",
        "2Alt",
        "3Alt",
        "4Alt",
        "5Alt",
        "6Alt",
        "7Alt",
        "8Alt",
        "9Alt"
    };

    public void ProcessButtonPush()
    {
        robot.SendMessage("ReceivedMessageAlter");
        scoreKeeper.SendMessage("ReceivedMessageAlter");
        gateCounter = gateCount.gateCounter;
        currentGate = loadedGates.GateOrder[gateCounter]; 
        SetText(gateResponses[currentGate]);
    }


    private void RefreshButton()
    {
        gateCounter = gateCount.gateCounter;
        currentGate = loadedGates.GateOrder[gateCounter];

        if (currentButtonText != currentGate.ToString())
        {
            SetText("Question the Robot Further");
        }
    }

    void GiveAnswer()
    {
        //TODO:: Change the answer based on the robot profile
    }

    public void SetText(string text)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = text;

        currentButtonText = txt.text;
    }
}
