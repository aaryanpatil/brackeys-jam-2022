
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] Color32 blueColor;
    [SerializeField] Color32 yellowColor;
    [SerializeField] Color32 greenColor;
    [SerializeField] Color32 whiteColor;
    [SerializeField] float delayTime;

    Light2D finishLight;
    BoxCollider2D boxCollider2D;

    int playerCount;
    Player1Movement player1;
    Player2Movement player2;

    void Awake() 
    {
        playerCount = 0;
        finishLight = GetComponent<Light2D>();
        boxCollider2D = GetComponent<BoxCollider2D>(); 
        player1 = FindObjectOfType<Player1Movement>();  
        player2 = FindObjectOfType<Player2Movement>();  
    }

    void Update() 
    {
        if(playerCount > 0)
        {
            CheckPlayer();    
        }  
        else
        {
            finishLight.color = whiteColor;
        }  
    }

    void OnTriggerEnter2D(Collider2D other) 
    {    
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            playerCount++;
        }   
    }

    void OnTriggerExit2D(Collider2D other) 
    {    
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            playerCount--;
        }   
    }

    void CheckPlayer()
    {
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player 1")) && playerCount == 1)
        {
            finishLight.color = blueColor;
        }
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player 2")) && playerCount == 1)
        {
            finishLight.color = yellowColor;
        }
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player 1")) && boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player 2")) && playerCount == 2)
        {
            finishLight.color = greenColor;
            ProcessFinish();
        }
    }

    void ProcessFinish()
    {
        StartCoroutine(DelayFinish());
    }

    IEnumerator DelayFinish()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        player1.DisableInputs();
        player2.DisableInputs();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("Credits");
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
