using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    private int currency;
    [SerializeField]
    private Text currencyText;

    public TowerBtn ClickedBtn { get; set;}
    public int Currency {
        get{ return currency;
        }
        set{
            this.currency = value;
            this.currencyText.text = value.ToString() + " <color=lime>$</color>";
            }
    }

    void Start()
    {
        Currency = 100;
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
}
