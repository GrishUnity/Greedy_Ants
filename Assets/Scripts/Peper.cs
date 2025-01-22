using System.Collections.Generic;
using UnityEngine;

public class Peper : MonoBehaviour {

  public GameObject particleFitel;

  private bool flagBoom = false;

  private float timer = -1;
  private int flagspawn;
  public GameObject particlecircle;
  public GameObject particledestroy;

  private GameObject particle1;
  private GameObject particle2 = null;

  private void Update() {
    if(particle2 != null) {
      Vector3 vec = gameObject.transform.position;
      vec.y -= 0.5f;
      particle2.transform.position = vec;
    }
    if(timer != -1) {
      timer -= Time.deltaTime;
      if (timer <= 0.5 && timer > 0) {
        particleFitel.SetActive(false);
      }
      if (timer <= 0) {
        PeperDestroyAnt();
      }
    }
  }

  public void PeperDestroyAnt() {
    flagBoom = true;
    Vector3 vec = gameObject.transform.position;
    vec.y += 0.63569999f;
    particle1 = Instantiate(particledestroy, vec, Quaternion.identity);
    particle1.GetComponent<AudioSource>().Play();
    Destroy(particle1, 2.5f);
    Destroy(particle2);
    Destroy(gameObject);
    Collider[] overlappedColliders = Physics.OverlapSphere(gameObject.transform.position, 7f);
    List<GameObject> obj = new List<GameObject>();
    for (int i = 0; i < overlappedColliders.Length; i++) {
      GameObject rd = overlappedColliders[i].gameObject;
      if (rd != null && rd.activeInHierarchy) {
        if (rd.name == "Ant" && !obj.Contains(rd)) {
          obj.Add(rd);
          Collider col = gameObject.GetComponent<Collider>();
          col.name = "Peper";
          rd.GetComponent<Ant>().InteractionInstrument(col);
          if (rd.GetComponent<AntMoney>() != null && rd.GetComponent<AntMoney>().hp == 1) {
            vec = gameObject.transform.position;
            vec.y += 2;
            Instantiate(rd.GetComponent<AntMoney>().money, vec, Quaternion.identity).name = "Money";
          }
        }
        else if (rd.name == "Box" && !obj.Contains(rd)) {
          rd.GetComponent<Animation>().Play("BoxOfPandora_Crash");
          obj.Add(rd);
        }
      }
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if(collision.gameObject.name == "Table" && timer == -1) {
      Vector3 vec = gameObject.transform.position;
      vec.y += collision.gameObject.transform.position.y + 0.6f;
      particle2 = Instantiate(particlecircle, vec, Quaternion.identity);
      particle2.GetComponent<TrigerBoomb>().pep = gameObject.GetComponent<Peper>();
      Destroy(particle2, 10f);
      timer = 7;
    }
    if (collision.gameObject.name == "Cake12" || collision.gameObject.name == "Cake" || collision.gameObject.name == "Cake13") {
      collision.gameObject.GetComponent<Popcorn>().hp -= 1;
      PeperDestroyAnt();
      if (collision.gameObject.GetComponent<Popcorn>().hp <= 0) {
        Destroy(collision.gameObject);
        if (Popcorn.listfreepop.Contains(collision.gameObject))
          Popcorn.listfreepop.Remove(collision.gameObject);
      }
      timer = 7;
    }
    else if(collision.gameObject.name != "Table" && collision.gameObject.name != "Box") {
      if(!flagBoom) {
        PeperDestroyAnt();
        timer = 7;
      }
    }
  }

  public int Spawn() {
    Vector3 vec = Return_Vector_Spawna();
    Vector3 rotate = transform.eulerAngles;
    rotate.y = Random.RandomRange(0, 360);
    if (flagspawn == 1 /*&& Input.touchCount == 1*/) {
      Manager.money -= 15;
      Instantiate(gameObject, vec, Quaternion.Euler(rotate)).name = "Peper";
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
      if (Physics.Raycast(ray, out placeInfo)) {
        if (placeInfo.collider.gameObject.name != "buttoncollider" && placeInfo.collider.gameObject.name != "buttoncolliderpause" && placeInfo.collider.gameObject.name != "Sphera") {
          if (placeInfo.collider.gameObject.name == "Box") {
            if (placeInfo.collider.gameObject.layer != 11)
              placeInfo.collider.gameObject.GetComponent<Animation>().Play("BoxOfPandora_Crash");
          }
          vec = new Vector3(placeInfo.point.x, GameObject.Find("Table").transform.position.y + 0.08827209f, placeInfo.point.z);
          flagspawn = 1;
        }
        else if (placeInfo.collider.gameObject.name == "buttoncollider") {
          flagspawn = -1;
        }
        else if (placeInfo.collider.gameObject.name == "buttoncolliderpause" || placeInfo.collider.gameObject.name == "Sphera") {
          flagspawn = 0;
        }
      }
    }
    else 
      flagspawn = 0;
    return vec;
  }
}