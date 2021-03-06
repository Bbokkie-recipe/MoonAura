using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRandomStateMachineBehaviour : StateMachineBehaviour
{
    #region Varibles
    [SerializeField] public int numberOfStates = 2;
    [SerializeField] public float minNormTime = 0f;
    [SerializeField] public float maxNormTime = 5f;

    [SerializeField] public float randowmNormalTime;

    readonly int hashRandomIdle = Animator.StringToHash("RandomIdle");

    #endregion

    //트랜지션에 필요한 시간 계산
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randowmNormalTime = Random.Range(minNormTime, maxNormTime);
    }

    //상태에 진입하고 업데이트호출
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //해당 스테이트에 들어와 있지 않다
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        {
            //아무 것도 하지 않기
            animator.SetInteger(hashRandomIdle, -1);
        }
        //randowmNormalTime이 지났으면 Random numberOfStates 지정
        if (stateInfo.normalizedTime > randowmNormalTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(hashRandomIdle, Random.Range(0, numberOfStates));
        }
    }
}
