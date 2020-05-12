using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPosition {get; private set;}
    public bool IsEmpty { get; set;}
    public Vector2 WorldPosition {
        get{ return GetComponent<SpriteRenderer>().bounds.center;}
    }
    private Color32 greenColor = new Color32(255, 118, 118, 255);
    private Color32 redColor = new Color32(96, 255, 90, 255);
    private SpriteRenderer spriteRenderer;

        // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Point gridPos, Vector2 worldPos, Transform parent){
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);
        IsEmpty = true;
    }

    private void OnMouseOver(){
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null){
            if (IsEmpty){
                ColorTile(redColor);
            }  
            if (!IsEmpty){
                ColorTile(greenColor);
            } else if (Input.GetMouseButtonDown(0)){
                PlaceTower();
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower(){
        GameObject tower = Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.y;
        tower.transform.SetParent(transform);
        GameManager.Instance.BuyTower();
        IsEmpty = false;
        ColorTile(Color.white);
    }

    private void ColorTile(Color newColor){
        spriteRenderer.color = newColor;
    }

}
