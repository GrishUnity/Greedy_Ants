using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class BlockInstrument : MonoBehaviour {

  public static bool flagBlock;

  public Text timeblock;

  private float timer = -1f;

  public bool flag = false;

  public GameObject presled;
  public List<GameObject> bloki;

  public static BlockInstrument blok;

  public void Awake() {
    blok = this;
    flagBlock = false;
  }

  private void Update() {
    if(flag) {
      if (gameObject.transform.position == presled.transform.position) {
        flagBlock = true;
        for (int i = 0; i < bloki.Count; i++) {
          bloki[i].SetActive(true);
        }
        gameObject.transform.position = new Vector3(-37.96875f, -240.895157f, 1281.71729f);
        flag = false;
        timer += 32f;
        timeblock.gameObject.SetActive(true);
      }
      if (presled != null) {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, presled.transform.position, 75 * Time.deltaTime);      
      }
    }
    if(timer != -1) {
      timeblock.text = ((int)timer).ToString();
      timer -= Time.deltaTime;
      if (timer <= 0) {
        for (int i = 0; i < bloki.Count; i++) {
          bloki[i].SetActive(false);
        }
        flagBlock = false;
        timer = -1;
        gameObject.SetActive(false);
        timeblock.gameObject.SetActive(false);
      }
    }
  }
}