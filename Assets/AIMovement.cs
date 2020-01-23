using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public float patrolPointTolerance = 1.0f;
    public float delayBetweenPatrol = 3.0f;
    public List<Transform> patrolPoints;
    public List<Transform> targets;
    private Rigidbody _rb;
    private bool _hasDetected = false;
    public bool hasDetected
    {
        get { return _hasDetected; }
        set { _hasDetected = value; }
    }
    private int _currentPointTracker = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasDetected)
        {
            GetComponent<NavMeshAgent>().destination = targets[0].position;
        }
    }
    IEnumerator Patrol()
    {
        while (!_hasDetected)
        {
            if (Vector3.Distance(transform.position, patrolPoints[_currentPointTracker].transform.position) <= patrolPointTolerance)
            {

                yield return new WaitForSeconds(delayBetweenPatrol);
                _currentPointTracker++;

                if (_currentPointTracker % patrolPoints.Count == 0)
                {
                    _currentPointTracker = 0;
                }

            }
            else
            {
                GetComponent<NavMeshAgent>().destination = new Vector3(patrolPoints[_currentPointTracker].transform.position.x,
                                                                    transform.position.y,
                                                                    patrolPoints[_currentPointTracker].transform.position.z);
            }
            yield return null;
        }
    }
}
