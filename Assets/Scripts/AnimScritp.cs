using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimScritp : MonoBehaviour {

  public GameObject t;
  public GameObject panel;
  public AudioSource srcTimeStratPlay;

  public AudioClip clip;
  public AudioSource src; 

  public GameObject tusovka;

  public Collider col;

  public int count;

  public Text textcount1, textcount2;

  public GameObject text;

  public GameObject TextPanel;

  public Text txt;
  private int k;

  public GameObject menu_el;
  public GameObject instr_start;

  public AudioSource srcTextVolna;

  private void Awake() {
    count = 1;
  }

  public void AudioFkeshka() {
    src.PlayOneShot(clip);
  }

  public void DestroyBox() {
    Destroy(gameObject);
  }

  public void LayerChange() {
    gameObject.layer = 11;
    gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
  }

  public void VisibleFalse() {
    t.SetActive(true);
    panel.transform.rotation = new Quaternion(0, 0, 0, 1);
    gameObject.SetActive(false);
  }

  public void TextVolnaAudio() {
    srcTextVolna.Play();
  }

  public void TextVolnaAnim() {
    count += 1;
    textcount1.text = textcount2.text = count.ToString();
    gameObject.SetActive(false);
  }

  public void SubCountTime() {
    src.PlayOneShot(clip);
    k--;
    txt.text = k.ToString();
  }

  public void RestartTime() {
    k = 3;
    srcTimeStratPlay.Play();
    txt.text = k.ToString();
  }

  public void EnableCountPause() {
    AudioPauseOf.OnAudioPause();

    TextPanel.SetActive(false);
    Time.timeScale = 1;
    ButtonCanvas.paus_flag = false;
  }

  public void MinusFonMusic() {
    if(PlayerPrefs.GetInt("Music") == 0)
      GameObject.FindGameObjectWithTag("Music").GetComponent<Animation>().Play("FonMusic");
  }

  public void StartGame() {
    SpawnMenuAnt.FlagStartGame = 0;
    tusovka.SetActive(true);
  }

  public void T() {
    TusovkaAnswer.kol = Random.Range(10, 15);
  }

  public void MenuAnimPanelVisible() {
    menu_el.SetActive(true);
    instr_start.SetActive(false);
    menu_el.GetComponent<Animation>().Play("MenuPavelAnimVisible");
  }

  public void MenuAnimPanel() {
    menu_el.SetActive(false);
    instr_start.SetActive(true);
    instr_start.GetComponent<Animation>().Play("SpawnAnswer");
  }

  public void Tusovka() {
    tusovka.SetActive(true);
  }
}