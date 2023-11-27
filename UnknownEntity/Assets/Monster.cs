
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
        // "Walk" Ʈ���Ÿ� Ȱ��ȭ�Ͽ� �ȴ� �ִϸ��̼��� ����
        animator.SetTrigger("Walk");
    }
    void Update()
    {
        nav.SetDestination(target.position);
    }
}