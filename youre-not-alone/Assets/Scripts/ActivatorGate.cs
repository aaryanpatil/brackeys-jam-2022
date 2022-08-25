using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivatorGate : MonoBehaviour
{   
    Animator animator;

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
        }
        else
        {
            animator.speed = 1;
        }
    }
}

