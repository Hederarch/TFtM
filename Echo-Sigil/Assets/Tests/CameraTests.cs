﻿using NUnit.Framework;
using UnityEngine;
using System;

namespace Camera_Tests
{
    class rotation
    {
        [Test]
        public void offset_from_Z0_consitent()
        {
            GamplayCamera camera = new GameObject("Test Camera").AddComponent<GamplayCamera>();
            for (float angle = 0; angle < Mathf.PI * 2f; angle += Mathf.PI / 4f)
            {
                camera.transform.position = GamplayCamera.CalcPostion(Vector3.zero, angle, 4, 4);
                Assert.AreEqual(-4, camera.transform.position.z, " Angle was " + (angle * Mathf.Rad2Deg));
            }
        }
        [Test]
        public void offset_from_foucus_consitent()
        {
            GamplayCamera camera = new GameObject("Test Camera").AddComponent<GamplayCamera>();
            for (float angle = 0; angle < Mathf.PI * 2f; angle += Mathf.PI / 4f)
            {
                camera.transform.position = GamplayCamera.CalcPostion(Vector3.zero, angle, 4, 4);
                Assert.AreEqual(4, Vector3.Distance(Vector3.zero, (Vector2)camera.transform.position), " Angle was " + (angle * Mathf.Rad2Deg));
            }
        }
    }
    class selection
    {

    }
}
