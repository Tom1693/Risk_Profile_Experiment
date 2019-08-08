using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRoundEndScreen : MonoBehaviour
{
    [SerializeField] BonusRoundRobotController robot;
    [SerializeField] GameObject endScreen;
    
    void Update()
    {
        if (robot.curSpeed == 0)
        {
            endScreen.SetActive(true);
        }
        else
        {
            endScreen.SetActive(false);
        }
    }
}
