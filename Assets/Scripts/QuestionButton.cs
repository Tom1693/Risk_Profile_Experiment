using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionButton : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] UIController gateCount;
    [SerializeField] RobotController robot;
    [SerializeField] DataLogger data;

    int currentGate = 0;
    int currentGateRotation = 0;
    int gateCounter = 0;
    public string currentButtonText;
    public bool isPushed = false;


    string[,] gateResponses =
{
         {"I value the uncertain option at 0 and the certain option at 1,920. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
            "I value the uncertain option at 2,000 and the certain option at 1,920. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                "I value the uncertain option at 1,953 and the certain option at 1,920. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                    "I value the uncertain option at 667 and the certain option at 775. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward." },
        {"I value the uncertain option at -2,400 and the certain option at -2,304. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
            "I value the uncertain option at 0 and the certain option at -2,304. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                "I value the uncertain option at -2,344 and the certain option at -2,304. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
                    "I value the uncertain option at -1,896 and the certain option at -2,047. Therefore I chose to take the uncertain option over the certain loss, I believe the risk is worth the reward." },
        {"I value the uncertain option at 0 and the certain option at 2,400. Therefore I chose certainty over the uncertain option, I do not believe the risk is worth the reward.",
            "I value the uncertain option at 3,200 and the certain option at 2,400. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                "I value the uncertain option at 2,560 and the certain option at 2,400. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                    "I value the uncertain option at 737 and the certain option at 943. Therefore I chose the certain option over uncertain option, I do not believe the risk is worth the reward." },
        {"I value the uncertain option at -2,560 and the certain option at -1,920. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
            "I value the uncertain option at 0 and the certain option at -1,920. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                "I value the uncertain option at -2,048 and the certain option at -1,920. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
                    "I value the uncertain option at -1,502 and the certain option at -1,743. Therefore I chose to take the uncertain option over the certain loss, I believe the risk is worth the reward." },
        {"I value the less certain option at 0.1 and the more certain option at 0.2. Therefore I chose the more certain option.",
            "I value the less certain option at 2,500 and the more certain option at 5,000. Therefore I chose the less certain option over the more certain option.",
                "I value both options at 500. Therefore I do not believe it matters which option I choose.",
                    "I value the less certain option at 26 and the more certain option at 21. Therefore I chose to take the less certain option." },
        {"I value the less certain option at -5,500 and the more certain option at -2,750. Therefore I chose the more certain option.",
            "I value the less certain option at 99.8 and the more certain option at 99.9. Therefore I chose the more certain option over the less certain option.",
                "I value both options at -550. Therefore I do not believe it matters which option I choose.",
                    "I value the less certain option at -37 and the more certain option at -32. Therefore I chose to take the more certain option." },
        {"I value the uncertain option at 0 and the certain option at 540. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
            "I value the uncertain option at 1,200 and the certain option at 540. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                "I value the uncertain option at 600 and the certain option at 540. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                    "I value the uncertain option at 215 and the certain option at 253. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward." },
        {"I value the uncertain option at -1,100 and the certain option at -495. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
            "I value the uncertain option at 0 and the certain option at -495. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                "I value the uncertain option at -550 and the certain option at -495. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
                    "I value the uncertain option at -484 and the certain option at -528. Therefore I chose to take the uncertain option over the certain loss, I believe the risk is worth the reward." },
        {"I value the uncertain option at 0 and the certain option at 1,500. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
            "I value the uncertain option at 2,000 and the certain option at 1,500. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                "I value the uncertain option at 1,600 and the certain option at 1,500. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                    "I value the uncertain option at 487 and the certain option at 623. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward." },
        {"I value the uncertain option at -2,200 and the certain option at -1,650. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
            "I value the uncertain option at 0 and the certain option at -1,650. Therefore I chose the uncertain option over the certain option, I believe the risk is worth the reward.",
                "I value the uncertain option at -1,760 and the certain option at -1,650. Therefore I chose the certain option over the uncertain option, I do not believe the risk is worth the reward.",
                    "I value the uncertain option at -1,314 and the certain option at -1,526. Therefore I chose to take the uncertain option over the certain loss, I believe the risk is worth the reward." }
    };

    public void ProcessButtonPush()
    {
        if (!gateCount.isTutorial)
        {
            data.SendMessage("WriteQuestioned");

            gateCounter = gateCount.gateCounter;
            currentGate = loadedGates.GateOrder[gateCounter];
            SetText(gateResponses[currentGate, robot.RobotProfile]);
            isPushed = true;
        }
        else
        {
            isPushed = true;
            SetText("Questioning the robot gives a brief overview of why it made it's choice. Once you have questioned the robot you may question it further, at any point you can accept or alter the robot's current decision by pressing the button's to the right");
        }

    }


    private void RefreshButton()
    {
        gateCounter = gateCount.gateCounter;
        currentGate = loadedGates.GateOrder[gateCounter];

        if (currentButtonText != currentGate.ToString())
        {
            SetText("Question the Robot");
            isPushed = false;
        }
    }

    public void SetText(string text)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = text;

        currentButtonText = txt.text;
    }
}
