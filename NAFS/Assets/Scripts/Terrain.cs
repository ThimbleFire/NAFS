using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Terrain : MonoBehaviour
{
    public Tilemap grassLayer;

    public TileBase tileBaseTilledEarth;
    public TileBase tileBaseDirt;

    public GameObject playerCharactersActiveTool;

    public void RemoveGrass()
    {
        grassLayer.SetTile(Cursor.Position, tileBaseDirt);
    }
    public void Till()
    {
        if (grassLayer.GetTile(Cursor.Position).Equals(tileBaseDirt))
            grassLayer.SetTile(Cursor.Position, tileBaseTilledEarth);
    }
    public void Sow()
    {
        //You need to make it so multiple game objects can't be placed on the same tile

        if (grassLayer.GetTile(Cursor.Position).Equals(tileBaseTilledEarth)) {
        // note: the reason why game object is out of position is due to the sprite origin being centered.
            CropMono seed = Instantiate(ResourceRepository.prefab["Seed"], grassLayer.CellToWorld(Cursor.Position), Quaternion.identity).GetComponent<CropMono>();
            seed.Setup(playerCharactersActiveTool.name);
        }
    }
}
