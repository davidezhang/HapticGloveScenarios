using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ColliderController : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private string originalLabel;

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
        Debug.Log("ColliderController: Collision with model part collider");
        debugText.text = "Collided!";
    }

    private void OnCollisionExit(Collision collision)
    {
        debugText.text = originalLabel;
    }



}
