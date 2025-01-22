using UnityEngine;

public class MusicOnOrOff : MonoBehaviour {

  public AudioClip clip;
  public GameObject g;
  public GameObject img1;
  public ZvukButton clickButton;

  public void Start() {
    GameObject g1 = GameObject.FindGameObjectWithTag("ZvukButton");
    clickButton = g1.GetComponent<ZvukButton>();
    g = GameObject.FindGameObjectWithTag("Music");
    if (PlayerPrefs.GetInt("Music") == 0) {
      img1.SetActive(false);
    }
    else {
      img1.SetActive(true);
    }
  }

  public void MusicOfforOn() {
    clickButton.ClickButton(clip);
    if (PlayerPrefs.GetInt("Music") == 0) {
      img1.SetActive(true);
      PlayerPrefs.SetInt("Music", 1);
      g.GetComponent<AudioSource>().volume = 0;
    }
    else {
      img1.SetActive(false);
      PlayerPrefs.SetInt("Music", 0);
      if(!ButtonCanvas.paus_flag)
        g.GetComponent<AudioSource>().volume = 1f;
    }
  }
}