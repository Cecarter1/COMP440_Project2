using UnityEngine;

[CreateAssetMenu(fileName = "NewTrack", menuName = "Track/Track Data")]
public class TrackData : ScriptableObject
{
    public string trackName;
    public AudioClip audioClip;
    public Sprite coverArt; // optional
    public float bpm;       // optional if needed
}

