using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    #region Private Variables

    private Animator _animator;
    
    #endregion
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation()
    {
        _animator.SetBool("Run",true);
    }
}
