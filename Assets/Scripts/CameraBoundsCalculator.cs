// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class CameraBoundsCalculator
{
    public static float CameraMinX { get; private set; }

    public static float CameraMaxX { get; private set; }

    public static float CameraMaxY { get; private set; }

    public static void CalculateCameraBounds()
    {
        var camera = Camera.main;
        var cameraHeight = 2f * camera.orthographicSize;
        var cameraWidth = cameraHeight * camera.aspect;
        Vector2 cameraPosition = camera.transform.position;

        CameraMinX = cameraPosition.x - cameraWidth / 2f;
        CameraMaxX = cameraPosition.x + cameraWidth / 2f;
        CameraMaxY = cameraPosition.y + cameraHeight / 2f;
    }
}