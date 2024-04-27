using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public SpriteRenderer circle;
    public GameObject door_light;
    public Material[] colours;
    public int color_index;

    private Color[] colors = new Color[] {
        Color.black, Color.red, Color.blue, Color.green, Color.yellow
    };

    void ColourSet(int index)
    {
        Material smallColour = colours[index * 2 + 1];
        Material Colour = colours[index * 2];

        circle.material = smallColour;
        door_light.GetComponent<SpriteRenderer>().material = Colour;
        door_light.GetComponent<Light>().color = colors[index];
    }

    /* door rotate */
    public Boolean is_rotate_Sig = false;
    private Boolean is_rotating = false;
    private Boolean opening = false;

    public Transform doorTransform;
    public float openRotationTime = 10f;
    public float closeRotationTime = 0.5f;
    public float openRotationAngle = -105f;
    private float closeRotationAngle = 0f;
    public AnimationCurve rotationCurve;
    private float rotationTimer = 0f;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    private RoomManager roomManager;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }

    void Update()
    {
        HandleInput();
        ProcessRotation();
    }

    private void HandleInput()
    {
        if (opening && !is_rotate_Sig)
        {
            opening = false;
            closeRotationAngle = -doorTransform.localRotation.eulerAngles.y;
            ChangeRotationDirection(closeRotationAngle);
        }
    }

    private void ChangeRotationDirection(float angle)
    {
        float currentAngle = doorTransform.localRotation.eulerAngles.y;
        float targetAngle = Mathf.Round((currentAngle + angle) / openRotationTime) * openRotationTime;
        targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        startRotation = doorTransform.localRotation;
        rotationTimer = openRotationTime - closeRotationTime;
    }

    private void ProcessRotation()
    {
        if (!is_rotating && is_rotate_Sig)
        {
            is_rotating = true;
            opening = true;
            StartRotation(openRotationAngle);
        }
        if (is_rotating)
        {
            rotationTimer += Time.deltaTime;
            float t = rotationTimer / openRotationTime;
            t = rotationCurve.Evaluate(t);
            doorTransform.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            if (rotationTimer >= openRotationTime)
            {
                if (is_rotate_Sig)
                {
                    is_rotate_Sig = false;
                    // GameManager.GameOver();
                }
                else
                {
                    //ChangeRotationDirection(-openRotationAngle);
                    color_index = 0;
                    ColourSet(0);
                    doorTransform.localRotation = targetRotation;
                    rotationTimer = 0f;
                    is_rotating = false;
                }
            }
        }
    }

    private void StartRotation(float angle)
    {
        ColourSet(color_index);
        startRotation = doorTransform.localRotation;
        targetRotation = Quaternion.Euler(doorTransform.localEulerAngles + new Vector3(0f, angle, 0f));
    }
}