using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using YG;

public class Manager : MonoBehaviour {

  public AnimScritp textvolna;

  public Text wave;
  public static bool flag;

  public GameObject panel;
  public FonMusic mscfon;

  public Text textXxp;

  public GameObject damageImage;

  public float timerX = 10;
  public static int x = 1;

  public static int count;
  public static int money;

  public Text count_text;
  public Text money_text;
  public Text record_text;
  public Text recordWaves_text;
  public Text count_text_lose;

  public GameObject lose_panel;
  public List<GameObject> play_instrum;

  private void Awake() {
    flag = false;
  }

  private void Start() {
    money = count = 0;
    x = 1;
    BonusBox.flagX = false;
  }

  private void Update() {
    Scan(Popcorn.listfreepop);
    if (Popcorn.listfreepop.Count == 0) {
      Time.timeScale = 0f;
      if (!flag) {
        if(!panel.activeInHierarchy) {
          AudioPauseOf.OfAudioPause();
          panel.SetActive(true);
        }
      }
      else {
        ButtonCanvas.paus_flag = true;
        if (!lose_panel.activeInHierarchy)
          LosePanelEnable();
      }
    }
    if(BonusBox.flagX) {
      timerX -= Time.deltaTime;
      if(timerX <= 0) {
        x = 1;
        BonusBox.flagX = false;
        timerX = 10;
      }
    }
    count_text.text = count.ToString();
    money_text.text = money.ToString();
    textXxp.text = $"X{x}";
  }

  public void LosePanelEnable() {
    AudioPauseOf.OfAudioPause();
    mscfon = GameObject.FindGameObjectWithTag("Music")?.GetComponent<FonMusic>();
    mscfon.src.volume = 0;
    lose_panel.SetActive(true);
    damageImage.SetActive(false);
    if(textvolna.count == 1) {
      textvolna.count = 1;
    }
    else if(textvolna.count > 1) {
      textvolna.count -= 1;
    }
    if (textvolna.count > PlayerPrefs.GetInt("RecordWaves")) {
      PlayerPrefs.SetInt("RecordWaves", textvolna.count);
      YandexGame.NewLeaderboardScores("LiderBordWaves", PlayerPrefs.GetInt("RecordWaves"));
    }
    if (count > PlayerPrefs.GetInt("Record")) {
      PlayerPrefs.SetInt("Record", count);
      YandexGame.NewLeaderboardScores("LiderBordCount", PlayerPrefs.GetInt("Record"));
    }
    if (PlayerPrefs.GetString("Langueg") == "Ru") {
      wave.text = "Максимальное кол-во волн:" + PlayerPrefs.GetInt("RecordWaves").ToString();
      recordWaves_text.text = "Количество волн:" + (textvolna.count);
      record_text.text = $"Рекорд:{PlayerPrefs.GetInt("Record")}";
      count_text_lose.text = $"Ваши очки:{count}";
    }
    else {
      wave.text = "Max count of waves:" + PlayerPrefs.GetInt("RecordWaves").ToString();
      recordWaves_text.text = "Count of waves:" + (textvolna.count);
      record_text.text = $"Record:{PlayerPrefs.GetInt("Record")}";
      count_text_lose.text = $"Yor count:{count}";
    }
  }

  public void Scan(List<GameObject> list) {
    for (int i = 0; i < list.Count; i++) {
      if (list[i] == null) {
        list.RemoveAt(i);
      }
    }
  }
}