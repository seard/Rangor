using UnityEngine;
using System.Collections;

public class NetAnimator : MonoBehaviour
{

    Animator animator;
    public string tempdodkey;

    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
    }

    [RPC]
    void setDoDAnimationBool(string _tag, string _key, bool _bool)
    {
        // This RPC function is called on both deceit and will, check who is calling it and teleport only him
        if (tag == _tag)
        {
            // Trigger teleport animation
            animator.SetBool("Teleporting", _bool);
            tempdodkey = _key;
        }
    }

    [RPC]
    void doDTeleport(string _tag, string _key, Vector3 _targetPos)
    {
        // This RPC function is called on both deceit and will, check who is calling it and teleport only him
        if (tag == _tag)
        {
            if (!GetComponent<NetworkView>().isMine)
            {
                // update targetpos for other player
                GetComponent<ItemAbilityManager>().items[_key].targetPos = _targetPos;
            }
        }
    }
}
