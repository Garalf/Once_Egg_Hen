using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Tile> linkedTiles;
    public Color colorBlocked;
    public Sprite spriteMur;
    public Animator animator;
    
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            foreach(Tile tile in linkedTiles)
            {
                tile.DisplayTile(tile.baseColor);
                tile.isBlocked=false;
            }
            animator.SetTrigger("isTrigger");
        }
        
    }
    void Start()
    {
        foreach(Tile tile in linkedTiles)
            {
                tile.DisplayTile(colorBlocked);
                tile.isBlocked = true;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
