using UnityEngine;

public class EffectAudio : MonoBehaviour {

  public float volume;
  private void Awake() {
    volume = gameObject.GetComponent<AudioSource>().volume;
    ValidAudio();
  }

  private void Start() {
    ValidAudio();
  }

  private void Update() {
    ValidAudio();
  }

  public void ValidAudio() {
    if (PlayerPrefs.GetInt("Zvuk") == 1) {
      gameObject.GetComponent<AudioSource>().volume = 0;
    }
    else {
      gameObject.GetComponent<AudioSource>().volume = volume;
    }
  }
}