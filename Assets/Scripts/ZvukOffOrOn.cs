using UnityEngine;

public class ZvukOffOrOn : MonoBehaviour {

  public AudioClip clip;
  public GameObject g;
  public GameObject img1;
  public ZvukButton clickButton;

  public void Start() {
    GameObject g1 = GameObject.FindGameObjectWithTag("ZvukButton");
    clickButton = g1.GetComponent<ZvukButton>();

    g = GameObject.FindGameObjectWithTag("ZvukButton");
    if (PlayerPrefs.GetInt("Zvuk") == 0) {
      img1.SetActive(false);
    }
    else {
      img1.SetActive(true);
    }
  }

  public void ZvukOfforOn() {
    clickButton.ClickButton(clip);
    if (!img1.activeInHierarchy) {
      img1.SetActive(true);
      PlayerPrefs.SetInt("Zvuk", 1);
      g.GetComponent<AudioSource>().volume = 0;
    }
    else {
      img1.SetActive(false);
      PlayerPrefs.SetInt("Zvuk", 0);
      g.GetComponent<AudioSource>().volume = 0.5f;
    }
  }
}