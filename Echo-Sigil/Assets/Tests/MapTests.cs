﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Map_Tests
{
    class map_reader
    {
       [Test]
       public void genarates_map_with_tile_with_hieght()
        {
            Tile[,] tiles = new Tile[1, 1];
            Tile tile = new Tile();
            tile.height = 2;
            tiles[0, 0] = tile;
            Tile[,] outTiles = MapReader.GeneratePhysicalMap(new Map(tiles));
            Assert.AreEqual(2, outTiles[0,0].height);
        }
        [Test]
        public void genarates_map_with_tile()
        {
            Tile[,] tiles = new Tile[1, 1];
            Tile tile = new Tile();
            tile.height = 2;
            tiles[0, 0] = tile;
            Tile[,] outTiles = MapReader.GeneratePhysicalMap(new Map(tiles));
            Assert.IsNotNull(outTiles[0, 0]);
        }
        [Test]
        public void genarates_map_with_unit()
        {
            MapReader.GeneratePhysicalMap(new Map(1,1,true));
            Assert.IsNotNull(MapReader.implements[0]);
        }
        [Test]
        public void genarates_map_with_nonNull_unit_array()
        {
            MapReader.GeneratePhysicalMap(new Map(1, 1, true));
            Assert.IsNotNull(MapReader.implements);
        }
        [Test]
        public void genarates_map_with_nonNull_tile_array()
        {
            Tile[,] outTiles = MapReader.GeneratePhysicalMap(new Map(1,1));
            Assert.IsNotNull(outTiles);
        }
        [Test]
        public void tile_parent_has_child()
        {
            MapReader.GeneratePhysicalMap(new Map(1, 1));
            Assert.AreEqual(1, MapReader.tileParent.childCount);
        }
        [Test]
        public void generates_tile_array_of_map_size()
        {
            Vector2Int size = new Vector2Int(3, 5);
            Tile[,] tiles = MapReader.GeneratePhysicalMap(new Map(size));
            Assert.AreEqual(size, new Vector2Int(tiles.GetLength(0), tiles.GetLength(1)));
        }
        [Test]
        public void returns_mapReader_tiles()
        {
            Tile[,] tiles = MapReader.GeneratePhysicalMap(new Map(1,1));
            Assert.AreEqual(MapReader.tiles, tiles);
        }
    }
    class mapeditor
    {

    }
    class map
    {
        [Test]
        public void map_can_not_return_null_1x1()
        {
            Map map = new Map(1, 1);
            Assert.IsNotNull(map);
        }
        [Test]
        public void map_can_not_return_null_3x5()
        {
            Map map = new Map(3, 5);
            Assert.IsNotNull(map);
        }
        [Test]
        public void new_maps_is_walkable()
        {
            Map map = new Map(1, 1);
            Tile tile = map.SetTileProperties(0,0);
            Assert.IsTrue(tile.walkable);
        }
        [Test]
        public void set_tile_properties_does_not_return_null_at_0x0()
        {
            Map map = new Map(1, 1);
            Assert.IsNotNull(map.SetTileProperties(0, 0));
        }
        [Test]
        public void set_tile_properties_does_not_return_null_at_1x1()
        {
            Map map = new Map(2, 2);
            Assert.IsNotNull(map.SetTileProperties(1, 1));
        }
        [Test]
        public void new_map_has_heigtmap_data()
        {
            Map map = new Map(1, 1);
            Assert.IsNotNull(map.heightMap[0,0]);
        }
        [Test]
        public void new_map_sets_size()
        {
            Map map = new Map(3, 5);
            Assert.AreEqual(new Vector2Int(3, 5), new Vector2Int(map.sizeX,map.sizeY));
        }
        [Test]
        public void new_map_overloads_equivalant()
        {
            Map intmap = new Map(3, 5);
            Map vecmap = new Map(new Vector2Int(3, 5));
            Assert.AreEqual(new Vector2Int(vecmap.sizeX,vecmap.sizeY), new Vector2Int(intmap.sizeX, intmap.sizeY));
        }
        [Test]
        public void height_map_is_map_sizex_1x1()
        {
            HeightMapIsSize(1, 1, true);
        }
        [Test]
        public void height_map_is_map_sizey_1x1()
        {
            HeightMapIsSize(1, 1, false);
        }
        [Test]
        public void height_map_is_map_sizex_3x5()
        {
            HeightMapIsSize(3, 5, true);
        }
        [Test]
        public void height_map_is_map_sizey_3x5()
        {
            HeightMapIsSize(3, 5, false);
        }
        public void HeightMapIsSize(int x, int y, bool isXAxis)
        {
            Map map = new Map(x, y);
            if (isXAxis)
            {
                Assert.AreEqual(map.sizeX, map.heightMap.GetLength(0));
            }
            else
            {
                Assert.AreEqual(map.sizeY, map.heightMap.GetLength(1));
            }
        }
        [Test]
        public void new_map_units_is_nonNull()
        {
            Map map = new Map(1, 1, true);
            Assert.IsNotNull(map.units);
        }
    }
    class save_system
    {
        [Test]
        public void get_map_from_file_with_size()
        {
            Map savedMap = new Map(1, 1);
            SaveSystem.SaveMap("UnitTest_get_map_from_file", savedMap);
            Map loadMap = SaveSystem.LoadMap("UnitTest_get_map_from_file");
            Assert.AreEqual(new Vector2Int(savedMap.sizeX,savedMap.sizeY), new Vector2Int(loadMap.sizeX, loadMap.sizeY));
            SaveSystem.DeleteMap("UnitTest_get_map_from_file");
        }
        [Test]
        public void get_map_from_file_with_tile_with_height()
        {
            Tile[,] tiles = new Tile[1, 1];
            Tile tile = new Tile();
            tile.height = 2;
            tiles[0, 0] = tile;
            Map savedMap = new Map(tiles);
            SaveSystem.SaveMap("UnitTest_get_map_from_file", savedMap);
            Map loadMap = SaveSystem.LoadMap("UnitTest_get_map_from_file");
            Assert.AreEqual(2, loadMap.SetTileProperties(0,0).height);
            SaveSystem.DeleteMap("UnitTest_get_map_from_file");
        }
        [Test]
        public void get_map_from_file_with_unit_with_name()
        {
            MapReader.GeneratePhysicalMap(new Map(1, 1, true));
            MapReader.implements[0].name = "Jhon";
            Map savedMap = new Map(MapReader.tiles, MapReader.implements.ToArray());
            SaveSystem.SaveMap("UnitTest_get_map_from_file", savedMap);
            Map loadMap = SaveSystem.LoadMap("UnitTest_get_map_from_file");
            Assert.AreEqual("Jhon", loadMap.units[0].name);
            SaveSystem.DeleteMap("UnitTest_get_map_from_file");
        }
    }
    class conversions
    {
        [Test]
        public void center_tile_is_at_0x0_1x1()
        {
            MapReader.GeneratePhysicalMap(new Map(1, 1));
            Transform tileTransform = MapReader.tileParent.GetChild(0);
            Assert.AreEqual(Vector3.zero, tileTransform.position);
        }
        [Test]
        public void center_tile_is_at_0x0_3x3()
        {
            MapReader.GeneratePhysicalMap(new Map(3, 3));
            Transform tileTransform = MapReader.tileParent.Find("1,1 tile");
            Assert.AreEqual(Vector3.zero, tileTransform.position);
        }
        [Test]
        public void grid_to_world_space_3x3()
        {
            MapReader.GeneratePhysicalMap(new Map(3, 3));
            Assert.AreEqual(Vector2.one, MapReader.GridToWorldSpace(Vector2Int.zero));
        }
        [Test]
        public void world_to_grid_space_3x3()
        {
            MapReader.GeneratePhysicalMap(new Map(3, 3));
            Assert.AreEqual(Vector2Int.zero, MapReader.WorldToGridSpace(Vector2.one));
        }
    }
}

