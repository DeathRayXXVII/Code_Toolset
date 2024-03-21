using System.Collections;
using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResponManager : MonoBehaviour
{  
    public FloatData life;
    public BoolData startSpawn;
    [Header ("Respawn")]
    public vector3Data spawnPosition;
    public vector3Data respawnPosition;
    public CharacterController cc;
    public GameObject playerOj;
    public float respawnDelay;
    public UnityEvent respawnEvent, noLifeEvent;
    [Header ("Particles")]
    public GameObject deathParticles;
    public GameObject respawnParticles;
    
    
    private void Start()
    {
        if (startSpawn.value == true)
        {
            cc.transform.position = spawnPosition.value;
        }
        else
        {
            cc.transform.position = respawnPosition.value;
        }
    }
    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }
    public IEnumerator RespawnPlayerCo()
    {
        Instantiate (deathParticles, cc.transform.position, cc.transform.rotation); //Generate Death Particles

        playerOj.SetActive(false); 
        cc.GetComponent<Renderer>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);
        
        if (life.value > 1 && startSpawn.value)
        {
            cc.transform.position = spawnPosition.value;
        }
        else
        {
            //noLifeEvent.Invoke();
            LoseLife();
            cc.transform.position = respawnPosition.value;
        }
        

        playerOj.SetActive(true);
        cc.GetComponent<Renderer>().enabled = true;

        Instantiate(respawnParticles, cc.transform.position, cc.transform.rotation);
    }
    
    public void StartingPosition()
    {
        cc.transform.position = spawnPosition.value;
    }
    
    public void LoseLife()
    {
        if (life.value <= 0)
        {
            noLifeEvent.Invoke();
        }
        else
        {
            cc.transform.position = spawnPosition.value;
        }
    }
    
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition.value = newSpawnPoint;
        startSpawn.value = false;
    }
}
