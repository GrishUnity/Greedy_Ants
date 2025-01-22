using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Vibor : MonoBehaviour {

  private ZvukButton clickButton;
  public AudioClip clip;
  public AudioClip missclip;

  public Animation anim;
  public Text txt;
  public static List<string> list;
  public static List<Animation> listAnim;

  void Start() {
    list = null;
    listAnim = new List<Animation>();
    GameObject g1 = GameObject.FindGameObjectWithTag("ZvukButton");
    clickButton = g1.GetComponent<ZvukButton>();
  }

  public void Zanesenije() {
    if (list != null && TusovkaAnswer.kol == 0 && list.Count != 3 && !list.Contains(txt.text)) {
      clickButton.ClickButton(clip);
      listAnim.Add(gameObject.GetComponent<Animation>());
      list.Add(txt.text);
      anim.Play("NotVopros");
    }
    else {
      clickButton.ClickButton(missclip);
    }
  }
}