using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Info Screen")] [Space(5)] public int LightCount;

    [Space(5)] public int BattoryCount;

    [SerializeField] private TextMeshProUGUI _textBattory;

    [Space(5)] public int ShipCount;

    [Space(5)] public bool Flashlight;

    [SerializeField] private GameObject _imageFlaslight;

    [Space(5)] public bool Pickaxe;

    [SerializeField] private GameObject _imagePickaxe;

    [Space(5)] public int MineTurretCount;
    public int AttackTurretCount;
    
    [SerializeField] private ParticleSystem _particleLvlUp;
    [SerializeField] private ScrollViewResourse _scrollViewResourse;

    private Movement _movement;
    private Shooting _shooting;
    private Health _health;
    private Mining _mining;
    private Leveling _leveling;
    private Economic _economic;

    public Movement Movement => _movement;
    public Health Health => _health;
    public Mining Mining => _mining;
    public Leveling Leveling => _leveling;
    public Economic Economic => _economic;


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

    public int ActivateFlashLight()
    {
        Flashlight = true;
        _imageFlaslight.gameObject.SetActive(true);
        return 0;
    }

    public int ActivatePickaxe()
    {
        Pickaxe = true;
        _imagePickaxe.gameObject.SetActive(true);
        return 0;
    }

    public int AddClip(int clip)
    {
        _shooting.AddClip(clip);
        return 0;
    }

    #endregion

    #region Methods

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _shooting = GetComponent<Shooting>();
        _health = GetComponent<Health>();
        _mining = GetComponent<Mining>();
        _leveling = GetComponent<Leveling>();

        _imageFlaslight.gameObject.SetActive(false);
        _imagePickaxe.gameObject.SetActive(false);

        _health.Died += Health_OnDied;

        UpdateUI();
    }

    private void OnDestroy()
    {
        _health.Died -= Health_OnDied;
    }

    private void Health_OnDied()
    {
        RestartGame();
    }

    public void UpdateUI()
    {
        UpdateAmmo();
        UpdateBattory();
        _shooting.UpdateClip();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateAmmo() => _shooting.UpdateAmmo();

    public void UpdateBattory() => _textBattory.text = BattoryCount.ToString();
    public void StateShooting(bool state) => _shooting.enabled = state;

    #endregion
}