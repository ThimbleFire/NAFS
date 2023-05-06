using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SpriteRenderer))]
public class Cursor : MonoBehaviour
{
    private readonly Vector3 tileCenterOffset = Vector3.one * Tile.HalfSize;
    public static Vector3Int Position { get; set; } = Vector3Int.zero;
    public static Vector3 WorldPosition
    {
        get
        {
            Vector3 cursorWorldPosition =
            PlayerCharacter.WorldPosition +
            PlayerCharacter.FacingPosition * Tile.Size;

            return new Vector3(
                Mathf.Floor(cursorWorldPosition.x / Tile.Size) * Tile.Size,
                Mathf.Floor(cursorWorldPosition.y / Tile.Size) * Tile.Size);
        }
    }
    private Vector3Int lastTile = Vector3Int.zero;
    public Grid grid;
    private static SpriteRenderer renderer;

    private void Awake()
    {
        PlayerMove.OnTileChange += PlayerMove_OnTileChange;
        PlayerMove.OnDirectionChange += PlayerMove_OnDirectionChange;

        renderer = GetComponent<SpriteRenderer>();
    }

    /// Update the cursor position when changing direction on the same tile
    private void PlayerMove_OnDirectionChange(Vector3 movementDirection) {
        
        Position = grid.WorldToCell(Cursor.WorldPosition + tileCenterOffset);
        transform.position = Cursor.WorldPosition + tileCenterOffset;
    }

    /// Update the cursor position when changing tile
    private void PlayerMove_OnTileChange(Vector3Int newTile) {
        Vector3Int dir = newTile - lastTile;
        Position = newTile + dir;
        transform.position = grid.CellToWorld(Position) + tileCenterOffset;
        lastTile = newTile;
    }

    public static void Show()
    {
        renderer.enabled = true;
    }

    public static void Hide()
    {
        renderer.enabled = false;
    }
}
