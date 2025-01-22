using UnityEngine;
using System.Collections.Generic;

public class Meteor : MonoBehaviour {

  private static bool valid;

  public static List<AudioSource> listaudiometeor;

  private float timer = 5;
  private AudioSource src;

  public LayerMask layer;

  int flagspawn;

  private void Awake() {
    valid = false;
    if (listaudiometeor == null)
      listaudiometeor = new List<AudioSource>();
  }

  private void Update() {
    if(valid && listaudiometeor.Count > 0) {
      listaudiometeor[0].Play();
      valid = false;
    }
    timer -= Time.deltaTime;
    if(timer <= 0) {
      listaudiometeor.RemoveAt(0);
      Destroy(gameObject);
      valid = true;
      timer = 5f;
    }
  }

  public int Spawn() {
    Vector3 vec = Return_Vector_Spawna();
    if (flagspawn == 1 /*&& Input.touchCount == 1*/) {
      Manager.money -= 10;
      GameObject g = Instantiate(gameObject, vec, gameObject.transform.rotation);
      g.name = "Girja";
      src = g.GetComponent<AudioSource>();
      listaudiometeor.Add(src);
      if(listaudiometeor.Count == 1) {
        src.Play();
      }
      return flagspawn;
    }
    else {
      return flagspawn;
    }
  }

  public Vector3 Return_Vector_Spawna() {
    Vector3 vec;
    vec = new Vector3(0, 0, 0);
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition/*Input.GetTouch(0).position*/);
    RaycastHit placeInfo;
    if(!BlockInstrument.flagBlock) {
      if (Physics.Raycast(ray, out placeInfo) && placeInfo.collider.gameObject.name != "buttoncollider" && placeInfo.collider.gameObject.name != "buttoncolliderpause") {
        vec = new Vector3(placeInfo.point.x, GameObject.Find("Table").transform.position.y + 0.08827209f, placeInfo.point.z);
        flagspawn = 1;
      }
      else if (placeInfo.collider.gameObject.name == "buttoncollider") {
        flagspawn = -1;
      }
      else if (placeInfo.collider.gameObject.name == "buttoncolliderpause") {
        flagspawn = 0;
      }
    }
    else 
      flagspawn = 0;
    return vec;
  }
}