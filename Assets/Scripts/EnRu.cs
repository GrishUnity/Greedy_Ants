using UnityEngine;
using UnityEngine.UI;

public class EnRu : MonoBehaviour {
  
  public Text buttonTxt;
  public Image img;
  public Sprite ru;
  public Sprite en;

  public void RuAndEn() {
    if(buttonTxt.text == "En") {
      buttonTxt.text = "Ру";
      PlayerPrefs.SetString("Langueg", "Ru");
      img.sprite = ru;
    }
    else {
      buttonTxt.text = "En";
      PlayerPrefs.SetString("Langueg", "En");
      img.sprite = en;
    }
  }
}