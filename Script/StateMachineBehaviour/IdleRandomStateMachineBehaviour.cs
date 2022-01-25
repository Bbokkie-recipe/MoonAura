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

    //Ʈ�����ǿ� �ʿ��� �ð� ���
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randowmNormalTime = Random.Range(minNormTime, maxNormTime);
    }

    //���¿� �����ϰ� ������Ʈȣ��
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //�ش� ������Ʈ�� ���� ���� �ʴ�
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        {
            //�ƹ� �͵� ���� �ʱ�
            animator.SetInteger(hashRandomIdle, -1);
        }
        //randowmNormalTime�� �������� Random numberOfStates ����
        if (stateInfo.normalizedTime > randowmNormalTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(hashRandomIdle, Random.Range(0, numberOfStates));
        }
    }
}
