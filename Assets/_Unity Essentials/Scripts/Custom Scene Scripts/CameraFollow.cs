using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player (character) to follow
    public float smoothSpeed = 5f; // Smooth movement speed
    public Vector3 offset; // Offset to keep some distance

    private float lookUpDownValue = 2.0f;
    private float verticalOffsetStart;
    private bool canLookDown = true;
    private bool canLookUp = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      verticalOffsetStart = offset.y;  
    }

    // Update is called once per frame
    void Update()
    {
        if (offset.y == verticalOffsetStart)
        {
            canLookDown = true;
            canLookUp = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (canLookDown)
            {
                LookDown();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canLookUp)
            {
                LookUp();
            }
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }

    void LookDown()
    {
        offset.y -= lookUpDownValue;
        canLookDown = false;
    }

    void LookUp()
    {
        offset.y += lookUpDownValue;
        canLookUp = false;
    }
}
