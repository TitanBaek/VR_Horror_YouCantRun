using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class StateDorant : MonsterStateBase<Mannequin>
{
    float recognizeCount;

    public StateDorant(Mannequin owner) : base(owner)
    {
    }

    public override void Enter()
    {
        recognizeCount = 0f;
        owner.Agent.speed = 0f;
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
        WaitPlayer();
    }
      
    public void WaitPlayer()
    {
        // 1. ������ ����
        Collider[] colliders = owner.PlayerInSensetiveRange();
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                recognizeCount += Time.deltaTime;
                if (recognizeCount >= 5f)
                    owner.ChangeState(Mannequin_State.Chase);
            }
        } else
        {
            recognizeCount = 0f;
        }
    }


    


}
