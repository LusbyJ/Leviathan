using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip activateAbility;
    public AudioClip bruiserDie;
    public AudioClip buildTower;
    public AudioClip centralHit;
    public AudioClip gameOver;
    public AudioClip leviathanDie;
    public AudioClip roundStart;
    public AudioClip smallFoeDie;
    public AudioClip towerDie;
    public AudioClip towerHit;
    public AudioClip upgradeTower;
    
    public static SfxManager sfxInstance;

    private void Awake()
    {
        if (sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}
