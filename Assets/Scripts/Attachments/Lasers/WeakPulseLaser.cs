using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPulseLaser : LaserAttachment
{
    public int pulseCount = 3;

    protected override void FireLaser()
    {
        Debug.Log($"Firing pulse laser with {damage} damage, {range} range, and {pulseCount} pulses");
        // Implement pulse laser firing logic
    }
}
