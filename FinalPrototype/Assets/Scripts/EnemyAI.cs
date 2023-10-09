using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

     NavMeshAgent agent;
    public Transform[] wayPoints;
    int wayPointIndex = 0;
    Vector3 target;
    [SerializeField] private float _speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        agent.speed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target) <1 )
        {
            //IterateWayPointIndex();
            RandomWayPoint();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = wayPoints[wayPointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWayPointIndex()
    {
        wayPointIndex++;
        if(wayPointIndex == wayPoints.Length)
        {
            wayPointIndex = 0;
        }
    }

    void RandomWayPoint()
    {
        wayPointIndex = Random.Range(0, wayPoints.Length);
    }

}
