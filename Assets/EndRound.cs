using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRound : MonoBehaviour
{
    [SerializeField] UIController UI;
    [SerializeField] RobotController robot;
    [SerializeField] GameObject endScreen;


    void Update()
    {
        print(UI.gateCounter);

        if(UI.gateCounter > 9)
        {
            endScreen.SetActive(true);
        }
        else
        {
            endScreen.SetActive(false);
        }
    }

    private void ToggleUI()
    {
        if (robot.curSpeed > 0)
        {
            endScreen.SetActive(false);
        }
        else
        {
            endScreen.SetActive(true);
        }
    }
}
