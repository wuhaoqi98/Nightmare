using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OvrAvatarHand : MonoBehaviour
{
    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        
    }

    void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.forward * 10);
    }
}
