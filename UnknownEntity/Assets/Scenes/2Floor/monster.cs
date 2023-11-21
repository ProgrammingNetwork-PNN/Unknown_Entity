using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MonsterController : MonoBehaviour
{
    Transform target;
    NavMeshAgent nmAgent;
    Animator anim;

    public float lostDistance;

    void Start()
    {
        anim = GetComponent<Animator>();
        nmAgent = GetComponent<NavMeshAgent>();
    }
    IEnumerator CHASE()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (curAnimStateInfo.IsName("WalkFWD") == false)
        {
            anim.Play("WalkFWD", 0, 0);
            // SetDestination 을 위해 한 frame을 넘기기위한 코드
            yield return null;
        }

        // 목표까지의 남은 거리가 멈추는 지점보다 작거나 같으면
        if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
        {

        }
        // 목표와의 거리가 멀어진 경우
        else if (nmAgent.remainingDistance > lostDistance)
        {
            target = null;
            nmAgent.SetDestination(transform.position);
            yield return null;
            // StateMachine 을 대기로 변경
        }
        else
        {
            // WalkFWD 애니메이션의 한 사이클 동안 대기
            yield return new WaitForSeconds(curAnimStateInfo.length);
        }
    }

    void Update()
    {
        if (target == null) return;
        // target 이 null 이 아니면 target 을 계속 추적
        nmAgent.SetDestination(target.position);
    }
}
