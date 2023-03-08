using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;


public class Man : MonoBehaviour, IControllable
{
    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        // Get the NavMeshAgent component
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void ReleaseControl()
    {
        _navMeshAgent.isStopped = true;
    }

    public void TakeControl()
    {
        _navMeshAgent.isStopped = false;
    }

    [Button]
    public void TestTakeControl()
    {
        Debug.Log("Take Control Man");
        TakeControl();
    }

    [Button]
    public void TestReleaseControl()
    {
        Debug.Log("Release Control Man");
        ReleaseControl();
    }
}
