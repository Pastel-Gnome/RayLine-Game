using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TileType;

public class DualGridTilemap : MonoBehaviour
{
	protected static Vector3Int[] NEIGHBOURS = new Vector3Int[] {
		new Vector3Int(0, 0, 0),
		new Vector3Int(1, 0, 0),
		new Vector3Int(0, 1, 0),
		new Vector3Int(1, 1, 0)
	};

	protected static Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> neighbourTupleToTile;

	// Provide references to each tilemap in the inspector
	public Tilemap placeholderTilemap;
	public Tilemap displayTilemap;

	// Provide the placeholder tile for the desired ground type in the inspector
	public Tile groundPlaceholderTile;

	// Provide the 16 tiles in the inspector
	public Tile[] tiles;

	void Start()
	{
		// This dictionary stores the "rules", each 4-neighbour configuration corresponds to a tile
		// |_1_|_2_|
		// |_3_|_4_|
		neighbourTupleToTile = new() {
			{new (On, On, On, On), tiles[6]},
			{new (Off, Off, Off, On), tiles[12]}, // OUTER_BOTTOM_RIGHT
            {new (Off, Off, On, Off), tiles[0]}, // OUTER_BOTTOM_LEFT
            {new (Off, On, Off, Off), tiles[8]}, // OUTER_TOP_RIGHT
            {new (On, Off, Off, Off), tiles[14]}, // OUTER_TOP_LEFT
            {new (Off, On, Off, On), tiles[1]}, // EDGE_RIGHT
            {new (On, Off, On, Off), tiles[11]}, // EDGE_LEFT
            {new (Off, Off, On, On), tiles[3]}, // EDGE_BOTTOM
            {new (On, On, Off, Off), tiles[9]}, // EDGE_TOP
            {new (Off, On, On, On), tiles[5]}, // INNER_BOTTOM_RIGHT
            {new (On, Off, On, On), tiles[2]}, // INNER_BOTTOM_LEFT
            {new (On, On, Off, On), tiles[10]}, // INNER_TOP_RIGHT
            {new (On, On, On, Off), tiles[7]}, // INNER_TOP_LEFT
            {new (Off, On, On, Off), tiles[13]}, // DUAL_UP_RIGHT
            {new (On, Off, Off, On), tiles[4]}, // DUAL_DOWN_RIGHT
			{new (Off, Off, Off, Off), null},
		};
		RefreshDisplayTilemap();
	}

	public void SetCell(Vector3Int coords, Tile tile)
	{
		placeholderTilemap.SetTile(coords, tile);
		setDisplayTile(coords);
	}

	private TileType getPlaceholderTileTypeAt(Vector3Int coords)
	{
		if (placeholderTilemap.GetTile(coords) == groundPlaceholderTile)
			return On;
		else
			return Off;
	}

	protected Tile calculateDisplayTile(Vector3Int coords)
	{
		// 4 neighbours
		TileType topRight = getPlaceholderTileTypeAt(coords - NEIGHBOURS[0]);
		TileType topLeft = getPlaceholderTileTypeAt(coords - NEIGHBOURS[1]);
		TileType botRight = getPlaceholderTileTypeAt(coords - NEIGHBOURS[2]);
		TileType botLeft = getPlaceholderTileTypeAt(coords - NEIGHBOURS[3]);

		Tuple<TileType, TileType, TileType, TileType> neighbourTuple = new(topLeft, topRight, botLeft, botRight);
		Tile returnedTile = neighbourTupleToTile[neighbourTuple];
        if (returnedTile == tiles[6]) // random chance to switch between variations of full tile
		{
			if (UnityEngine.Random.Range(0f,1f) > 0.5f) { returnedTile = tiles[15]; }
		}

        return returnedTile;
	}

	protected void setDisplayTile(Vector3Int pos)
	{
		for (int i = 0; i < NEIGHBOURS.Length; i++)
		{
			Vector3Int newPos = pos + NEIGHBOURS[i];
			displayTilemap.SetTile(newPos, calculateDisplayTile(newPos));
		}
	}

	// The tiles on the display tilemap will recalculate themselves based on the placeholder tilemap
	public void RefreshDisplayTilemap()
	{
		for (int i = -50; i < 50; i++)
		{
			for (int j = -50; j < 50; j++)
			{
				setDisplayTile(new Vector3Int(i, j, 0));
			}
		}
	}
}

public enum TileType
{
	On,
	Off
}
