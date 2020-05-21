 using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

    private int currency;
    [SerializeField]
    private Text currencyText;

    private ObjectPool pool;

    public ObjectPool Pool {
        get{ return pool;
        }
        set{
            this.pool = value;
            }
    }

    public TowerBtn ClickedBtn { get; set;}
    public int Currency {
        get{ return currency;
        }
        set{
            this.currency = value;
            this.currencyText.text = value.ToString() + " <color=lime>$</color>";
            }
    }

    private void Awake(){
        Pool = GetComponent<ObjectPool>();
    }

    void Start()
    {
        Currency = 5;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerBtn towerBtn){
        if (Currency >= towerBtn.Price){
        this.ClickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
        }
    }

    public void BuyTower(){
        if (Currency >= ClickedBtn.Price){
            Currency -= ClickedBtn.Price;
            Hover.Instance.Deactivate();
        } 
    }

    private void HandleEscape(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave(){
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int monsterIndex = Random.Range(0, 4);

        string type = string.Empty;
        switch (monsterIndex){
            case 1: {type = "BlueMonster"; break;}
            case 2: {type = "RedMonster"; break;}
            case 3: {type = "PurpleMonster"; break;}
            case 4: {type = "GreenMonster"; break;}
        }
        Monster monster = pool.GetObject(type).GetComponent<Monster>();
        monster.Spawn();
        yield return new WaitForSeconds(2.5f);  
    }
    
}
