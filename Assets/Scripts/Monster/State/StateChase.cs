using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : MonsterStateBase<Mannequin>
{

    public StateChase(Mannequin owner) : base(owner)
    {
    }

    public override void Enter()
    {
        owner.Agent.speed = owner.MoveSpeed;
    }

    public override void Exit()
    {
    }

    public override void LateUpdate()
    {
    }

    public override void Setup()
    {
    }

    public override void Transition()
    {
    }

    public override void Update()
    {
        owner.Agent.destination = owner.playerPos.position;

        if (Vector3.Distance(owner.playerPos.position, owner.transform.position) > owner.ChaseRange)        // �÷��̾ ChaseRange���� ����� Return(Patrol�� �ص� �ɵ�..)���� ���� ����
        {
            owner.ChangeState(Mannequin_State.Dormant);
        }
        else if (Vector3.Distance(owner.playerPos.position, owner.transform.position) < owner.AttackRange)  // �÷��̾ ���ݹ����� ������ Attack���� ���� ����
        {
            owner.ChangeState(Mannequin_State.Attack);
        }
    }
}
