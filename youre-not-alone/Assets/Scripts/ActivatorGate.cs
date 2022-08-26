using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivatorGate : MonoBehaviour
{   
    Animator animator;
    [SerializeField] Animator childAnimator;

    void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Crushed");
        }    
    }

    public void GateDeactivated(bool isDeactivated)
    {
        if(isDeactivated)
        {
            animator.speed = 0;
            childAnimator.SetBool("Deactivated", true);
        }
        else
        {
            animator.speed = 1;
            childAnimator.SetBool("Deactivated", false);
        }
    }
}

