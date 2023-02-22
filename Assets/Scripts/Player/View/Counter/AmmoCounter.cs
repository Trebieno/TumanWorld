using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private Shooting _shooting;
    [SerializeField] private TMP_Text _textAmmo;

    private void Awake()
    {
        _textAmmo = GetComponent<TMP_Text>();
        _shooting.AmmoChanged += Shooting_OnAmmoChanged;
        _textAmmo.text = $"{_shooting.CurrentAmmo} / {_shooting.MaximumAmmo}";
    }

    private void OnDestroy()
    {
        _shooting.AmmoChanged -= Shooting_OnAmmoChanged;
    }

    private void Shooting_OnAmmoChanged()
    {
        _textAmmo.text = $"{_shooting.CurrentAmmo} / {_shooting.MaximumAmmo}";
    }
}