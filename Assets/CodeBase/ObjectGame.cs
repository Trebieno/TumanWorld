using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGame : MonoCache, IAttackeble
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;
    [SerializeField] protected float maximimTimeDismantling = 1;
    [SerializeField] protected GameObject drop; // Выпадаемый дроп
    [SerializeField] protected int percent; //Процент выпадения дропа
    protected Player player;
    protected bool isDownKey = false;
    protected float currentTimeDismantling = 0;
    protected GameObjects typeObject;


    public GameObjects TypeObject => typeObject;
    public float CurHealth => curHealth;
    private void Start()
    {
        player = PlayerCash.Instance.Player;
        Objects.Instance.ObjectsGame.Add(this);
    }

    public void Dismantling(GameObjects gameObjects)
    {
        switch (gameObjects)
        {
            case GameObjects.MiningTurret:
                player.MineTurretCount += 1;
                break;

            case GameObjects.AttackTurret:
                player.AttackTurretCount += 1;
                break;

            case GameObjects.Light:
                player.LightCount += 1;
                break;

            case GameObjects.Ship:
                player.SpikeTrapCount += 1;
                break;
        }

        player.UpdateUI();
        Destroy(gameObject);
    }

    public virtual void Active()
    {
        if(player == null)
            player = PlayerCash.Instance.Player;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isDownKey = true;
            currentTimeDismantling = 0;
        }

        else if (Input.GetKeyUp(KeyCode.Z))
        {
            isDownKey = false;
            player.DestroySlider.gameObject.SetActive(false);
        }

        if (isDownKey)
        {
            if (currentTimeDismantling < maximimTimeDismantling)
            {
                if(!player.DestroySlider.gameObject.activeSelf)
                    player.DestroySlider.gameObject.SetActive(true);
                currentTimeDismantling += Time.deltaTime;
                player.DestroySlider.maxValue = maximimTimeDismantling;
                player.DestroySlider.value = currentTimeDismantling;
            }

            else
            {
                if (player.DestroySlider.gameObject.activeSelf)
                    player.DestroySlider.gameObject.SetActive(false);
                Dismantling(typeObject);
            }

            
        }
    }

    public virtual void NotActive()
    {

    }

    public virtual void SetDamage(float damage, Turret turret = null)
    {
        curHealth -= damage;
        if(curHealth <= 0)
        {

            Vector3 centrPoint = transform.position;
            Vector3 randomPoint = centrPoint + new Vector3(Random.value-0.5f,Random.value-0.5f,Random.value-0.5f).normalized * 0.3f;
            if(drop != null && Random.Range(0, 100) <= percent)
                Instantiate(drop, randomPoint, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

public enum GameObjects
{
    Ore,
    Light,
    Ship,
    MiningTurret,
    AttackTurret,
    Clip,
    Wall,
    Tree,
    Radar,
    Grass,
}
