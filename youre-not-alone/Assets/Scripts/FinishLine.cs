using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FinishLine : MonoBehaviour
{
    [SerializeField] Color32 blueColor;
    [SerializeField] Color32 yellowColor;
    [SerializeField] Color32 greenColor;
    [SerializeField] Color32 whiteColor;
    Light2D finishLight;
    BoxCollider2D boxCollider2D;

    int playerCount;

    void Awake() 
    {
        playerCount = 0;
        finishLight = GetComponent<Light2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();   
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
        }

    }
}
