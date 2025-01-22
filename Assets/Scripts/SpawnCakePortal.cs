using UnityEngine;

public class SpawnCakePortal : MonoBehaviour {

  public static int index;
  
  public void DeleTe() {
    index = 0;
    Destroy(gameObject);
  }

  public void SpawnCake() {
    Popcorn.listfreepop[index].SetActive(true);
    index++;
  }
}