using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    [SerializeField]
    bool patrolWaiting;

    [SerializeField]
    float totalWaitTime = 3f;

    [SerializeField]
    float switchProbability = 0.2f;

    [SerializeField]
    List<Transform> wayPoints;
    [SerializeField]
    GameObject canvas;

    bool travelling;
    bool waiting;
    bool patrolForward=true;
    bool isHider = false;
    float waitTimer;
    int currentPatrolIndex;
    NavMeshAgent navMeshAgent;
    Rigidbody rb;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        if (navMeshAgent == null)
        {
            Debug.LogError("No NavMeshAgent on" + gameObject + ", nerd");
        }
        else
        {
            if(wayPoints != null && wayPoints.Count >= 2)
            {
                currentPatrolIndex = GetClosestPoint(wayPoints);
                SetDestination();
            }
            else
            {
                Debug.Log("Need more waypoints, nerd");
            }
        }
    }

    void Update()
    {
        if (navMeshAgent.enabled)
        {
            if (travelling && navMeshAgent.remainingDistance < 1.0f)
            {
                travelling = false;
                if (patrolWaiting)
                {
                    waiting = true;
                    waitTimer = 0f;
                }
                else
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
            }
            if (waiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= totalWaitTime)
                {
                    waiting = false;

                    ChangePatrolPoint();
                    SetDestination();
                }
            }
        }
    }
    private void SetDestination()
    {
        if (wayPoints != null)
        {
            //Debug.Log("Destination set to " + currentPatrolIndex);
            Vector3 targetVector = wayPoints[currentPatrolIndex].position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f,1f) <= switchProbability)
        {
            patrolForward = !patrolForward;
        }
        if (patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % wayPoints.Count;
        }
        else
        {
            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = wayPoints.Count - 1;
            }
        }
    }
    int GetClosestPoint(List<Transform> points)
    {
        int tMin = 0;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        for(int i = 0; i <= wayPoints.Count - 1; i++){ 
            float dist = Vector3.Distance(points[i].position, currentPos);
            if (dist < minDist)
            {
                tMin = i;
                minDist = dist;
            }
        }
        return tMin;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            
            navMeshAgent.enabled = false;
            rb.isKinematic = false;
            if (isHider)
            {
                Debug.Log("target!");
                canvas.SetActive(true);
            }
        }
    }
    public void BecomeHider()
    {
        isHider = true;
        totalWaitTime /= 2;
        Debug.Log("I, " + this.gameObject.name + " am the hider");
    }
}