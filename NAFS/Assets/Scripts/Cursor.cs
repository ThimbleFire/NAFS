using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public static Vector3Int Position { get; set; } = Vector3Int.zero;
    private Vector3Int lastTile = Vector3Int.zero;
    public Grid grid;
    private static SpriteRenderer renderer;

    private void Awake()
    {
        PlayerMove.OnTileChange += PlayerMove_OnTileChange;
        PlayerMove.OnDirectionChange += PlayerMove_OnDirectionChange;

        renderer = GetComponent<SpriteRenderer>();
    }

    private void PlayerMove_OnDirectionChange(Vector3 direction)
    {
        Vector3 offset = new Vector3(0.08f, 0.08f);

        Vector3Int tile = grid.WorldToCell(PlayerCharacter.WorldPosition + direction * 0.16f);

        transform.position = grid.CellToWorld(tile) + offset;
    }
    private void PlayerMove_OnTileChange(Vector3Int newTile)
    {
        Vector3 offset = new Vector3(0.08f, 0.08f);

        Vector3Int dir = newTile - lastTile;

        Position = newTile + dir;
        transform.position = grid.CellToWorld(Position) + offset;
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
