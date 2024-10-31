using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jobs
{
    public static string[] jobList = {"unemployed", "woodcutting", "mining", "foraging", "building", "combat" };
    
}
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
