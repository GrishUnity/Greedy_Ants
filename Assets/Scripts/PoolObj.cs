using UnityEngine;
using System.Collections.Generic;

public class PoolObj : MonoBehaviour {

  public bool flagUpdagte = true;

  private GameObject updateGameObject;

  private AudioSource src;
  public AudioClip clip;

  public GameObject pointobject;
  public List<Transform> pointlist;

  public static PoolObj antRef;

  [SerializeField] private GameObject ant;
  [SerializeField] private GameObject antbron;
  [SerializeField] private GameObject antunosit;
  [SerializeField] private GameObject antmom;

  [SerializeField] private GameObject pop;
  public GameObject popspawn;

  public List<GameObject> listAntComlexity;
  public static Queue<GameObject> queueAntComlexity;
  public List<GameObject> MainListAnt;

  private void Awake() {
    queueAntComlexity = new Queue<GameObject>(listAntComlexity);
    MainListAnt = new List<GameObject>();
    src = gameObject.GetComponent<AudioSource>();
    antRef = this;
  }

  private void Start() {
    Vector3 rotate = transform.eulerAngles;
    rotate.y = Random.RandomRange(0, 360);
    pointobject.transform.rotation = Quaternion.Euler(rotate);
    Popcorn.listfreepop = new(6);

    for(int i = 0; i < 20; i++) {
      AddList(ant);
      if (i < 6) {
        GameObject obj = Instantiate(pop);
        obj.name = "Cake";
        Popcorn.listfreepop.Add(obj);
        obj.SetActive(false);
      }
    }
    SpawnPopcorn();
    MixAntPool(ref MainListAnt);
  }

  public GameObject GetBulletPrefabVolna() {
    GameObject obj = MainListAnt[0];
    for (int i = 0; i < MainListAnt.Count; i++) {
      MainListAnt.Add(MainListAnt[0]);
      MainListAnt.RemoveAt(0);
      if (!obj.activeInHierarchy && obj.GetComponent<Ant>().name != "AntMoney")
        return obj;
      obj = MainListAnt[0];
    }
    return null;
  }

  public GameObject GetBulletPrefab() {
    GameObject obj = MainListAnt[0];
    for (int i = 0; i < MainListAnt.Count; i++) {
      MainListAnt.Add(MainListAnt[0]);
      MainListAnt.RemoveAt(0);
      if (!obj.activeInHierarchy)
        return obj;
      obj = MainListAnt[0];
    }
    return null;
  }

  public static void MixAntPool(ref List<GameObject> poolQueue) {
    for (int i = 0; i < poolQueue.Count; i++) {
      int j = Random.Range(0, i + 1);
      if (j != i)
        (poolQueue[i], poolQueue[j]) = (poolQueue[j], poolQueue[i]);
    }
  }

  private void AddList(GameObject antbron, string name = "Ant") {
    GameObject obj = Instantiate(antbron);
    obj.name = name;
    obj.SetActive(false);
    MainListAnt.Add(obj);
  }

  public void SpawnPopcorn() {
    SpawnCakePortal.index = 0;
    src.PlayOneShot(clip);
    for (int i = 0; i < Popcorn.listfreepop.Count; i++) {
      Popcorn.listfreepop[i].transform.position = pointlist[i].position;
      Vector3 rotate1 = gameObject.transform.position;
      Vector3 rotate = new Vector3(90,0,0);
      rotate1.y = rotate.y = Random.RandomRange(0, 360);
      GameObject g = Instantiate(popspawn, Popcorn.listfreepop[i].transform.position, Quaternion.Euler(rotate));
      Popcorn.listfreepop[i].transform.rotation = Quaternion.Euler(rotate1);
      if (i % 2 == 0) {
        g.GetComponent<Animation>().Play("SpawnCake2");
      }
      else {
        g.GetComponent<Animation>().Play("SpawnCake");
      }
    }
  }

  public void UpdateList() {
    if(queueAntComlexity.Count != 0) {
      int lenght;
      int index = 0;
      GameObject g = queueAntComlexity.Dequeue();
      if (g.GetComponent<Ant>().name == "AntMoney") {
        lenght = 2;
      }
      else if(g.GetComponent<Ant>().name == "AntMom") {
        lenght = 2;
      }
      else if(g.GetComponent<Ant>().name == "AntUnosit") {
        lenght = 2;
      }
      else {
        lenght = 5;
      }
      for (int i = 0; i < lenght; i++) {
        int j = 0;
        for (; j < MainListAnt.Count; j++) {
          if (!MainListAnt[j].activeInHierarchy && MainListAnt[j].GetComponent<Ant>().name == "Ant") {
            updateGameObject = Instantiate(g);
            updateGameObject.SetActive(false);
            updateGameObject.name = "Ant";
            if (PlayInstrument.freez) {
              updateGameObject.GetComponent<Ant>().speed /= 4;
              updateGameObject.GetComponent<Ant>().speed_east /= 4;
              updateGameObject.GetComponent<Animator>().speed /= 4;
            }
            Destroy(MainListAnt[j]);
            MainListAnt[j] = updateGameObject;
            break;
          }
        }
      }
    }
  }
}