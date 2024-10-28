using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Movement : MonoBehaviour
{

    public float moveDistance = 0.1f; // Distance to move upwards

    // Assign these in the Unity Inspector
    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;
    public Button onButton;
    public Button offButton;

    public Button respawnButton;
    public GameObject explosionPrefab; // Assign the explosion prefab in the inspector
    public Animator[] fanAnimators;  // Array of Animators for the fan animations

    private Vector3 startingPosition; // Variable to store the starting position


    private void Start()
    {
        // Stop the fan animations
        UpdateFanAnimation(false); 

        // Store the starting position
        startingPosition = transform.position;

        SetButtonInteractable(false); // Start with direction buttons inactive
        

        offButton.interactable = false;


        Debug.Log("sstra"); // Log when movement starts
    }

    private void Update()
    {
      
    }

    private void UpdateFanAnimation(bool state)
    {
        foreach (Animator animator in fanAnimators)
        {
            if (animator != null)
            {
                animator.enabled = state;
              
            }
        }
    }

    private void SetButtonInteractable(bool state)
    {
        forwardButton.interactable = state;
        backwardButton.interactable = state;
        leftButton.interactable = state;
        rightButton.interactable = state;
        upButton.interactable = state;
        downButton.interactable = state;
        respawnButton.interactable = state;
    }

    public void startMoveRight()
    {
        Debug.Log("Movement started"); // Log when movement starts
        transform.position += Vector3.right * moveDistance;
    }

    public void startMoveLeft()
    {
        Debug.Log("Movement started"); // Log when movement starts
        transform.position += Vector3.left * moveDistance;
    }

    public void startMoveForward()
    {
        Debug.Log("Movement started"); // Log when movement starts
        transform.position += Vector3.forward * moveDistance;
    }

    public void startMoveBackward()
    {
        Debug.Log("Movement started"); // Log when movement starts
        transform.position += Vector3.back * moveDistance;
    }

    public void startMoveUpward()
    {
        Debug.Log("Movement started"); // Log when movement starts
        transform.position += Vector3.up * moveDistance;
    }

    public void startMoveDownward()
    {
        Debug.Log("Movement ended");
      
        transform.position += Vector3.down * moveDistance;
    }

    public void onButtonClicked()
    {
        float dist = 0.3f;
        transform.position += Vector3.up * dist;

        // Stop the fan animations
        UpdateFanAnimation(true);

        SetButtonInteractable(true); // Start with direction buttons inactive
    }

    public void offButtonClicked() 
    {
        // Reset position to starting point
        transform.position = startingPosition;

        // Stop the fan animations
        UpdateFanAnimation(false);

    }


    private IEnumerator TriggerExplosion()
    {

        // Get the position of the drone
        Vector3 dronePosition = transform.position;

        // Instantiate the explosion at the drone's position
        //Instantiate(explosionPrefab, dronePosition, Quaternion.identity);

        // Instantiate the explosion at the drone's position
        GameObject explosion = Instantiate(explosionPrefab, dronePosition, Quaternion.identity);


        // Wait for respawn delay
        yield return new WaitForSeconds(1f);

        // Remove the explosion from the scene
        Destroy(explosion);

        // Respawn the drone at the new location
        transform.position = startingPosition; // Set the new position



        TurnOff();
        offButton.interactable = false;

    }

    private void TurnOff()
    {
        SetButtonInteractable(false); // Deactivate direction buttons
        UpdateFanAnimation(false); // Stop fan animations
    }


    public void OnRespawnButtonClicked()
    {
        StartCoroutine(TriggerExplosion());

    }
}
