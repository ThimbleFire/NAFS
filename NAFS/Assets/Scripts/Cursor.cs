using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cursor : MonoBehaviour
{
    public static Tilemap tilemapGrass;
    public static Tilemap tileMapDirt;

    public static Vector3Int Position { get; set; } = Vector3Int.zero;
    private Vector3Int lastTile = Vector3Int.zero;
    public Grid grid;
    private static SpriteRenderer renderer;

    private void Awake()
    {
        PlayerMove.OnTileChange += PlayerMove_OnTileChange;
        PlayerMove.OnDirectionChange += PlayerMove_OnDirectionChange;

        tilemapGrass = GameObject.Find("Dynamic Grass Layer").GetComponent<Tilemap>();
        tileMapDirt = GameObject.Find("Dynamic Dirt Layer").GetComponent<Tilemap>();

        renderer = GetComponent<SpriteRenderer>();
    }

    private void PlayerMove_OnDirectionChange(Vector3 direction)
    {
        Vector3 tileCenterOffset = Vector3.One * 0.08f;
        Vector3 cursorWorldPosition = PlayerCharacter.WorldPosition + direction * 0.16f;

        Position = grid.WorldToCell(cursorWorldPosition);
        cursorWorldPosition = Position / 0.16f;

        transform.position = cursorWorldPosition + tileCenterOffset;
    }
    private void PlayerMove_OnTileChange(Vector3Int newTile)
    {
        Vector3 tileCenterOffset = Vector3.One * 0.08f;

        Vector3Int dir = newTile - lastTile;

        Position = newTile + dir;
        transform.position = grid.CellToWorld(Position) + tileCenterOffset;
        lastTile = newTile;
    }

    public static void Show()
    {
        renderer.enabled = true;
    }

    public static void RemoveGrass()
    {
        tilemapGrass.SetTile(Position, null);
    }

    public static void Hide()
    {
        renderer.enabled = false;
    }
}
