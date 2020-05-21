using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]    
    private CameraMovement cameraMovement;

    [SerializeField]
    private Transform map;

    public float tileSize{
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;}
    }

    public Dictionary<Point, TileScript> Tiles {get; set;}

    public Portal BluePortal {get; set;}

    private Point bluePortalSpawn, redPortalSpawn;

    [SerializeField]
    private GameObject bluePortalPrefab, redPortalPrefab;

    // Start is called before the first frame update
    void Start()
    {
       CreateLevel(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel(){

        Tiles = new Dictionary<Point, TileScript>();
        
        string[] mapData = ReadLevelText();
        Vector2 worldStart = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;
        Vector3 maxTile = Vector3.zero;
        for(int y = 0; y < mapYSize; y++){
            char[] newTiles = mapData[y].ToCharArray();
            for(int x = 0; x < mapXSize; x++){
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }
        maxTile = Tiles[new Point(mapXSize-1, mapYSize-1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x + tileSize, maxTile.y - tileSize));
        SpawnPortals();
    }

    private void PlaceTile(string tileType, int x, int y, Vector2 worldStart){
        int tileIndex = int.Parse(tileType);
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
        newTile.Setup(new Point(x,y), new Vector2(worldStart.x + (tileSize * x), worldStart.y - (tileSize * y)), map);
    }

    private string[] ReadLevelText(){
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split('-');
    }

    private void SpawnPortals(){
        bluePortalSpawn = new Point(0,1);
        GameObject tmp = (GameObject)Instantiate(bluePortalPrefab, Tiles[bluePortalSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        BluePortal = tmp.GetComponent<Portal>();
        BluePortal.name = "BluePortal";
        redPortalSpawn = new Point(20,7);
        Instantiate(redPortalPrefab, Tiles[redPortalSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }
}
