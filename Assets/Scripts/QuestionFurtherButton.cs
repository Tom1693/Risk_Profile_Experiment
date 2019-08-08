using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionFurtherButton : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] UIController gateCount;
    [SerializeField] RobotController robot;
    [SerializeField] QuestionButton questionButton;
    [SerializeField] DataLogger data;

    int currentGate = 0;
    int currentGateRotation = 0;
    int gateCounter = 0;
    public string currentButtonText;

    string[,] gateResponses =
     { 
        {"When examining this choice I look at the worst case outcome. In this case the risk has the chance of gaining me nothing, whereas the certain option allows me to gain 1,920. Therefore I chose certainty.",
            "When examining this choice I look at the best case outcome. In this case the risk has the chance of gaining me 2,000. Therefore I believe that the chance to gain 2,000 outweighs the certainty in gaining 1,920.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I chose. ",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values certainty is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. In this case the risk has the chance of losing me 2,400, whereas the certain option only loses me 2,304. Therefore I chose certainty.",
            "When examining this choice I look at the best case outcome. In this case the risk has the chance of losing me nothing. Therefore I believe that the chance to lose nothing outweighs the certainty of losing 2,304.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the values given above. When comparing these values the certain option is worth more and is therefore the option I chose.",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. In this case the risk has the chance of gaining me nothing, whereas the certain option allows me to gain 2,400. Therefore I chose certainty.",
            "When examining this choice I look at the best case outcome. In this case the risk has the chance of gaining me 3,200. Therefore I believe that the chance to gain 3,200 outweighs the certainty in gaining 2,400.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I chose.",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values certainty is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. In this case the risk has the chance of losing me 2,560, whereas the certain option only loses me 1,920. Therefore I chose certainty.",
            "When examining this choice I look at the best case outcome. In this case the risk has the chance of losing me nothing. Therefore I believe that the chance to lose nothing outweighs the certainty of losing 1,920.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the values given above. When comparing these values the certain option is worth more and is therefore the option I chose. ",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. As both options have the same worst case of 0, I look at which is more likely to give me a gain. Therefore I choose the option with the higher probability to provide gain.",
            "When examining this choice I look at the best case outcome. In this case the best case outcome is 5,000. Therefore this is the option I chose.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the value given above. As both options have the same average, I decided between them randomly. ",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. In this case the lower probability option has the chance of losing me 5,500, whereas the higher probability option only loses me 2,750. Therefore I chose the higher probability option.",
            "When examining this choice I look at the best case outcome. As both options have the same best case, I look at which is less likely to give me a loss. Therefore I chose the 0.1% chance to gain to lose 5,500.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the value given above. As both options have the same average, I decided between them randomly. ",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. In this case the risk has the chance of gaining me nothing, whereas the certain option allows me to gain 540. Therefore I chose certainty.",
            "When examining this choice I look at the best case outcome. In this case the risk has the chance of gaining me 1,200. Therefore I believe that the chance to gain 1,200 outweighs the certainty in gaining 540.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I chose.",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. In this case the risk has the chance of losing me 1,100, whereas the certain option only loses me 495. Therefore I chose certainty.",
            "When examining this choice I look at the best case outcome. In this case the risk has the chance of losing me nothing. Therefore I believe that the chance to lose nothing outweighs the certainty of losing 495.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the values given above. When comparing these values the certain option is worth more and is therefore the option I chose. ",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. In this case the risk has the chance of gaining me nothing, whereas the certain option allows me to gain 1,500. Therefore I chose certainty.",
            "When examining this choice I look at the best case outcome. In this case the risk has the chance of gaining me 2,000. Therefore I believe that the chance to gain 2,000 outweighs the certainty in gaining 1,500.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I chose. ",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I choose." },
        {"When examining this choice I look at the worst case outcome. In this case the risk has the chance of losing me 2,200, whereas the certain option only loses me 1,650. Therefore I chose certainty.",
            "When examining this choice I look at the best case outcome. In this case the risk has the chance of losing me nothing. Therefore I believe that the chance to lose nothing outweighs the certainty of losing 1,650.",
                "To assess this choice I calculate the average gain in each case if I made this decision many times. This leads me to arrive at the values given above. When comparing these values the certain option is worth more and is therefore the option I chose. ",
                    "To arrive at these values I factor in what the average human would choose to do in this situation. Keeping this in mind I then calculate the average values and augment them with the human opinion in order to aid my decision. This leads me to arrive at the values given above. When comparing these values the risk is worth more and is therefore the option I choose." }
    };

public void ProcessButtonPush()
    {
        if (!gateCount.isTutorial && questionButton.isPushed)
        {
            data.SendMessage("WriteQuestionedFurther");

            gateCounter = gateCount.gateCounter;
            currentGate = loadedGates.GateOrder[gateCounter];
            SetText(gateResponses[currentGate, robot.RobotProfile]);
        }
        else if(questionButton.isPushed)
        {
            SetText("Questioning the robot further will allow further insight into how the robot has come to it's decision. After having questioned it further you may accept or alter its decision");
        }
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


    public void SetText(string text)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = text;

        currentButtonText = txt.text;
    }
}
