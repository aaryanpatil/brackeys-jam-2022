using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;

    public float smoothTime = 0.2f;
    public Vector3 offset;

    public float minZoom = 6f;
    public float maxZoom = 4f;
    public float zoomLimiter = 6f;
    
    private  Vector3 velocity;
    private Camera cam;

    void Start() 
    {
        cam = GetComponent<Camera>();     
    }

    void LateUpdate() 
    {
        if (targets.Count == 0) { return; }
        Move();
        Zoom();
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position); 
        }

        return bounds.center;
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position); 
        }

        return bounds.size.x;
    }
}
