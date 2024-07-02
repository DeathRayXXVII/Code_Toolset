using System;
using UnityEngine;
using Steamworks;

public class SteamManager : MonoBehaviour
{
    
    private void Awake()
    {
        try
        {
            SteamClient.Init(2957560);
            Debug.Log("Steamworks initialized");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void Update()
    {
        SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }
}