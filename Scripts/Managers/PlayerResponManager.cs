using System.Collections;
using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResponManager : MonoBehaviour
{
    public GameObject currentCheakPoint; //Last cheak point found
    public CharacterController cc;
    public GameObject playerOj; //Player object
    public IntData score;
    public IntData health; //visual health
    public IntData maxHealth; //max health
    public UnityEvent respawnEvent;
    [Header ("Particals")]
    public GameObject deathParticles;
    public GameObject respawnParticles;
    [Header ("Respawn Delay")]
    public float respawnDelay; //Delay for respawning
    [Header ("Penalty on Death")]
    public int deathPenalty; //Penalty for dieing
    // Start is called before the first frame update
    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }
    public IEnumerator RespawnPlayerCo()
    {
        //Instantiate (deathParticles, cc.transform.position, cc.transform.rotation); //Generate Death Particles
        //Hide the player on death
        playerOj.SetActive(false); 
        cc.GetComponent<Renderer>().enabled = false;
        
        //Respon Delay
        yield return new WaitForSeconds(respawnDelay);
        //Match Player transfom poition with cheak point
        cc.transform.position = currentCheakPoint.transform.position;
        //Show Player
        playerOj.SetActive(true);
        cc.GetComponent<Renderer>().enabled = true;
        //player.curHP = player.maxHp;
        //healthBar.SetHealth(player.maxHp);
        //Show Respawn Particle
        //Instantiate(respawnParticles, currentCheakPoint.transform.position, currentCheakPoint.transform.rotation);
    }
}
