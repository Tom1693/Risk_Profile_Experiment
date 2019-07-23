using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] GameObject UI;
    [SerializeField] RobotController robot;
    [SerializeField] Text leftText;
    [SerializeField] Text rightText;
    [SerializeField] Button leftButton;

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
        "50% chance of irradiation measuring 1,100μSv\n20% chance of irradiation measuring 0μSv",
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayRisk();

        ToggleUI();
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


            if (Input.GetKeyDown("u") || Input.GetKeyDown("c"))
            {
                gateCounter++;
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
