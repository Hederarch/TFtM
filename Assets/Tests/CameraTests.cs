﻿using NUnit.Framework;
using UnityEngine;
using TileMap;

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
                Assert.AreEqual(4, camera.transform.position.z, " Angle was " + (angle * Mathf.Rad2Deg));
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
        [Test]
        public void angle_sign_bigger_returns_minus()
        {
            Assert.IsFalse(Angle.Sign(Mathf.PI, Mathf.PI / 2f));
        }
        [Test]
        public void angle_sign_bigger_returns_plus()
        {
            Assert.IsTrue(Angle.Sign(Mathf.PI + (Mathf.PI / 2f), 0));
        }
        [Test]
        public void angle_sign_smaller_returns_minus()
        {
            Assert.IsFalse(Angle.Sign(0, Mathf.PI + (Mathf.PI / 2f)));
        }
        [Test]
        public void angle_sign_smaller_returns_plus()
        {
            Assert.IsTrue(Angle.Sign(Mathf.PI / 2f, Mathf.PI));
        }
    }
    class selection
    {
        [Test]
        public void center_cursor_selects_single_tile()
        {
            InitalCursorSetup(new Map(1, 1));
            Cursor.GetCursor(true);
            Assert.AreEqual(MapReader.GetTile(0, 0, 0, 1), Cursor.Tile);
        }
        [Test]
        public void center_cursor_selects_center_tile()
        {
            InitalCursorSetup(new Map(3, 3));
            Cursor.GetCursor(true);
            Assert.AreEqual(MapReader.GetTile(1, 1, 0, 1), Cursor.Tile);
        }
        [Test]
        public void center_cursor_at_0_0_posInWorld()
        {
            InitalCursorSetup(new Map(3, 3));
            Cursor.GetCursor(true);
            Assert.AreEqual(Vector3.forward, Cursor.posInWorld);
        }
        [Test]
        public void center_cursor_at_1_1_posInGrid()
        {
            InitalCursorSetup(new Map(3, 3));
            Cursor.GetCursor(true);
            Assert.AreEqual(new TilePos(1,1,1), Cursor.posInGrid);
        }
        [Test]
        public void center_cursor_posInGrid_matches_tile()
        {
            InitalCursorSetup(new Map(3, 3));
            Cursor.GetCursor(true);
            Assert.AreEqual(Cursor.Tile.posInGrid, Cursor.posInGrid);
        }

        private static void InitalCursorSetup(Map map)
        {
            MapReader.GeneratePhysicalMap(map);
            GamplayCamera gamplayCamera = new GameObject("Camera", typeof(GamplayCamera)).GetComponent<GamplayCamera>();
            gamplayCamera.Start();
            gamplayCamera.transform.position = GamplayCamera.CalcPostion(Vector3.forward, 0, 4, 4);
            gamplayCamera.transform.rotation = Quaternion.LookRotation(Vector3.forward - gamplayCamera.transform.position, Vector3.forward);
        }
    }
}