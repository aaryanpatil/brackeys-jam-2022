using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Activator : MonoBehaviour
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
            animator.SetBool("IsPressed", true);
        }   
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsPressed", false);
        }   
    }
}
