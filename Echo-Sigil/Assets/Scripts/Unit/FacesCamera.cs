﻿using UnityEngine;

public class FacesCamera : MonoBehaviour
{
    public SpriteRenderer unitSprite;

    private void Start()
    {
        GamplayCamera.CameraMoved += FaceTarget;
    }

    private void FaceTarget(Vector2 target)
    {
        if (Selector.instance != null)
        {
            Vector2 forward = (Vector2)Selector.instance.transform.position - (Vector2)transform.position;
            if (forward != Vector2.zero)
            {
                unitSprite.transform.rotation = Quaternion.LookRotation(-forward, Vector3.forward);
            }
        }
    }
}