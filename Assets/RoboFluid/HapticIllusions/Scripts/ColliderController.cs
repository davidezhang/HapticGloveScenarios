using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ColliderController : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private string originalLabel;
    private Vector3 collisionForce = Vector3.zero;
    private float force;

    private Visualizer fingerVisualizer;

    private Vector3 enterPoint;

    public OVRSkeleton skeletonLeft; 
    public OVRSkeleton skeletonRight;
    public float MaterialMultiplier = 50f;

    private bool initialized = false;

    private string collidedFinger = "";
    // Start is called before the first frame update
    void Start()
    {
        originalLabel = debugText.text;

        fingerVisualizer = FindObjectOfType<Visualizer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized && skeletonLeft.Capsules.Count > 0)
        {
            foreach (OVRBone collider in skeletonLeft.Bones)
            {
                Visualizer vis = collider.Transform.AddComponent<Visualizer>();
                vis.BoneId = collider.Id;
                vis.hand = OVRHand.Hand.HandLeft;
            }
        
            foreach (OVRBone collider in skeletonRight.Bones)
            {
                Visualizer vis = collider.Transform.AddComponent<Visualizer>();
                vis.BoneId = collider.Id;
                vis.hand = OVRHand.Hand.HandRight;
            }

            initialized = true;
        }
    }

    // When a collision with a model part collider is detected, send an event to the HapticRouter
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("ColliderController: Collision with model part collider");

        //collisionForce = collision.impulse / Time.fixedDeltaTime;
       if (collision.collider.tag != "ignoreCollision" && collision.collider.GetComponent<Visualizer>())
       {
            Vector3 collisionVelocity = collision.relativeVelocity;
            force = collisionVelocity.magnitude * 100f;

            Visualizer vis = collision.collider.GetComponent<Visualizer>();
            if (vis)
            {
                OVRSkeleton currentHand = vis.hand == OVRHand.Hand.HandLeft ? skeletonLeft : skeletonRight;

                //OVRSkeleton.BoneId id = currentHand.Bones[collision.collider.GetComponent<Visualizer>().BoneId].Id;
                collidedFinger = OVRSkeleton.BoneLabelFromBoneId(OVRSkeleton.SkeletonType.HandLeft, vis.BoneId);
                vis.OnForceChanged(force);
            }
            debugText.text = "Collided! " + (int)force + " " + collidedFinger;

            enterPoint = collision.GetContact(0).point;
        }
       
        
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag != "ignoreCollision" && collision.collider.GetComponent<Visualizer>())
        {

            Vector3 collisionVelocity = collision.relativeVelocity;
            force = collisionVelocity.magnitude * 100f;
            
            float pressure = Vector3.Distance(enterPoint, collision.GetContact(0).point);
            force += pressure * MaterialMultiplier;
            
            debugText.text = "Collided! " + (int)force + " " + collidedFinger;
            
            Visualizer vis = collision.collider.GetComponent<Visualizer>();
            if (vis)
            {
                vis.OnForceChanged(force);
            }
            
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag != "ignoreCollision" && collision.collider.GetComponent<Visualizer>())
        {
            collidedFinger = "";
            debugText.text = originalLabel;
            Visualizer vis = collision.collider.GetComponent<Visualizer>();
            if (vis)
            {
                vis.OnForceDisable();
            }
        }
        
    }



}
