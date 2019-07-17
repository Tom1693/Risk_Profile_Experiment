using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    struct GateData
    {
        public Vector3 left;
        public Vector3 right;
        public Vector3 end;
    }

    public Camera cam;
    public NavMeshAgent agent;

    public Gate_Loader gates;
    


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
