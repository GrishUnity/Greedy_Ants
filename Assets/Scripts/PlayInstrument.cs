using UnityEngine;
using UnityEngine.UI;

public class PlayInstrument : MonoBehaviour {

  private static int flagOtmena;

  public AudioSource frez;
  public ZvukButton clickButton;
  public AudioClip missClick;
  public AudioClip clip;

  public static bool flagINstrumentVibran;

  public GameObject atomracket;

  public GameObject freezparticle;

  bool flagbutton = false;
  bool flag = false;

  private float timer_freez = 5f;
  public bool flag_girja, flag_freez, flag_raspel, flag_sphera, flag_atom, flag_perec;

  public static bool freez;

  public Text txt;
  public Image button;
  public GameObject button_enable;

  public Peper perec;
  public Sphera sphera;
  public Meteor girja;
  public Raspel raspel;
  public Atom atom;

  public GameObject table;

  public GameObject CoolDown;

  public static bool tuch;

  public bool bigFlag;

  private void Start() {
    flagOtmena = 0;
    tuch = false;
    freez = false;
    flagINstrumentVibran = false;
    GameObject g1 = GameObject.FindGameObjectWithTag("ZvukButton");
    clickButton = g1.GetComponent<ZvukButton>();
  }

  public void Using() {
    if (/*Input.touchCount == 1 &&*/ !ButtonCanvas.paus_flag) {
      tuch = !tuch;
      if (txt.text == "Заморозка" || txt.text == "Freezing") {
        if (Manager.money >= 5 && !CoolDown.activeInHierarchy) {
          Manager.money -= 5;
          clickButton.ClickButton(clip);
          CoolDown.SetActive(true);
          freezparticle.SetActive(true);
          flag_freez = true;
          for (int i = 0; i < PoolObj.antRef.MainListAnt.Count; i++) {
            PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed /= 4;
            PoolObj.antRef.MainListAnt[i].GetComponent<Animator>().speed /= 4;
            PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed_east /= 4;
          }
          freez = true;
          button_enable.GetComponent<Button>().enabled = false;
        }
        else {
          clickButton.ClickButton(missClick);
        }
      }
      if (txt.text == "Сфера" || txt.text == "Sphere") {
        if (Manager.money >= 25 && !CoolDown.activeInHierarchy) {
          clickButton.ClickButton(clip);
          CoolDown.SetActive(true);
          sphera.instrumet = CoolDown;
          flag_sphera = true;
          sphera.Spawn(table.gameObject.transform.position);
          button_enable.GetComponent<Button>().enabled = false;
        }
        else {
          clickButton.ClickButton(missClick);
        }
      }
      if (txt.text == "Метеориты" || txt.text == "Meteorites") {
        if (Manager.money >= 10 && !CoolDown.activeInHierarchy) {
          clickButton.ClickButton(clip);
          CoolDown.SetActive(true);
          flag_girja = true;
          Button_enable_false();
        }
        else {
          clickButton.ClickButton(missClick);
        }
      }
      if ((txt.text == "Распылитель" || txt.text == "The sprayer") && flagOtmena == 0) {
        if (Manager.money >= 20 && !CoolDown.activeInHierarchy) {
          clickButton.ClickButton(clip);
          CoolDown.SetActive(true);
          flag_raspel = true;
          tuch = true;
          flagINstrumentVibran = true;
          flagOtmena = 1;
        }
        else {
          clickButton.ClickButton(missClick);
        }
      }
      else if(flagOtmena == 1)
        flagOtmena = 0;
      if (txt.text == "Бомбочка" || txt.text == "The bomb") {
        if (Manager.money >= 15 && !CoolDown.activeInHierarchy) {
          clickButton.ClickButton(clip);
          CoolDown.SetActive(true);
          flag_perec = true;
          Button_enable_false();
        }
        else {
          clickButton.ClickButton(missClick);
        }
      }
      if (txt.text == "Динамит" || txt.text == "Dynamite") {
        if (Manager.money >= 35 && !CoolDown.activeInHierarchy) {
          clickButton.ClickButton(clip);
          flag_atom = true;
          CoolDown.SetActive(true);
          button_enable.GetComponent<Button>().enabled = false;
          atomracket.SetActive(true);
          Manager.money -= 35;
        }
        else {
          clickButton.ClickButton(missClick);
        }
      }
    }
  }

