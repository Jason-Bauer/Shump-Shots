﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    // Fields
    private static float left;
    private static float right;
    private static float top;
    private static float bottom;

    private float viewPosX = 0.175f;
    private float viewPosY = 0.0f;
    private float viewWidth = 0.65f;
    private float viewHeight = 1f;

    /// <summary>
    /// Return left side of screen in world units
    /// </summary>
    public static float Left
    {
        get { return left; }
    }

    /// <summary>
    /// Return top side of screen in world units
    /// </summary>
    public static float Top
    {
        get { return top; }
    }

    /// <summary>
    /// Return right side of screen in world units
    /// </summary>
    public static float Right
    {
        get { return right; }
    }

    /// <summary>
    /// Return bottom side of screen in world units
    /// </summary>
    public static float Bottom
    {
        get { return bottom; }
    }


    // Use this for initialization
    void Start()
    {
        Camera.main.rect = new Rect(viewPosX, viewPosY, viewWidth, viewHeight);

        left = Camera.main.ViewportToWorldPoint(Vector3.zero).z
            + Camera.main.transform.position.z;

        right = Camera.main.ViewportToWorldPoint(Vector3.right).z
            + Camera.main.transform.position.z;

        top = Camera.main.ViewportToWorldPoint(Vector3.up).x
            + Camera.main.transform.position.x;

        bottom = Camera.main.ViewportToWorldPoint(Vector3.zero).x
            + Camera.main.transform.position.x;
    }

    void Update()
    {

    }
}