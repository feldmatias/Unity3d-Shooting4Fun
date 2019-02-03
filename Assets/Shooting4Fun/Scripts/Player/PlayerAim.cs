using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public float verticalOffset = 3f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorIK()
    {
        if (IsAiming){
            var targetPosition = transform.position + transform.forward;
            targetPosition.y += transform.position.y - Camera.main.transform.position.y + verticalOffset;

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, targetPosition);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, targetPosition);
        } else {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        }
    }

    public void SetIsAiming(bool aiming)
    {
        IsAiming = aiming;
    }

    public bool IsAiming { get; private set; } = false;
}
