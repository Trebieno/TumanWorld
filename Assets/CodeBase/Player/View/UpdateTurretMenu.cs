using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTurretMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _speedTurnText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _countScoreText;
    [SerializeField] private TMP_Text _powerText;
    [SerializeField] private TMP_Text _radiusText;
    [SerializeField] private TMP_Text _oreCell;
    [SerializeField] private TMP_Text _turretInfoText;

    public void UpdateTextInfo(float damage, float speedTurn, float currentHealth, float maximumHealth, int ammo, int maximumAmmo, int countScore, float currentExp, float maximumExp, int level, int power, float radius, int oreCell)
    {
        _damageText.text = $"Урон: {Math.Round(damage, 1)}";
        _speedTurnText.text = $"Поворот: {Math.Round(speedTurn, 1)}";
        _healthText.text = $"Жизнь: {Math.Round(currentHealth, 0)}/{Math.Round(maximumHealth, 0)}";
        _ammoText.text = $"Патроны: {ammo}/{maximumAmmo}";
        _countScoreText.text = $"Очки прокачки: {countScore}";
        _turretInfoText.text = $"Turret {level} level {Math.Round(currentExp, 0)}/{Math.Round(maximumExp, 0)}";
        _powerText.text = $"Время работы: {power}s";
        _radiusText.text = $"Радиус {Math.Round(radius, 2)}";
        _oreCell.text = $"Цена улучшения рудой: {oreCell}";
    }

    
}
