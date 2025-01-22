using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OnClickVideo : MonoBehaviour {

  public Text textButton;

  public VideoPlayer video;
  public ZvukButton clickButton;
  public AudioClip clip;

  public bool flagText = false;
  public GameObject panel;

  public void Awake() {
    GameObject g1 = GameObject.FindGameObjectWithTag("ZvukButton");
    clickButton = g1.GetComponent<ZvukButton>();
  }

  public void OnCklikk() {
    clickButton.ClickButton(clip);
    if (!flagText) {
      panel.GetComponent<Animation>().Play("VideoPanelClick2");
      flagText = !flagText;
      video.Play();
      textButton.text = "Text";
    }
    else {
      video.Pause();
      textButton.text = "Video";
      panel.GetComponent<Animation>().Play("VideoPanelClick1");
      flagText = !flagText;
    }
  }
}