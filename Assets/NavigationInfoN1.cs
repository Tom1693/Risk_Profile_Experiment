using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationInfoN1 : MonoBehaviour
{

    Vector3 LeftPoint = new Vector3(1, 1, 1);
    Vector3 RightPoint = new Vector3(1, 1, 1);
    Vector3 EndPoint = new Vector3(1, 1, 1);


    public Vector3 GetLeftPoint()
    {
        return LeftPoint;
    }

    public Vector3 GetRightPoint()
    {
        return (RightPoint);
    }

    public Vector3 GetEndPoint()
    {
        return (EndPoint);
    }
}
