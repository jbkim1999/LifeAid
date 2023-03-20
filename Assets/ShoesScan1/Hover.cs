using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{

    // Define the amount of vertical displacement
    public float displacementAmount = 5f;
    
    // Define the speed of rotation
    public float rotationSpeed = 10f;

    // Define the initial position of the object
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Store the object's initial position
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position of the object
        Vector3 newPosition = startPosition + new Vector3(0, Mathf.Sin(Time.time) * displacementAmount, 0);

        // Rotate the object around the Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Update the object's position
        transform.position = newPosition;
    }

}

 