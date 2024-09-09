//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class Movement : MonoBehaviour
//{
//    // Start is called before the first frame update
//    public Camera mainCamera;
//    public int layer = 8;
//    public GameObject moveIcon;
//    NavMeshAgent myNavMeshAgent;
//    void Start()
//    {
//        myNavMeshAgent = GetComponent<NavMeshAgent>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButton(1))
//        {
//            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
//            {
//                if (Physics.Raycast(ray, out RaycastHit hit))
//                {
//                    if (hit.collider.gameObject.layer == layer)
//                    {
//                        myNavMeshAgent.SetDestination(hit.point);
//                    }
//                }
//            }
//        }

//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update+
    public Camera mainCamera;
    public int layer = 8;
    public NavMeshAgent agent;
    public float rotateSpeedMovement = 0.05f;
    private float rotateVelocity;

    public Animator anim;
    float motionSmoothTime = 0.1f;

    [Header("Enemy Targeting")]
    public GameObject targetEnemy;
    public float stoppingDistance;
    //private OutLineManager hmScript;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //hmScript = GetComponent<OutLineManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Animation();
        Move();

    }

    public void Animation() 
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime); //transitions from idle to running
    }

    public void Move()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {   //Movement
                    if (hit.collider.gameObject.layer == layer)
                    {
                        MoveToPosition(hit.point);
                    }
                    else if (hit.collider.CompareTag("Enemy"))
                    {
                        MoveTowardsEnemy(hit.collider.gameObject);
                    }


                }
            }
        }

        if (targetEnemy != null)
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) > stoppingDistance)
            {
                agent.SetDestination(targetEnemy.transform.position);
            }
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
        agent.stoppingDistance = 0;

        Rotation(position);

        if (targetEnemy != null)
        {
            //hmScript.DeselectHighlight();
            targetEnemy = null;
        }
    }

    public void MoveTowardsEnemy(GameObject enemy)
    {
        targetEnemy = enemy;
        agent.SetDestination(targetEnemy.transform.position);
        agent.stoppingDistance = stoppingDistance;

        Rotation(targetEnemy.transform.position);
       // hmScript.SelectedHighlight();
    }

    public void Rotation(Vector3 lookatPosition) 
    {
        //Rotation
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookatPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
            ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

}


