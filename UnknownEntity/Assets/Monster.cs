
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform target;
    NavMeshAgent nav;
    private Animator animator;
    public float lostDistance;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // "Walk" 트리거를 활성화하여 걷는 애니메이션을 시작
        animator.SetTrigger("Walk");
    }
    void Update()
    {
        nav.SetDestination(target.position);
    }
}