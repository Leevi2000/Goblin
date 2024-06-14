using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Helper.TilemapOperations;

public class Logic : MonoBehaviour
{
    Creatures.Goblin goblinData;
    [SerializeField] float timer = 6;
    float timerStart = 6;
    // Start is called before the first frame update
    void Start()
    {
        goblinData = this.gameObject.GetComponent<Creatures.Goblin>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if(timer < 0)
        {
            timer = timerStart + Random.Range(-2f, 2f);
            if(goblinData.PathRequest == false && !goblinData.Moving)
            {
                SelectAction();
            }
        }    
    }

    /// <summary>
    /// Action selection logic.
    /// </summary>
    public void SelectAction()
    {
        if (LogicHelper.HostileNearby(goblinData))
        {

        }
        else if (IsNight())
        {

        }
        else if (LogicHelper.TirednessThresholdSurpassed(goblinData))
        {
            // Go Find a house to sleep in
        }
        else if (LogicHelper.SicknessThresholdSurpassed(goblinData))
        {
            // Go find a house to rest in, preferably medicinal house
        }
        else if (LogicHelper.HungerThresholdSurpassed(goblinData))
        {
            // Go find food
        }
        else if (LogicHelper.AngerThresholdSurpassed(goblinData))
        {
            // Increases strength and
        }
        else if (LogicHelper.FearThresholdSurpassed(goblinData))
        {

        }
        else if (LogicHelper.SadnessThresholdSurpassed(goblinData))
        {

        }
        else if (LogicHelper.DisgustThresholdSurpassed(goblinData))
        {

        }
        else if (LogicHelper.HappinessThresholdSurpassed(goblinData))
        {

        }
        else
            StartRandomWander(goblinData);
    }

// Below are the actions goblin can take

    public void GeneralSleepRoutine()
    {
        //GoHome()
    }

    /// <summary>
    /// Rest is generally worse than sleep. Used in situations such as extreme exhaustion or when there's no bed to be found.
    /// </summary>
    public void Rest()
    {
        
    }

    public void Sleep()
    {

    }

    public void ScreamHelp()
    {

    }

    public void Defend()
    {

    }

    public void Kill()
    {

    }

    public void Attack()
    {

    }

    public void Rampage()
    {

    }

    public void Eat()
    {

    }

    public void Discuss()
    {

    }


    // Maybe to be removed?
    public void Cry()
    {

    }

    private void StartRandomWander(Creatures.Creature creature)
    {
        var maxDistance = creature.WanderDistance;
        var tiles = TilemapHelper.GetTilesAroundTarget(creature.ActiveTile, creature.WanderDistance);
        List<OverlayTile> validTiles = new List<OverlayTile>();
        // Delete all tiles from list where creature can't walk on.
        foreach (var tile in tiles)
        {
            if (creature.MovementTypeTileNames.Contains(tile.tileType))
                validTiles.Add(tile);
        }

        //Choose a random target tile
        if(validTiles.Count > 0)
        {
            var index = UnityEngine.Random.Range(0, validTiles.Count);
            creature.TargetTile = validTiles[index];
            creature.PathRequest = true;
        }
        else
        {
            Debug.Log("Could not start wandering: Tile couldn't be found.");
        }
    }



    // Below are all the boolean checker methods for SelectAction() logic.
    public bool HostileNearby()
    {
        return false;
    }

    public bool IsNight()
    {
        return false;
    }

    public bool TirednessTresholdSurpassed()
    {
        return false;
    }
    public bool SicknessTresholdSurpassed()
    {
        return false;
    }
    public bool HungerTresholdSurpassed()
    {
        return false;
    }
    public bool AngerTresholdSurpassed()
    {
        return false;
    }
    public bool FearTresholdSurpassed()
    {
        return false;
    }
    public bool SadnessTresholdSurpassed()
    {
        return false;
    }
    public bool DisgustTresholdSurpassed()
    {
        return false;
    }
    public bool HappinessTresholdSurpassed()
    {
        return false;
    }
}


