using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField, Range(0.5f, 2f)] float mouseSense = 1;
    [SerializeField, Range(-20, -10)] int lookUp = -15;
    [SerializeField, Range(15, 25)] int lookDown = 20;
    
    public bool isSpectator;
    [SerializeField] float speed = 50f;

    float currentX = 0f;
    float currentY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSense;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense;

        if (!isSpectator)
        {
            currentX += mouseX;
            currentY -= mouseY;
            currentY = Mathf.Clamp(currentY, lookUp, lookDown);

            Quaternion camRotation = Quaternion.Euler(currentY, currentX, 0);
            transform.rotation = camRotation;

            player.transform.rotation = Quaternion.Euler(0, currentX, 0);
        }
        else
        {
            Vector3 rotCamera = transform.rotation.eulerAngles;
            rotCamera.x -= mouseY;
            rotCamera.z = 0;
            rotCamera.y += mouseX;
            transform.rotation = Quaternion.Euler(rotCamera);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 dir = transform.right * x + transform.forward * z;
            transform.position += dir * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}