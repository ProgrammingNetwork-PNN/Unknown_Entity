using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MonsterController : MonoBehaviour
{
    public Transform target;
    NavMeshAgent nav;

    public float lostDistance;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.SetDestination(target.position);
    }
}
