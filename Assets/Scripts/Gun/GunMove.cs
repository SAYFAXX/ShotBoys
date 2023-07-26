using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMove : MonoBehaviour
{
    public float rotateSpeed = 45f;
    public float rotationThreshold = 45f;

    private float currentRotation = 0f;
    private bool rotateClockwise = true;

    // Update is called once per frame
    void Update()
    {
        // Silah�n d�nme a��s�n� g�ncelle
        currentRotation = (currentRotation + rotateSpeed * Time.deltaTime) % 360;

        // D�nme e�i�ine ula��ld���nda d�n�� y�n�n� de�i�tir
        if (currentRotation >= rotationThreshold && rotateClockwise)
        {
            rotateClockwise = false;
        }
        else if (currentRotation <= -rotationThreshold && !rotateClockwise)
        {
            rotateClockwise = true;
        }

        // Silah�n d�nme y�n�ne g�re Z ekseninde d�nd�r
        if (rotateClockwise)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotateSpeed * Time.deltaTime);
        }
    }
}

