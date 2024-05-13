using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ColliderController : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private string originalLabel;
    private Vector3 collisionForce = Vector3.zero;
    private float force;

    // Start is called before the first frame update
    void Start()
    {
        originalLabel = debugText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When a collision with a model part collider is detected, send an event to the HapticRouter
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("ColliderController: Collision with model part collider");

        //collisionForce = collision.impulse / Time.fixedDeltaTime;
       if (collision.collider.tag != "ignoreCollision")
        {
            Vector3 collisionVelocity = collision.relativeVelocity;
            force = collisionVelocity.magnitude * 100f;
            debugText.text = "Collided!" + (int)force;
        }
       
        
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag != "ignoreCollision")
        {
            debugText.text = "Collided!" + (int)force;
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag != "ignoreCollision")
        {
            debugText.text = originalLabel;
        }
        
    }



}
