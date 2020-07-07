using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 0f);
    [SerializeField] float period = 4f;

    Vector3 startPos;
    float movementFactor;
    const float tau = Mathf.PI * 2;  // About 6.28
    const float sinOffset = 0.5f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);  // -1 to 1

        movementFactor = rawSinWave / 2f + sinOffset;  // Adjust to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
    }
}
