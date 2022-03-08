using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{

    public Vector3Int gridPosition;
    GameObject result;
    public Tile tileFox;
    public LayerMask layerMaskFox;

    public List<Vector3Int> atteignables = new List<Vector3Int>();
    public List<Vector3Int> visibles = new List<Vector3Int>();

    public Color visiblesColor;
    public Color ateignablesColor;
    public bool voisinsDisplayedFox = false;
    public int sightFox;
    public int vitesse = 1;

    public static Fox instance;

    void Awake()
    {
        if (instance !=null)
        {
            Debug.Log("Plus d'une instance de Fox dans la scène !!!");
        }
        instance = this;
    }




    

    void Start()
    {
        
        gridPosition = GridManager.instance.ConvertPositionToOffset(transform.position);
        transform.position = gridPosition;
        tileFox = GridManager.instance.GetTileAt(gridPosition);
        tileFox.hasFox = true;
        visibles = tileFox.GetVoisinsRec(sightFox,tileFox.offsetCoordinates);
        atteignables = tileFox.GetVoisinsRec(vitesse,tileFox.offsetCoordinates);
        atteignables.Add(tileFox.offsetCoordinates);


    }


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) )
        {
            if((FindTarget(Input.mousePosition, layerMaskFox) && voisinsDisplayedFox == false))
            {
                visibles = tileFox.GetVoisinsRec(sightFox,tileFox.offsetCoordinates);
                atteignables = tileFox.GetVoisinsRec(vitesse,tileFox.offsetCoordinates);
                atteignables.Add(tileFox.offsetCoordinates);
                DisplayVisible(visiblesColor);
                DisplayAtteignable(ateignablesColor);
                
            }
            else if (voisinsDisplayedFox == true) 
            {
                ResetAll();
                
            }
        }
    }

    public bool CheckIfPlayerInSight(out Tile result)
    {
        result = null;
        foreach(Vector3Int coord in visibles)
        {
            Tile tile = GridManager.instance.GetTileAt(coord);
            if (tile.hasCharacter == true)
            {
                result = tile;
                return true;
            }


        }
            return false;
    }


    public Tile FoxTarget(Tile tileTarget)
    {
        
        //calcul pour tous les points atteignables la distance au poulet
        float minDistance = Vector3.Distance(tileTarget.offsetCoordinates, atteignables[0]);
        Tile foxTargetTile = GridManager.instance.GetTileAt(atteignables[0]);

        foreach(Vector3Int coord in atteignables)
        {   Tile lookup = GridManager.instance.GetTileAt(coord);;
            int distance = (Mathf.Abs(coord.x-tileTarget.offsetCoordinates.x)+Mathf.Abs(coord.y-tileTarget.offsetCoordinates.y));
            if (distance <= minDistance && lookup.GetVoisins().Count>1 )
            {
                minDistance = distance;
                foxTargetTile = lookup;
            }
        }

        return foxTargetTile;


    }

    public void MoveFoxToTile(Tile target)
    {
        transform.position = target.offsetCoordinates;
        UpdateFox();
        
    }

    public void MoveFox()
    {
        Tile result;
 
        if (CheckIfPlayerInSight(out result))
        {
            Tile foxTargetTile = FoxTarget(result);
            MoveFoxToTile(foxTargetTile);

        }

    }


    public void UpdateFox()
    {
        tileFox.hasFox = false;
        gridPosition = GridManager.instance.ConvertPositionToOffset(transform.position);
        transform.position = gridPosition;
        tileFox = GridManager.instance.GetTileAt(gridPosition);
        tileFox.hasFox = true;
        visibles = tileFox.GetVoisinsRec(sightFox,tileFox.offsetCoordinates);
        atteignables = tileFox.GetVoisinsRec(vitesse,tileFox.offsetCoordinates);
        atteignables.Add(tileFox.offsetCoordinates);

    }

    public bool FindTarget(Vector3 mousePos, LayerMask layerMask)
    {
        GameObject clicked;
         
         if (Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePos), layerMask) != null)
         {
            Collider2D clicked_collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePos), layerMask);
             clicked = clicked_collider.gameObject;  
               if (clicked !=null)
         {
             return true;
         }
         }
        return false;
    }

     public bool FindTarget(Vector3 mousePos, LayerMask layerMask, out GameObject result)
    {
        GameObject clicked;
         result = null;
         if (Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePos), layerMask) != null)
         {
            Collider2D clicked_collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePos), layerMask);
             clicked = clicked_collider.gameObject;  
               if (clicked !=null)
         {
             result = clicked;
             return true;
         }
         }
        return false;
    }


    public void DisplayVisible(Color color)
    {
        foreach(Vector3Int coord in visibles)
        {
            Tile tile = GridManager.instance.GetTileAt(coord);
            tile.DisplayTile(color);
            //tile.isSelected = true;
            voisinsDisplayedFox = true;
        }
    }

     public void ResetAll()
    {
        foreach(Vector3Int coord in visibles)
        {
            Tile tile = GridManager.instance.GetTileAt(coord);
            tile.DisplayTile(tile.baseColor);
            voisinsDisplayedFox = false;
        }
    }
    
    public void DisplayAtteignable(Color color)
    {
        foreach(Vector3Int coord in atteignables)
        {
            Tile tile = GridManager.instance.GetTileAt(coord);
            tile.DisplayTile(color);
            //tile.isSelected = true;
            voisinsDisplayedFox = true;
        }
    }

    



}
