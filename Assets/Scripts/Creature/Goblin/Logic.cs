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

    [SerializeField] float workTimer = 0;
    [SerializeField] float workTreshold = 10;
    [SerializeField] float workCooldown = 20;
    [SerializeField] float coolDownTimer = 0;
    OverlayTile workTile;
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
            if (coolDownTimer > workCooldown)
            {
                workTimer = 0;
            }

            timer = timerStart + Random.Range(-2f, 2f);
            if(goblinData.PathRequest == false && !goblinData.Moving)
            {
                SelectAction();
            }
        }    

        if(goblinData.working)
        {
            workTimer += Time.fixedDeltaTime;
            coolDownTimer = 0;
        }
        else
        {
            //Reset work timer after cooldown to start working again
            coolDownTimer += Time.fixedDeltaTime;
            
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
        else if (workTimer < workTreshold)
        {
            if(!goblinData.working)
            {
                goblinData.working = true;
                Work(goblinData);
            }
        }
        else if (LogicHelper.HappinessThresholdSurpassed(goblinData))
        {

        }
        else
            StartRandomWander(goblinData);

        if(workTimer > workTreshold)
        {
            goblinData.working = false;
        }
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

    private void Work(Creatures.Goblin goblin)
    {
        Debug.Log("Initiating work");
        if(workTile == null)
        {
            GetWorking(goblin);
        }

        // If goblin is close enough to work site, begin working. 
        if(TilemapHelper.GetManhattanDistance(goblin.ActiveTile, workTile) < 1)
        {
            // If there are other fitting jobs closer, begin working on those instead. (For example when current site depletes)
            if(workTile != GameObject.Find("GameManager").GetComponent<JobFinder>().ReturnWork(goblin))
            {
                GetWorking(goblin);
            }
        }
        else
        {
            goblin.TargetTile = workTile;
            goblin.PathRequest = true;
        }

        workTile = GameObject.Find("GameManager").GetComponent<JobFinder>().ReturnWork(goblin);

    }

    void GetWorking(Creatures.Goblin goblin)
    {
        workTile = GameObject.Find("GameManager").GetComponent<JobFinder>().ReturnWork(goblin);
        goblin.TargetTile = workTile;
        goblin.PathRequest = true;
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


