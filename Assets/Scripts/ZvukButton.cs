using UnityEngine;

public class ZvukButton : MonoBehaviour {

  private AudioSource src;

  private void Awake() {
    src = gameObject.GetComponent<AudioSource>();
    GameObject g = GameObject.FindGameObjectWithTag("ZvukButton");
    if (g != null)
      Destroy(gameObject);
    else {
      gameObject.tag = "ZvukButton";
      DontDestroyOnLoad(this);
    }
  }

  public void ClickButton(AudioClip clip) {
    src.clip = clip;
    src.Play();
  }
}