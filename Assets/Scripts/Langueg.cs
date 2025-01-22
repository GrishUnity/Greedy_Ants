using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Langueg : MonoBehaviour {

  public List<Text> txt;
  public List<string> RuText;
  public List<string> EnText;

  private void Update() {
    if(PlayerPrefs.GetString("Langueg") == "En") {
      for(int i = 0; i < txt.Count; i++) {
        txt[i].text = EnText[i];
      }
    }
    else {
      for (int i = 0; i < txt.Count; i++) {
        txt[i].text = RuText[i];
      }
    }
  }
}