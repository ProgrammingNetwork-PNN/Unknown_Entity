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
            // SetDestination �� ���� �� frame�� �ѱ������ �ڵ�
            yield return null;
        }

        // ��ǥ������ ���� �Ÿ��� ���ߴ� �������� �۰ų� ������
        if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
        {

        }
        // ��ǥ���� �Ÿ��� �־��� ���
        else if (nmAgent.remainingDistance > lostDistance)
        {
            target = null;
            nmAgent.SetDestination(transform.position);
            yield return null;
            // StateMachine �� ���� ����
        }
        else
        {
            // WalkFWD �ִϸ��̼��� �� ����Ŭ ���� ���
            yield return new WaitForSeconds(curAnimStateInfo.length);
        }
    }

    void Update()
    {
        if (target == null) return;
        // target �� null �� �ƴϸ� target �� ��� ����
        nmAgent.SetDestination(target.position);
    }
}
