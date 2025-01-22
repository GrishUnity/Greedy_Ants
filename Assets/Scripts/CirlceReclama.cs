using UnityEngine;

public class CirlceReclama : MonoBehaviour {

  public GameObject panel;
  public Manager mng;

  public void Otmena() {
    ButtonCanvas.paus_flag = true;
    if(!mng.lose_panel.activeInHierarchy)
      mng.LosePanelEnable();
    panel.SetActive(false);
    Manager.flag = true;
  }
}