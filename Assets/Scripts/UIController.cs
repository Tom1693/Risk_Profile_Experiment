using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] GameObject UI;
    [SerializeField] RobotController robot;
    [SerializeField] ScoreKeeper scoreKeeper;

    [SerializeField] Text scoreText;
    [SerializeField] Text leftText;
    [SerializeField] Text rightText;

    [SerializeField] QuestionFurtherButton questionFurtherButton;
    [SerializeField] QuestionButton questionButton;
    [SerializeField] AcceptDecisionButton acceptDecisionButton;
    [SerializeField] AlterDecisionButton alterDecisionButton;

    

    public int gateCounter = 0;
    int currentGate = 0;
    int currentGateRotation = 0;

    string[] gateQuestionsA =
    {
        "66% chance of viewing area of size 2,000m\u00B2\n33% chance of viewing area of size 1,920m\u00B2\n1% chance of viewing area of size 0m\u00B2",
        "66% chance of irradiation measuring 2,400μSv\n33% chance of irradiation measuring 2,304μSv\n1% chance of irradiation measuring 0μSv",
        "80% chance of viewing area of size 3,200m\u00B2\n20% chance of viewing area of size 0m\u00B2\n",
        "80% chance of irradiation measuring 2,560μSv\n20% chance of irradiation measuring 0μSv",
        "0.1% chance of viewing area of size 5,000m\u00B2\n99.9% chance of viewing area of size 0m\u00B2",
        "0.1% chance of irradiation measuring 5,500μSv\n99.9% chance of irradiation measuring 0μSv",
        "50% chance of viewing area of size 1,200m\u00B2\n50% chance of viewing area of size 0m\u00B2\n",
        "50% chance of irradiation measuring 1,100μSv\n50% chance of irradiation measuring 0μSv",
        "80% chance of viewing area of size 2,000m\u00B2\n20% chance of viewing area of size 0m\u00B2\n",
        "80% chance of irradiation measuring 2,200μSv\n20% chance of irradiation measuring 0μSv"
    };

    string[] gateQuestionsB =
    {
        "100% chance of viewing area of size 1,920m\u00B2",
        "100% chance of irradiation measuring 2,304μSv",
        "100% chance of viewing area of size 2,400m\u00B2",
        "100% chance of irradiation measuring 1,920μSv",
        "0.2% chance of viewing area of size 2,500m\u00B2\n99.8% chance of viewing area of size 0m\u00B2",
        "0.2% chance of irradiation measuring 2,750μSv\n99.8% chance of irradiation measuring 0μSv",
        "100% chance of viewing area of size 540m\u00B2",
        "100% chance of irradiation measuring 495μSv",
        "100% chance of viewing area of size 1500m\u00B2",
        "100% chance of irradiation measuring 1650μSv"
    };


    // Update is called once per frame
    void Update()
    {
        if (gateCounter <= 10)
        {
            DisplayRisk();
            RefreshButtons();
            ToggleUI();
            ShowRobotChoice();
            DisplayScore();
        }

        
    }

    private void DisplayScore()
    {
        scoreText.text = scoreKeeper.score.ToString();
    }

    private void ShowRobotChoice()
    {
        if (robot.curSpeed <= 0)
        {


            int decision = robot.robotGateDecisions[gateCounter, robot.RobotProfile];

            Image rightPanel = GameObject.Find("Right Text Panel").GetComponent<Image>();
            Image leftPanel = GameObject.Find("Left Text Panel").GetComponent<Image>();

            Image rightPanelRobot = GameObject.Find("Right Panel Image").GetComponent<Image>();
            Image leftPanelRobot = GameObject.Find("Left Panel Image").GetComponent<Image>();

            Color choiceColour = new Color((165f / 255f), (246f / 255f), (255f / 255f), (223f / 255f));
            Color notChoiceColour = new Color(1f, 1f, 1f, (223f / 255f));

            if (currentGateRotation == 0 && decision == 0)
            {
                leftPanel.color = choiceColour;
                leftPanelRobot.enabled = true;

                rightPanel.color = notChoiceColour;
                rightPanelRobot.enabled = false;
            }
            else if (currentGateRotation == 0 && decision == 1)
            {
                rightPanel.color = choiceColour;
                rightPanelRobot.enabled = true;

                leftPanel.color = notChoiceColour;
                leftPanelRobot.enabled = false;
            }
            else if (currentGateRotation == 1 && decision == 0)
            {
                rightPanel.color = choiceColour;
                rightPanelRobot.enabled = true;

                leftPanel.color = notChoiceColour;
                leftPanelRobot.enabled = false;
            }
            else if (currentGateRotation == 1 && decision == 1)
            {
                leftPanel.color = choiceColour;
                leftPanelRobot.enabled = true;

                rightPanel.color = notChoiceColour;
                rightPanelRobot.enabled = false;
            }
        }
    }

    private void RefreshButtons()
    {
        if (robot.curSpeed != 0)
        {
            questionButton.SetText("Question the Robot");
            questionFurtherButton.SetText("Question the Robot Further");
            acceptDecisionButton.SetText("Accept Robot Decision");
            alterDecisionButton.SetText("Alter Robot Decision");
        }
    }

    private void DisplayRisk()
    {
        if (robot.curSpeed <= 0)
        {
            currentGate = loadedGates.GateOrder[gateCounter];
            currentGateRotation = loadedGates.Rotations[gateCounter];

            //accounts for whether the gate is rotated or not
            if (currentGateRotation == 0)
            {
                leftText.text = gateQuestionsA[currentGate];
                rightText.text = gateQuestionsB[currentGate];
            }
            else if (currentGateRotation == 1)
            {
                leftText.text = gateQuestionsB[currentGate];
                rightText.text = gateQuestionsA[currentGate];
            }
        }
    }

    private void ToggleUI()
    {
        if (robot.curSpeed > 0)
        {
            UI.SetActive(false);
        }
        else
        {
            UI.SetActive(true);
        }
    }
}
