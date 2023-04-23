using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Feeling;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int LightCount;

    public int BattoryCount;

    [SerializeField] private TextMeshProUGUI _textBattory;

    public int SpikeTrapCount;

    public int MineTrapCount; 

    public bool Flashlight;

    public bool Pickaxe;

    [SerializeField] private GameObject _imageFlaslight;

    [SerializeField] private GameObject _imagePickaxe;

    public int MineTurretCount;
    public int AttackTurretCount;
    
    [SerializeField] private ParticleSystem _particleLvlUp;
    [SerializeField] private ScrollViewResourse _scrollViewResourse;

    private List<Collider2D> hitCollidersUse;
    [SerializeField] private float _radiusUse;
    [SerializeField] private LayerMask _use;
    [SerializeField] private Transform _usePoint;
    [SerializeField] private GameObject _deadMenu;
    [SerializeField] private Slider _destroySlider;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _updateTurretMenu;

    
    private Movement _movement;
    private Shooting _shooting;
    private Health _health;
    private Mining _mining;
    private Leveling _leveling;
    private Economic _economic;
    private UpgradablePerks _upgradablePerks;

    
    public Slider DestroySlider => _destroySlider;
    public Movement Movement => _movement;
    public Shooting Shooting => _shooting;
    public Health Health => _health;
    public Inventory Inventory => _inventory;
    public Mining Mining 
    {
        get{
            if(_mining == null)
                _mining = GetComponent<Mining>();

            return _mining;
        }
    }

    public Leveling Leveling 
    {
        get{
            if(_leveling == null)
                _leveling = GetComponent<Leveling>();

            return _leveling;
        }
    }

    public Economic Economic 
    {
        get{
            if(_economic == null)
                _economic = GetComponent<Economic>();

            return _economic;
        }
    }

    public UpgradablePerks UpgradablePerks => _upgradablePerks;

    #region PublicApi

    public void UpdateScrollView()
    {
        if (_scrollViewResourse.Index == 0)
            _scrollViewResourse.TextInventory.text = SpikeTrapCount.ToString();
        if (_scrollViewResourse.Index == 1)
            _scrollViewResourse.TextInventory.text = LightCount.ToString();
        if (_scrollViewResourse.Index == 2)
            _scrollViewResourse.TextInventory.text = AttackTurretCount.ToString();
        if (_scrollViewResourse.Index == 3)
            _scrollViewResourse.TextInventory.text = MineTurretCount.ToString();
    }

    public void ActivateFlashLight()
    {
        Flashlight = true;
        _imageFlaslight.gameObject.SetActive(true);
    }

    public void ActivatePickaxe()
    {
        Pickaxe = true;
        _imagePickaxe.gameObject.SetActive(true);
    }

    public void AddClip(int clip)
    {
        _shooting.AddClip(clip);
    }

    #endregion

    #region Methods

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _shooting = GetComponent<Shooting>();
        _health   = GetComponent<Health>();
        _mining   = GetComponent<Mining>();
        _leveling = GetComponent<Leveling>();
        _economic = GetComponent<Economic>();
        _upgradablePerks = GetComponent<UpgradablePerks>();


        _imageFlaslight.gameObject.SetActive(false);
        _imagePickaxe.gameObject.SetActive(false);

        UpdateUI();
        _health.Died += Health_OnDied;
        PlayerCash.Instance.Player = this;
        _destroySlider.gameObject.SetActive(false);
    }

    private ObjectGame _previusObject;
    private bool isOpened;
    private void Update()
    {
        hitCollidersUse = Physics2D.OverlapCircleAll(_usePoint.position, _radiusUse, _use).ToList();

        if(hitCollidersUse.Count > 0)
        {
            Collider2D collider = hitCollidersUse[0];
            if(collider.CompareTag("Object"))
            {
                Objects.Instance.ObjectsGame.RemoveAll(x => x == null);
                ObjectGame objectGame = Objects.Instance.ObjectsGame.Find(x => x.transform == collider.transform);
                objectGame.Active();
                _previusObject = objectGame;

                if(objectGame.TypeObject == GameObjects.AttackTurret || objectGame.TypeObject == GameObjects.MiningTurret)
                {
                    if(Input.GetKeyDown(KeyCode.X))
                    {
                        isOpened = !isOpened;
                        if(isOpened)
                            _updateTurretMenu.SetActive(true);
                        
                        else
                            _updateTurretMenu.SetActive(false);
                    }
                }
            }
            else if(collider.CompareTag("Loot") && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Подбираю");
                Loots.Instance.Items.RemoveAll(x => x == null);
                Item item = Loots.Instance.Items.Find(x => x.transform == collider.transform);
                _inventory.AddItem(item.ItemScriptableObject, item.Amount);
                Loots.Instance.Items.RemoveAll(x => x == null);
                Destroy(item.gameObject);

            }
        }

            
        if(_previusObject != null && hitCollidersUse.Find(x => x.transform == _previusObject.transform) == null)
        {
            _previusObject.NotActive();
            _destroySlider.gameObject.SetActive(false);
        }
        
    }

    private void OnDestroy() => _health.Died -= Health_OnDied;

    private void Health_OnDied() => StateDeadMenu(true);
    private void StateDeadMenu(bool state) => _deadMenu.SetActive(state);


    public void UpdateUI()
    {
        UpdateScrollView();
        UpdateBattory();        
    }

    public void RestartGame() => SceneManager.LoadScene("Game");
    


    public void UpdateBattory() => _textBattory.text = BattoryCount.ToString();
    
    public void StateShooting(bool state) => _shooting.enabled = state;

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_usePoint.position, _radiusUse);
    }

    #endregion
}