  void Update() {
    if (BlockInstrument.flagBlock) {
      flagINstrumentVibran = false;
    }
    if (flag_sphera) {
      if (Sphera.flag == false) {
        flag_sphera = false;
        button_enable.GetComponent<Button>().enabled = true;
      }
    }
    if (flag_girja) {
      if (Input.GetMouseButton(0) && !ButtonCanvas.paus_flag) {
        if (girja.Spawn() == 0) {
          flag_girja = true;
        }
        else {
          CoolDown.SetActive(false);
          flag_girja = false;
          Button_enable_true();
        }
      }
    }
    if (flag_perec) {
      if (Input.GetMouseButton(0) && !ButtonCanvas.paus_flag) {
        if (perec.Spawn() == 0) {
          flag_perec = true;
        }
        else {
          CoolDown.SetActive(false);
          flag_perec = false;
          Button_enable_true();
        }
      }
    }
    if (flag_atom) {
      CoolDown.GetComponent<Image>().fillAmount -= 1 / 0.75f * Time.deltaTime;
      if (atom.flagboom) {
        CoolDown.SetActive(false);
        CoolDown.GetComponent<Image>().fillAmount = 1;
        button_enable.GetComponent<Button>().enabled = true;
        flag_atom = false;
        atom.DestroyAtom();
      }
    }
    if (flag_raspel) {
      if (Input.GetMouseButtonDown(0) && !flag && !ButtonCanvas.paus_flag) {
        raspel.Spawn();
        if (raspel.flagspawn == 0) {
          flag_raspel = true;
        }
        else if(raspel.flagspawn == -1) {
          flag_raspel = false;
          flag = false;
          CoolDown.SetActive(false);
          flagINstrumentVibran = false;
          CoolDown.GetComponent<Image>().fillAmount = 1;
        }
        else if(raspel.flagspawn == 1){
          flag = true;
        }
      }
      else if (flagbutton == tuch && flag) {
        Raspel.rsp?.DestroyY();
        flag = false;
        tuch = false;
        flag_raspel = false;
        CoolDown.SetActive(false);
        flagINstrumentVibran = false;
        flagOtmena = 0;
        CoolDown.GetComponent<Image>().fillAmount = 1;
      }
      else if (Raspel.rsp?.flagwork == false && flag) {
        flag_raspel = false;
        flag = false;
        CoolDown.SetActive(false);
        flagINstrumentVibran = false;
        flagOtmena = 0;
        CoolDown.GetComponent<Image>().fillAmount = 1;
      }
      else if (CoolDown.activeInHierarchy && Raspel.rsp?.flagwork == true && flag) {
        CoolDown.GetComponent<Image>().fillAmount -= 1 / 5f * Time.deltaTime;
      }
    }
    if (flag_freez) {
      CoolDown.GetComponent<Image>().fillAmount -= 1 / 5f * Time.deltaTime;
      timer_freez -= Time.deltaTime;
      if (timer_freez <= 0) {
        for (int i = 0; i < PoolObj.antRef.MainListAnt.Count; i++) {
          PoolObj.antRef.MainListAnt[i].GetComponent<Animator>().speed *= 4;
          PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed_east *= 4;
          PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed *= 4;
        }
        freez = false;
        timer_freez = 5f;
        flag_freez = false;
        button_enable.GetComponent<Button>().enabled = true;
        freezparticle.SetActive(false);
        CoolDown.SetActive(false);
        CoolDown.GetComponent<Image>().fillAmount = 1;
      }
    }
  }

  void Button_enable_true() {
    flagINstrumentVibran = false;
    button_enable.GetComponent<Button>().enabled = true;
  }

  void Button_enable_false() {
    flagINstrumentVibran = true;
    button_enable.GetComponent<Button>().enabled = false;
  }
}