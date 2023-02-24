using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Action selection logic.
    /// </summary>
    public void SelectAction()
    {
     

        if(HostileNearby())
        {

        }
        else if (IsNight())
        {
            
        }
        else if (TirednessTresholdSurpassed())
        {

        }
        else if (SicknessTresholdSurpassed())
        {

        }
        else if (HungerTresholdSurpassed())
        {

        }
        else if (AngerTresholdSurpassed())
        {

        }
        else if (FearTresholdSurpassed())
        {

        }
        else if (SadnessTresholdSurpassed())
        {

        }
        else if (DisgustTresholdSurpassed())
        {

        }
        else if (HappinessTresholdSurpassed())
        {

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


