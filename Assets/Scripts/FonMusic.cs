using UnityEngine;

public class FonMusic : MonoBehaviour {

  public AudioClip clipPause;
  public AudioSource src;
  public float value;

  private void Awake() {
    GameObject g = GameObject.FindGameObjectWithTag("Music");
    if (g != null)
      Destroy(gameObject);
    else {
      gameObject.tag = "Music";
      if (PlayerPrefs.GetInt("Music") == 0) {
        PlayClip(clipPause);
        src.volume = 0.75f;///////////////////////////////////////////////////
      }
      else {
        PlayClip(clipPause);
        src.volume = 0f;
      }
      DontDestroyOnLoad(this);
    }
  }

  public void PlayClip(AudioClip clip) {
    src.clip = clip;
    src.Play();
  }
}