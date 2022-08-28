using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ActivatorGate : MonoBehaviour
{   
    Animator animator;
    [SerializeField] Animator childAnimator;
    [SerializeField] float delayStartUpTime;

    void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.enabled = false;
        StartCoroutine(DelayStartUp());
    }

    IEnumerator DelayStartUp()
    {
        yield return new WaitForSecondsRealtime(delayStartUpTime * 1.283f);
        animator.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player Crushed");
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
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

