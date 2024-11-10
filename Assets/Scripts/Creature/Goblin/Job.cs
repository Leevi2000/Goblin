using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a list of different Jobs that can be assigned to Goblins. Goblin proficiencies are also based on this list.
/// </summary>
public class Jobs
{
    public static string[] jobList = {"unemployed", "woodcutting", "mining", "foraging", "building", "combat" };
    
}

/// <summary>
/// Contains Job type and a specified workSite if given.
/// </summary>
public class Job
{
    public const string UNEMPLOYED = "No assigned job";
    // Default job is unemployed
    public int _workId = 0;
    GameObject workSite;

    public override string ToString() 
    {
        return GetJobName();
    }

    public string GetJobName()
    {
        
        try
        {
            return Jobs.jobList[_workId];
        }
        catch (Exception e)
        {
            return UNEMPLOYED;
        }
        
    }

}
