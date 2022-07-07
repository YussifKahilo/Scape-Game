using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    int lastAnimationIndex = 0;

    [SerializeField] float time;

    internal Animator Anim { get => anim; set => anim = value; }

    public void SetJumping(bool state)
    {
        anim.SetBool("Jump", state);
        StartCoroutine(SetIsJumping(state));
    }

    IEnumerator SetIsJumping(bool state)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("isJumping", state);
    }

    public void SetAnimation(Vector3 movementDir)
    {
        int animationIndex = 0;

        animationIndex += movementDir.x > 0 ? 2 : 0;
        animationIndex += movementDir.x < 0 ? 1 : 0;

        animationIndex += movementDir.z > 0 ? 20 : 0;
        animationIndex += movementDir.z < 0 ? 10 : 0;

        if (lastAnimationIndex != animationIndex)
        {
            lastAnimationIndex = animationIndex;
            anim.SetInteger("Walk Dir", animationIndex);
        }
    }
}
