using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    [Header("Beat Scroll Settings")]
    public float beatsPerMinute;

    [Tooltip("Determines if the beat scrolling has started.")]
    public bool hasStarted;

    private float beatSpeed;

    private void Start()
    {
        beatSpeed = beatsPerMinute / 60f; // Convert BPM to speed
    }

    private void Update()
    {
        if (hasStarted)
        {
            // Scroll the object downward at calculated speed
            transform.position -= new Vector3(0f, beatSpeed * Time.deltaTime, 0f);
        }
    }
}