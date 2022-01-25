using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static HeroMovement;

public class CharController : MonoBehaviour
{

    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform mainCamBox;
    [SerializeField] public Animator playerAni;
    readonly int moveHash = Animator.StringToHash("Move");
    readonly int fallingHash = Animator.StringToHash("Falling");
    readonly int jumpHash = Animator.StringToHash("Jump");
    public void Jump()
    {
        playerAni.SetTrigger(jumpHash);
    }
    public void Move(Vector2 inputDir)
    {
        Vector2 moveInput = inputDir;
        bool isMove = moveInput.magnitude != 0;
        playerAni.SetBool(moveHash, isMove);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(mainCamBox.forward.x, 0f, mainCamBox.forward.z).normalized;
            Vector3 lookRight = new Vector3(mainCamBox.right.x, mainCamBox.right.y, mainCamBox.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            
            characterBody.rotation = Quaternion.Slerp(characterBody.rotation, Quaternion.LookRotation(moveDir),0.2f);
            transform.position += moveDir * Time.deltaTime * 6f;
        }
        Debug.DrawRay(mainCamBox.position, new Vector3(mainCamBox.forward.x, 0f, mainCamBox.forward.z).normalized, Color.red);
    }

    public void LookAround(Vector3 inputDir)
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mouseDelta = inputDir;
            Vector3 mainCamAngle = mainCamBox.rotation.eulerAngles;
            //mainCamBox xÃà È¸Àü Á¦ÇÑ
            float camBoxAnglex = mainCamAngle.x - mouseDelta.y;
            if (camBoxAnglex < 180f)
            {
                camBoxAnglex = Mathf.Clamp(camBoxAnglex, -1f, 70f);
            }
            else
            {
                camBoxAnglex = Mathf.Clamp(camBoxAnglex, 335f, 361f);
            }

            mainCamBox.rotation = Quaternion.Euler(camBoxAnglex-mouseDelta.y, mainCamAngle.y + mouseDelta.x, mainCamAngle.z);
        }
    }

}
