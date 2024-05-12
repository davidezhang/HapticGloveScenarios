using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticRouter : MonoBehaviour
{
    // TODO
    // 1. Send debug messages when hand is colliding with model part colliders, not bounds collider 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Subscribe to an event about a collision with a model part
    public void OnModelPartCollisionEnter(Collider collider)
    {
        Debug.Log("HapticRouter: Collision with model part collider");
    }

}
