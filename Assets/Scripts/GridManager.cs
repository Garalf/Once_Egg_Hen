
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GridManager : MonoBehaviour
{

    Dictionary<Vector3Int,Tile> tileDict = new Dictionary<Vector3Int, Tile>();
    Dictionary<Vector3Int,List<Vector3Int>> tileNeighbDict = new Dictionary<Vector3Int,List<Vector3Int>>();

    public static GridManager instance;

    void Awake()
    {
  
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tile.offsetCoordinates = ConvertPositionToOffset(tile.gameObject.transform.position);
            tileDict[tile.offsetCoordinates] = tile;
        }

              instance = this;

    }


        public Vector3Int ConvertPositionToOffset(Vector3 position)
    {

        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        int z = Mathf.RoundToInt(position.z);

        return new Vector3Int(x,y,z);
    }

    public Tile GetTileAt(Vector3Int TileCoordinates)
    {
        Tile result = null;

        tileDict.TryGetValue(TileCoordinates,out result);
        
        return result;
    }

    public List<Vector3Int> GetNeighbFor(Vector3Int TileCoordinates)
    {
        if (tileDict.ContainsKey(TileCoordinates) == false)
        {
            return new List<Vector3Int>();
        }

        if (tileNeighbDict.ContainsKey(TileCoordinates))
        {
            return tileNeighbDict[TileCoordinates];
        }

        tileNeighbDict.Add(TileCoordinates, new List<Vector3Int>());

        List<Vector3Int> direction = new List<Vector3Int>{
            new Vector3Int(1,0,0),
            new Vector3Int(-1,0,0),
            new Vector3Int(0,1,0),
            new Vector3Int(0,-1,0),
        
        };

        foreach(Vector3Int dir in direction)

        {
           if (tileDict.ContainsKey(TileCoordinates + dir))
           {
                tileNeighbDict[TileCoordinates].Add(TileCoordinates+dir);
           }
        }

        return tileNeighbDict[TileCoordinates];

    }


}
