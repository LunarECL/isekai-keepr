using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public Transform roomTransform;
    public float rotationDuration = 0.4f;
    public AnimationCurve rotationCurve;

    private bool isRotating = false;
    private float rotationTimer = 0f;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private Queue<float> rotationQueue = new Queue<float>();

    private void Update()
    {
        HandleInput();
        ProcessRotation();
    }
    
    public int GetCurrentWallIndex()
    {
        // 현재 바라보고 있는 문의 인덱스 반환 0 = 1, 3 = 1, 2 = 2, 1 = 3
        float currentAngle = roomTransform.rotation.eulerAngles.y;
        int wallIndex = Mathf.RoundToInt(currentAngle / 90f) % 4;
        
        if (wallIndex == 0)
        {
            wallIndex = 0;
        }
        else if (wallIndex == 3)
        {
            wallIndex = 1;
        }
        else if (wallIndex == 2)
        {
            wallIndex = 3;
        }
        else if (wallIndex == 1)
        {
            wallIndex = 2;
        }
        
        return wallIndex;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isRotating)
            {
                ChangeRotationDirection(90f);
            }
            else
            {
                EnqueueRotation(90f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (isRotating)
            {
                ChangeRotationDirection(-90f);
            }
            else
            {
                EnqueueRotation(-90f);
            }
        }
    }

    private void ChangeRotationDirection(float angle)
    {
        float currentAngle = roomTransform.rotation.eulerAngles.y;
        float targetAngle = Mathf.Round((currentAngle + angle) / 90f) * 90f;
        targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        startRotation = roomTransform.rotation;
        rotationTimer = 0f;
        rotationQueue.Clear();
    }

    private void EnqueueRotation(float angle)
    {
        rotationQueue.Enqueue(angle);
    }

    private void ProcessRotation()
    {
        if (!isRotating && rotationQueue.Count > 0)
        {
            StartRotation(rotationQueue.Dequeue());
        }

        if (isRotating)
        {
            rotationTimer += Time.deltaTime;
            float t = rotationTimer / rotationDuration;
            t = rotationCurve.Evaluate(t);
            roomTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            if (rotationTimer >= rotationDuration)
            {
                roomTransform.rotation = targetRotation;
                rotationTimer = 0f;
                isRotating = false;

                if (rotationQueue.Count > 0)
                {
                    StartRotation(rotationQueue.Dequeue());
                }
            }
        }
    }

    private void StartRotation(float angle)
    {
        isRotating = true;
        startRotation = roomTransform.rotation;
        targetRotation = Quaternion.Euler(roomTransform.eulerAngles + new Vector3(0f, angle, 0f));
    }
}