using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Terrain : MonoBehaviour
{
    public Tilemap grassLayer;

    public TileBase tileBaseTilledEarth;
    public TileBase tileBaseDirt;

    public void RemoveGrass()
    {
        grassLayer.SetTile(Cursor.Position, tileBaseDirt);
    }
    public void Till()
    {
        if (grassLayer.GetTile(Cursor.Position).Equals(tileBaseDirt))
            grassLayer.SetTile(Cursor.Position, tileBaseTilledEarth);
    }
}
