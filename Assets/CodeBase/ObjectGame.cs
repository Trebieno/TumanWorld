using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGame : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;
    [SerializeField] protected float maximimTimeDismantling = 1;
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
                Debug.Log(player);
                Debug.Log(player.DestroySlider);
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
}

public enum GameObjects
{
    MiningTurret,
    AttackTurret,
    Light,
    Ship,
    Radar,
    Ore,
    Wall,
    Tree,
    Grass,
}
