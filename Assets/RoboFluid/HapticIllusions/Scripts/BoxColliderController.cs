using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class BoxColliderController : MonoBehaviour
{
    public GameObject[] mysteryObjects;
    public GameObject nextButton;
    public GameObject mysteryBox;
    private int currentIndex = 0;
    private bool isButtonVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject mysteryObject in mysteryObjects)
        {
            mysteryObject.SetActive(false);
        }

        nextButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Assuming the "other" is a hand or finger collider
        if (other.CompareTag("Hand") && !isButtonVisible)
        {
            // Call ObjectTouched with the current index
            ObjectTouched();
        }
    }

    private void ObjectTouched()
    {
        // TODO: Trigger corresponding haptic feedback
        print("Trigger haptic feedback for object " + currentIndex);

        // Show the next button in 3 seconds
        StartCoroutine(ShowNextButton());

    }

    private IEnumerator ShowNextButton()
    {
        yield return new WaitForSeconds(3);
        nextButton.SetActive(true);
        isButtonVisible = true;
    }

    public void NextButtonClicked()
    {
        // If current object is hidden, reveal it
        if (mysteryObjects[currentIndex].activeSelf == false)
        {
            mysteryObjects[currentIndex].SetActive(true);
            mysteryBox.SetActive(false);

        } else
        {
            // If already revealed, move on to the next object
            mysteryObjects[currentIndex].SetActive(false);
            mysteryBox.SetActive(true);
            // Hide the next button
            nextButton.SetActive(false);
            isButtonVisible = false;
            currentIndex++;
            if (currentIndex >= mysteryObjects.Length)
            {
                currentIndex = 0;
            }
        }


    }
}
