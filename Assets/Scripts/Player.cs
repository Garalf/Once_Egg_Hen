using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Vector3Int gridPosition;
    GameObject result;
    public Tile tileCharacter;
    public LayerMask layerMaskCharacter;
    public List<Vector3Int> voisinsCharacter = new List<Vector3Int>();

    public Color selectedColor;
    public bool voisinsDisplayed = false;

    public bool IsAdult= false;

    public Animator animator;
    public GameObject panelGO;

    public AudioSource audioSource;
    public AudioSource mainAudioSource;
    public AudioClip soundGO;
    

    void Start()
    {
        
        gridPosition = GridManager.instance.ConvertPositionToOffset(transform.position);
        transform.position = gridPosition;
        tileCharacter = GridManager.instance.GetTileAt(gridPosition);
        tileCharacter.hasCharacter = true;
        voisinsCharacter = tileCharacter.GetVoisins();

    }


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) )
        {
            if((FindTarget(Input.mousePosition, layerMaskCharacter) && voisinsDisplayed == false))
            {
                DisplayVoisins(selectedColor);
                
            }
            else 
            {
                if((FindTarget(Input.mousePosition,tileCharacter.layerMaskTile) && voisinsDisplayed == true))
                {
                    GameObject clicked;
                    FindTarget(Input.mousePosition,tileCharacter.layerMaskTile, out clicked);
                    if (clicked.GetComponent<Tile>() != null)
                    {
                        Tile targetTile = clicked.GetComponent<Tile>();
                        MoveCharacterToTile(targetTile);
                    }
                }
                ResetVoisins();
                
            }
        }
    }

    public void MoveCharacterToTile(Tile target)
    {
        if (target.isSelected) 
        {
            transform.position = target.offsetCoordinates;
            UpdateCharacter();
        }
        
    }
    public void CheckIfWin()
    {
        if (tileCharacter.tileType == "Win" && IsAdult)
        {

            StartCoroutine(LoadNext());
        }
        
    
    }

    public IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void CheckGrain()
    {
        if (tileCharacter.tileType == "Grain" && tileCharacter.item.activeSelf == true)
        {
            IsAdult = true;
            tileCharacter.item.SetActive(false);
            print("Miom");
            animator.SetTrigger("adulte");

        }
    
    }

    public bool CheckIfKilled()
    {
        return(tileCharacter.hasFox);
    }


    public void UpdateCharacter()
    {
        ResetVoisins();
        gridPosition = GridManager.instance.ConvertPositionToOffset(transform.position);
        transform.position = gridPosition;
        tileCharacter.hasCharacter = false;
        tileCharacter = GridManager.instance.GetTileAt(gridPosition);
        tileCharacter.hasCharacter = true;
        voisinsCharacter = tileCharacter.GetVoisins();
        if(!CheckIfKilled())
        {
            CheckGrain();
            CheckIfWin();
            Fox.instance.MoveFox();
            if (CheckIfKilled())
                {
                    ReloadScene();
                }
        }
        else
        {
            ReloadScene();
            print("tmort");
        }
        
    }

    public void ReloadScene()
    {
        mainAudioSource.gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(soundGO,transform.position);
        panelGO.SetActive(true);

        
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


    public void DisplayVoisins(Color color)
    {
        foreach(Vector3Int coord in voisinsCharacter)
        {
            Tile tile = GridManager.instance.GetTileAt(coord);
            tile.DisplayTile(color);
            tile.isSelected = true;
            voisinsDisplayed = true;
        }
    }

    public void ResetVoisins()
    {
        foreach(Vector3Int coord in voisinsCharacter)
        {
            Tile tile = GridManager.instance.GetTileAt(coord);
            tile.DisplayTile(tile.baseColor);
            tile.isSelected = false;
            voisinsDisplayed = false;
        }
    }




}
