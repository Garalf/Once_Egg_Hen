using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public SpriteRenderer spriteR;
    public bool hasCharacter = false;
    public string tileType;

    public Color baseColor;
    public bool isSelected = false;

    public bool hasFox = false;

    public LayerMask layerMaskTile;

    public GameObject item;

    public bool isBlocked;


   

    public Vector3Int offsetCoordinates;

    void Awake()

    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        baseColor = spriteR.color;
    }

    void Start()
    {
        offsetCoordinates = GridManager.instance.ConvertPositionToOffset(transform.position);
    }



    public List<Vector3Int> GetVoisins()
    {
        
        List<Vector3Int> voisins = new List<Vector3Int>();
        foreach( Vector3Int coord in GridManager.instance.GetNeighbFor(offsetCoordinates))
        {
            if (!(GridManager.instance.GetTileAt(coord).isBlocked))
            {
                voisins.Add(coord);
            }
        }
        return(voisins);
    }

    public List<Vector3Int> GetVoisins(Vector3Int Coordinates)
    {
        List<Vector3Int> voisins = new List<Vector3Int>();
        foreach( Vector3Int coord in GridManager.instance.GetNeighbFor(Coordinates))
        {
            if (!(GridManager.instance.GetTileAt(coord).isBlocked))
            {
                voisins.Add(coord);
            }
        }
        return(voisins);

    }


    public List<Vector3Int> GetVoisinsRec(int depth ,Vector3Int coordinates)
    {   
        List<Vector3Int> newVoisins1 = new List<Vector3Int>();
        newVoisins1.Add(coordinates);
        List<Vector3Int> newVoisins2 = new List<Vector3Int>();
         List<Vector3Int> newVoisins3 = new List<Vector3Int>();
        List<Vector3Int> allVoisins = new List<Vector3Int>();
        allVoisins.Add(coordinates);
        for(int i = 0; i<depth;i++)
        {
            foreach(Vector3Int coord in newVoisins1)
            {
                allVoisins = GetVoisins(coord);
                foreach(Vector3Int coord2 in allVoisins)
                {
                    if (!newVoisins2.Contains(coord2))
                    {
                        newVoisins2.Add(coord2);
                        newVoisins3.Add(coord2);
                    }
                }
            }
            newVoisins1 = newVoisins3;
            newVoisins3 = new List<Vector3Int>();
        }
        return newVoisins2;
        
    }

    


    public void DisplayTile(Color colorSelected)
    {
        spriteR.color = colorSelected;
    }
    


}
