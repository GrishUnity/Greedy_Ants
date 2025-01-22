using UnityEngine;
using System.Collections.Generic;

public class Popcorn : MonoBehaviour {

  public float hp = 3;
  public GameObject cake;
  public GameObject cake12;
  public static List<GameObject> listfreepop;

  private void Update() {
    if(hp <= 2 && hp > 1) {
      if (cake.activeInHierarchy) {
        gameObject.name = "Cake12";
        cake.SetActive(false);
        cake12.GetComponent<Collider>().enabled = true;
      }
    }
    else if(hp <= 1) {
      if (cake12.activeInHierarchy) {
        gameObject.name = "Cake13";
        cake12.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = true;
      }
    }
    else {
      if (!cake.activeInHierarchy || !cake12.activeInHierarchy) {
        gameObject.name = "Cake";
        cake.SetActive(true);
        cake12.SetActive(true);
        cake12.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
      }
    }
  }
}