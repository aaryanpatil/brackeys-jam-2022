using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class WallInteract : MonoBehaviour
{
    [Header("Gizmo Setings")]
    public LayerMask groundLayer;
    public Color32 gizmoColor;
    public float collisionRadius = 0.3f; 

    [Header("Wall Stick Detection Adjustment")]

    public Vector2 leftOffset;
    public Vector2 rightOffset;
    
    [Header("Wall Stick Check")]
    public bool onWall;
    public bool onLeftWall;
    public bool onRightWall;
    public int wallSide;
    
    void Update()
    {
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer) 
            || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer); 
        
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer); 
        
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? 1 : -1;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere((Vector2) transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2) transform.position + rightOffset, collisionRadius);
    }
}
