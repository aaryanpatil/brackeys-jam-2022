using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    Animator animator;

    bool isActivated;

    [SerializeField] ActivatorGate activatorGate;

    void Awake() 
    {   
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsPressed", true);
            isActivated = true;
            activatorGate.GateDeactivated(isActivated);
        }   
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsPressed", false);

            isActivated = false;
            activatorGate.GateDeactivated(isActivated);
        }   
    }
}
