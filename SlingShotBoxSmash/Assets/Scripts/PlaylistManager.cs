using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaylistManager : MonoBehaviour
{
    List<AudioClip> trackPlaylist = new List<AudioClip>();

    public AudioClip[] tracks;
    public AudioSource audioSource;

    public static bool hasSong1BeenPurchased = false;
    public static bool hasSong2BeenPurchased = false;
    public static bool hasSong3BeenPurchased = false;
    public static bool hasSong4BeenPurchased = false;
    public static bool hasSong5BeenPurchased = false;

    

    private void Start()
    {

        //Add default song to laylist
        trackPlaylist.Add(tracks[5]);

        if (hasSong1BeenPurchased)
        {
            trackPlaylist.Add(tracks[0]);
        }

        if (hasSong2BeenPurchased)
        {
            trackPlaylist.Add(tracks[1]);
        }

        if (hasSong3BeenPurchased)
        {
            trackPlaylist.Add(tracks[2]);
        }

        if (hasSong4BeenPurchased)
        {
            trackPlaylist.Add(tracks[3]);
        }

        if (hasSong5BeenPurchased)
        {
            trackPlaylist.Add(tracks[4]);
        }

        if (trackPlaylist.Count == 1)
        {
            audioSource.loop = true;
        }
        else if (trackPlaylist.Count > 1)
        {
            audioSource.loop = false;
        }

        PlayRandomTrack();
    }

    private void Update()
    {
        //If clip is finished play next song
        if(audioSource.isPlaying == false && GameObject.Find("BossMusic").GetComponent<AudioSource>().isPlaying == false)
        {
            PlayRandomTrack();
        }
    }

    public void PlayRandomTrack()
    {
        int randomTrackNum = Random.Range(0, trackPlaylist.Count);

        if (trackPlaylist.Count > 0)
        {
            audioSource.clip = trackPlaylist[randomTrackNum];
            audioSource.Play();
        }

    }
}
