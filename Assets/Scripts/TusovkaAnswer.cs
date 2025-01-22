using System.Collections.Generic;
using UnityEngine;

public class TusovkaAnswer : MonoBehaviour {
  public AudioClip tusn;
  public AudioSource src;

  private List<GameObject> arrValid = new List<GameObject>();
  public static int kol;
  public GameObject[] arrObj;
  public GameObject[] arrPos = new GameObject[6];

  private void Start() {
    kol = 0;
    Tus(ref kol);
    if(PlayerPrefs.GetInt("Zvuk") == 1) {
      src.volume = 0;
    }
    else {
      src.volume = 1;
    }
  }

  private void Update() {
    if(kol != 0) {
      for (int i = 0; i < arrObj.Length; i++) {
        if (arrObj[i].transform.position != arrPos[i].transform.position) {
          arrObj[i].transform.position = Vector3.MoveTowards(arrObj[i].transform.position, arrPos[i].transform.position, 30 * Time.deltaTime);
        }
        else {
          if (!arrValid.Contains(arrObj[i])) {
            arrValid.Add(arrObj[i]);
          }
        }
      }
      if (arrValid.Count == 6) {
        arrValid.Clear();
        Tus(ref kol);
      }
    }
  }

  private void Tus(ref int kol) {
    if(kol != 0) {
      src.GetComponent<AudioSource>().PlayOneShot(tusn);
      for (int i = 0; i < arrPos.Length; i++) {
        int j = Random.Range(0, i + 1);
        if (i != j)
          (arrPos[i], arrPos[j]) = (arrPos[j], arrPos[i]);
      }
      kol--;
      if(kol == 0) {
        Button_Vibor.list = new List<string>();
      }
    }
    if(kol == 0){
      src.GetComponent<AudioSource>().Stop();
    }
  }
}