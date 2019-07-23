using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionButton : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] UIController gateCount;

    int currentGate = 0;
    int currentGateRotation = 0;
    int gateCounter = 0;
    public string currentButtonText;

    string[] gateResponses =
{
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9"
    };

    public void ProcessButtonPush()
    {
        print(currentGate);

        gateCounter = gateCount.gateCounter;
        currentGate = loadedGates.GateOrder[gateCounter];
        SetText(gateResponses[currentGate]);
    }

    private void Update()
    {
        RefreshButton();
    }

    private void RefreshButton()
    {
        gateCounter = gateCount.gateCounter;
        currentGate = loadedGates.GateOrder[gateCounter];

        if (currentButtonText != currentGate.ToString())
        {
            SetText("Question");
        }
    }

    void GiveAnswer()
    {
        //TODO:: Change the answer based on the robot profile
    }

    void SetText(string text)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = text;

        currentButtonText = txt.text;
    }
}
