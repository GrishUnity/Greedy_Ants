using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using YG;

public class ButtonCanvas : MonoBehaviour {

  public AnimScritp textvolna;

  public Complexity comp;
  public GameObject textVolna;
  public Animator circle;
  public GameObject cake;
  public List<Vector3> tochkiCake;
  public GameObject panel;
  public YandexGame SDK;
  public Animator settings;

  public AudioClip clipPause;

  public AudioClip clipGame;
  public FonMusic mscfon;

  private ZvukButton clickButton;
  public AudioClip clip;

  public Text countWaves;

  public GameObject buttonRestart;
  public GameObject buttonManu;

  public GameObject panelAnswer;

  public GameObject timeCountReturn;

  public GameObject damageImage;
  
  public static bool paus_flag;

  public Text record;
  public GameObject pause_menu;

  private void Start() {
    GameObject g1 = GameObject.FindGameObjectWithTag("ZvukButton");
    clickButton = g1.GetComponent<ZvukButton>();
    if(gameObject.name == "Button Manager")
      paus_flag = false;
  }

  public void Play() {
    clickButton.ClickButton(clip);
    SceneManager.LoadScene(1);
    Meteor.listaudiometeor = new List<AudioSource>();
    textvolna.count = 1;
  }

  public void Menu()  {
    clickButton.ClickButton(clip);
    SceneManager.LoadScene(0);
    GameObject g = GameObject.FindGameObjectWithTag("Music");
    g.GetComponent<FonMusic>().PlayClip(clipPause);
    paus_flag = false;
    Button_Vibor.list = new List<string>();
  }

  public void PanelAnswer() {
    clickButton.ClickButton(clip);
    panelAnswer.SetActive(true);
    buttonRestart.SetActive(false);
    buttonManu.SetActive(true);
  }

  public void PanelAnswerRestart() {
    clickButton.ClickButton(clip);
    panelAnswer.SetActive(true);
    buttonRestart.SetActive(true);
    buttonManu.SetActive(false);
  }

  public void Pausegame() {
    AudioPauseOf.OfAudioPause();
    mscfon = GameObject.FindGameObjectWithTag("Music")?.GetComponent<FonMusic>();
    mscfon.src.volume = 0;
    clickButton.ClickButton(clip);
    Time.timeScale = 0f;
    paus_flag = true;
    pause_menu.SetActive(true);
    damageImage.SetActive(false);
    if (PlayerPrefs.GetString("Langueg") == "En") {
      record.text = "Record:" + PlayerPrefs.GetInt("Record").ToString();
      countWaves.text = "Max count of waves:" + PlayerPrefs.GetInt("RecordWaves").ToString();
    }
    else {
      record.text = "Рекорд:" + PlayerPrefs.GetInt("Record").ToString();
      countWaves.text = "Максимальное кол-во волн:" + PlayerPrefs.GetInt("RecordWaves").ToString();
    }
    timeCountReturn.SetActive(false);
}

  public void Returngame() {
    clickButton.ClickButton(clip);
    pause_menu.SetActive(false);
    timeCountReturn.SetActive(true);
  }

  public void ReturnPausPanel() {
    clickButton.ClickButton(clip);
    panelAnswer.SetActive(false);
  }

  public void SettingsVisible() {
    clickButton.ClickButton(clip);
    settings.Play("PauseSettings");
  }

  public void SettingsInVisible() {
    clickButton.ClickButton(clip);
    settings.Play("PauseSettings1");
  }

  public void CallReclama() {
    clickButton.ClickButton(clip);
    SDK._RewardedShow(1);
    circle.speed = 0f;
    paus_flag = true;
  }

  public void RestartGameReclama() {
    textVolna.SetActive(false);
    comp.flag_volna = false;
    panel.SetActive(false);
    circle.speed = 1f;
    GameObject[] antChild = GameObject.FindGameObjectsWithTag("AntChild");
    for (int i = 0; i < antChild.Length; i++) {
      Destroy(antChild[i]);
    }
    for (int i = 0; i < PoolObj.antRef.MainListAnt.Count; i++) {
      if (PoolObj.antRef.MainListAnt[i].activeInHierarchy) {
        PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().DeadAntReastartReclama();
      }
    }
    Vector3 vec = tochkiCake[Random.RandomRange(0, tochkiCake.Count)];
    GameObject obj = Instantiate(cake, vec, Quaternion.identity);
    obj.name = "Cake";
    Popcorn.listfreepop.Add(obj);
    obj.SetActive(false);
    PoolObj.antRef.SpawnPopcorn();
    Spawn.StaticDamage.SetActive(false);
    Spawn.time_spawna = 3f;
    Manager.flag = true;
    paus_flag = false;
    AudioPauseOf.OnAudioPause();
  }
}