using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceController : MonoBehaviour
{
    public float NoVoiceTime=120f;
    private float NoVoiceTimer=0f;
    public AudioSource VoiceSource;
    public AudioClip[] CentralDamage; 
    public AudioClip[] TowerDestroyed; 
    public AudioClip[] Leviathan; 
    public AudioClip[] Restock; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (NoVoiceTimer > 0)
        {
            NoVoiceTimer--;
        }
        else
        {
            NoVoiceTimer = 0;
        }
    }
    public void PlayCentralClip()
    {
        AudioClip clip = CentralDamage[Random.Range(0, CentralDamage.Length)];
        PlayVoiceClip(clip);
    }
    public void PlayDestroyedClip()
    {
        AudioClip clip = TowerDestroyed[Random.Range(0, TowerDestroyed.Length)];
        PlayVoiceClip(clip);
    }
    public void PlayLeviathanClip()
    {
        AudioClip clip = Leviathan[Random.Range(0, Leviathan.Length)];
        PlayVoiceClip(clip);
    }
    public void PlayRepairClip()
    {
        AudioClip clip = Restock[Random.Range(0, Restock.Length)];
        PlayVoiceClip(clip);
    }
    private void PlayVoiceClip(AudioClip clip)
    {
        if (!VoiceSource.isPlaying && NoVoiceTimer==0)
        {
            NoVoiceTimer = NoVoiceTime;
            VoiceSource.clip = clip;
            VoiceSource.Play(0);
        }
    }
}
