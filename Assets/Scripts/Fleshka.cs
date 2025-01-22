using UnityEngine;
using UnityEngine.UI;

public class Fleshka : MonoBehaviour {

  public bool flaganim = false;

  public GameObject ImageFleshka;

  public float timer = 3.5f;

  public bool flagback = false;

  public float speed = 1.0f;
  public static bool flagStart = false;
  public Image image;
  public Color colorboom, colornormal;

  public static float startTime;

  public void Update() {
    float t = (Time.time - startTime) * speed;
    if(flagStart && !flaganim) {
      ImageFleshka.SetActive(true);
      ImageFleshka.GetComponent<Animation>().Play("FleshkaGlass");
      flaganim = true;
    }
    if (flagStart) {
      image.color = Color.Lerp(colornormal, colorboom, t);
      if (image.color == colorboom) {
        flagStart = false;
        flagback = true;
        startTime = -1;
      }
    }
    else if(flagback) {
      timer -= Time.deltaTime;
      if(timer <= 0) {
        if(startTime == -1) {
          startTime = Time.time;
          t = (Time.time - startTime) * speed;
        }
        image.color = Color.Lerp(colorboom, colornormal, t);
        if (image.color == colornormal) {
          flagback = false;
          timer = 2.5f;
          flaganim = false;
          ImageFleshka.SetActive(false);
        }
      }
    }
  }
}