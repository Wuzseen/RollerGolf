using UnityEngine;
using System.Collections;

public class SoundTrackController : MonoBehaviour {
	public AudioClip[] soundTracks;

	public static SoundTrackController Instance;

    public float musicVolume;

	void Awake() {
		if(Instance != null) {
			Destroy(this.gameObject);
			return;
		}
		SoundManager.Instance.showDebug = false;
        SoundManager.SetIgnoreLevelLoad(true);
		SoundManager.SetCrossDuration(2f);
        SoundManager.SetVolumeMusic(musicVolume);
		Instance = this;
		PlayRandomSong();
	}

	void PlayRandomSong() {
		SoundManager.Play (soundTracks[Random.Range(0,soundTracks.Length - 1)],false,SongEnd);
	}

	void SongEnd() {
		PlayRandomSong();
	}
}
