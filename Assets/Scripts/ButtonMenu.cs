using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ButtonMenu : MonoBehaviour {

  public GameObject panel;

  public YandexGame SDK;
  public List<Button> buttons = new List<Button>();
  public bool loadScene2 = false;

  public bool flagOpen = false;

  public ZvukButton clickButton;

  public AudioClip clip;

  public AudioClip missclip;

  public Animation Settings;

  public Text txtRecord;
  public Text txtWolni;

  public GameObject go;
  public GameObject goB;

  public GameObject restart;
  public GameObject play;

  public GameObject image;

  public GameObject menu_el;
  public GameObject instr_start;

  public Animation playAnim;

  public Animation visibleInstrument;
  public Animation visibleInstrumentPanel;

  public List<Animation> l;

  private void Start() {
    GameObject g1 = GameObject.FindGameObjectWithTag("ZvukButton");
    clickButton = g1.GetComponent<ZvukButton>();
    if (PlayerPrefs.GetInt("Zvuk") == 0) {
      g1.GetComponent<AudioSource>().volume = 0.25f;/////////////////////////////////////////////
    }
    else {
      g1.GetComponent<AudioSource>().volume = 0;
    }

    GameObject g = GameObject.FindGameObjectWithTag("Music");
    if (PlayerPrefs.GetInt("Music") == 0) {
      g.GetComponent<AudioSource>().volume = 0.75f;///////////////////////////////////////////
    }
    else {
      g.GetComponent<AudioSource>().volume = 0;
    }
  }

  private void Update() {
    if(loadScene2) {
      foreach (Button t in buttons) {
        t.enabled = false;
      }
    }
    txtRecord.text = $":{PlayerPrefs.GetInt("Record")}";
    txtWolni.text = $":{PlayerPrefs.GetInt("RecordWaves")}";
    if (Button_Vibor.list?.Count != 3) {
      restart.SetActive(false);
    }
    else {
      restart.SetActive(true);
    }
  }

  public void SettingsInVisible() {
    clickButton.ClickButton(clip);
    Settings.Play("SettingsAnim2");
    flagOpen = false;
  }

  public void SettingsVisible() {
    clickButton.ClickButton(clip);
    Settings.Play("SettingsAnim1");
    flagOpen = true;
  }

  public void Go() {
    clickButton.ClickButton(clip);
    go.SetActive(false);
    play.SetActive(true);
    for(int i = 0; i < l.Count; i++) {
      l[i].Play("VoprosAnim");
    }
  }

  public void StartPlay() {
    clickButton.ClickButton(clip);
    visibleInstrument.Play("MenuPanelAnim");
    if(flagOpen)
      Settings.Play("SettingsAnim2");
  }

  public void Play() {
    if (Button_Vibor.list != null && Button_Vibor.list.Count == 3) {
      clickButton.ClickButton(clip);
      playAnim.Play("PerehodKGame");
      loadScene2 = true;
    }
    else {
      clickButton.ClickButton(missclip);
    }
  }

  public void CallReclama() {
    clickButton.ClickButton(clip);
    SDK._RewardedShow(1);
  }

  public void NotCallReclama() {
    panel.SetActive(false);
  }

  public void VisiblePanelVoprosReclama() {
    panel.SetActive(true);
  }

  public void RestartInstrument() {
    panel.SetActive(false);
    if (Button_Vibor.listAnim.Count != 0 && TusovkaAnswer.kol == 0) {
      for (int i = 0; i < Button_Vibor.listAnim.Count; i++) {
        Button_Vibor.list[i] = null;
        Button_Vibor.listAnim[i]?.Play("VoprosAnim");
        Button_Vibor.listAnim[i] = null;
      }
      Button_Vibor.listAnim.Clear();
      Button_Vibor.list = null;
    }
  }

  public void Exitgame() {
    clickButton.ClickButton(clip);
    Application.Quit();
  }


  public void Menu() {
    if ((Button_Vibor.list == null || Button_Vibor.list.Count == 0 || Button_Vibor.list.Count == 3) && TusovkaAnswer.kol == 0) {
      clickButton.ClickButton(clip);
      playAnim.Play("AnswerInvisible");
    }
    else {
      clickButton.ClickButton(missclip);
    }
  }

  public void MenuAnimPanel() {
    clickButton.ClickButton(clip);
    instr_start.SetActive(true);
  }
}