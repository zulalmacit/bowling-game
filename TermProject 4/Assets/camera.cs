using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distance;
    public float rotationSpeed = 100f;
    public float directionSpeed = 1f;
    public float height;
    public GameObject objectToFollow;

    void LateUpdate()
    {
        if (objectToFollow == null)
            return;

        Vector3 destination = objectToFollow.transform.position;
        destination.x = 5f;
        
        destination.z += distance;

        transform.position = destination;

        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -10f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 10f;
        }

        // Kamerayı yatay girişe göre döndür
        if (horizontalInput != 0f)
        {
            float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(rotationAmount,  0f,0f, Space.World); // Dönüşü dünya koordinatlarına göre yap
        }
    }
}
