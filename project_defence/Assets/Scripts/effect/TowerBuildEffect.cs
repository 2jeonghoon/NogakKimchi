using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Boom()
    {
        //Debug.Log("Boom");
        animator.SetTrigger("Boom");
    }

}
