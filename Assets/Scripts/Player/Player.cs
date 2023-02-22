using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public int LightCount;

    public int BattoryCount;

    [SerializeField] private TextMeshProUGUI _textBattory;

    public int ShipCount;

    public bool Flashlight;

    [SerializeField] private GameObject _imageFlaslight;

    public bool Pickaxe;

    [SerializeField] private GameObject _imagePickaxe;

    public int MineTurretCount;
    public int AttackTurretCount;
    
    [SerializeField] private ParticleSystem _particleLvlUp;
    [SerializeField] private ScrollViewResourse _scrollViewResourse;

    
    private Movement _movement;
    private Shooting _shooting;
    private Health _health;
    private Mining _mining;
    private Leveling _leveling;
    private Economic _economic;
    private UpgradablePerks _upgradablePerks;

    public Movement Movement => _movement;
    public Shooting Shooting => _shooting;
    public Health Health => _health;
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
            _scrollViewResourse.TextInventory.text = ShipCount.ToString();
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
    }

    private void OnDestroy() => _health.Died -= Health_OnDied;

    private void Health_OnDied() => RestartGame();


    public void UpdateUI()
    {
        UpdateScrollView();
        UpdateBattory();        
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateBattory() => _textBattory.text = BattoryCount.ToString();
    
    public void StateShooting(bool state) => _shooting.enabled = state;

    #endregion
}