using System.Collections.Generic;
using UnityEngine;

public class AntMomKnigth : Ant {

  public GameObject pointSpawn = null;
  private int hp = 4;
  public GameObject childmodel;
  public List<Transform> antchild;

  private void OnMouseDown() {
    if (!PlayInstrument.flagINstrumentVibran && !ButtonCanvas.paus_flag) {
      hp--;
      gameObject.GetComponent<AudioSource>().Play();
      if (hp <= 0) {
        Vector3 vec = gameObject.transform.position;
        vec.y += 1.2648649f;
        GameObject g = Instantiate(deadantparticle, vec, Quaternion.identity);
        if (Manager.x == 1) {
          g.GetComponent<Renderer>().material = ochkimat;
        }
        else if (Manager.x == 2) {
          g.GetComponent<Renderer>().material = ochkimatX2;
        }
        else if (Manager.x == 4) {
          g.GetComponent<Renderer>().material = ochkimatX4;
        }
        Destroy(g, 1f);
        if (pointSpawn != null)
          pointSpawn.tag = "Untagged";
        gameObject.SetActive(false);
        int k = 5;
        for (int i = 0; i < antchild.Count; i++) {
          bool flag = true;
          Vector3 vector = antchild[i].position;

          Collider[] overlappedColliders = Physics.OverlapSphere(vector, 5);
          for (int j = 0; j < overlappedColliders.Length; j++) {
            GameObject rd = overlappedColliders[j].gameObject;
            if (rd.name == "Cake") {
              flag = false;
              break;
            }
          }
          if (flag) {
            Instantiate(childmodel, antchild[i].position, Quaternion.identity).name = "Ant";
            k--;
            if (k == 0) {
              break;
            }
          }
        }
        Manager.count += Ochki * Manager.x;
        Complexity.count += Ochki;
        hp = 4;
        Spawn.StaticDamage.SetActive(false);
        Complexity.complexity2 += 1;
      }
    }
  }

  public override void InteractionInstrument(Collider other) {
    Vector3 vec = gameObject.transform.position;
    vec.y += 1.2648649f;
    if (other.name == "Sphera") {   
      GameObject g = Instantiate(deadantparticlesphera, vec, Quaternion.identity);
      g.GetComponent<AudioSource>().Play();
      if (Manager.x == 1) {
        g.GetComponent<Renderer>().material = ochkimat;
      }
      else if (Manager.x == 2) {
        g.GetComponent<Renderer>().material = ochkimatX2;
      }
      else if (Manager.x == 4) {
        g.GetComponent<Renderer>().material = ochkimatX4;
      }
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
      Destroy(g, 1f);
      gameObject.SetActive(false);
      int k = 5;
      for (int i = 0; i < antchild.Count; i++) {
        bool flag = true;
        Vector3 vector = antchild[i].position;

        Collider[] overlappedColliders = Physics.OverlapSphere(vector, 5);
        for (int j = 0; j < overlappedColliders.Length; j++) {
          GameObject rd = overlappedColliders[j].gameObject;
          if (rd.name == "Cake") {
            flag = false;
            break;
          }
        }
        if (flag) {
          Instantiate(childmodel, antchild[i].position, Quaternion.identity).name = "Ant";
          k--;
          if (k == 0) {
            break;
          }
        }
      }
    }
    else if (other.name == "ColliderAtom") {
      GameObject g = Instantiate(deadantparticle, vec, Quaternion.identity);
      if (Manager.x == 1) {
        g.GetComponent<Renderer>().material = ochkimat;
      }
      else if (Manager.x == 2) {
        g.GetComponent<Renderer>().material = ochkimatX2;
      }
      else if (Manager.x == 4) {
        g.GetComponent<Renderer>().material = ochkimatX4;
      }
      Destroy(g,1f);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
      gameObject.SetActive(false);
    }
    else if (other.name == "Peper") {
      vec = gameObject.transform.position;
      vec.y += 1.2648649f;
      GameObject g = Instantiate(deadantparticle, vec, Quaternion.identity);
      if (Manager.x == 1) {
        g.GetComponent<Renderer>().material = ochkimat;
      }
      else if (Manager.x == 2) {
        g.GetComponent<Renderer>().material = ochkimatX2;
      }
      else if (Manager.x == 4) {
        g.GetComponent<Renderer>().material = ochkimatX4;
      }
      Destroy(g, 1f);
      if (pointSpawn != null)
        pointSpawn.tag = "Untagged";
      gameObject.SetActive(false);
      int k = 5;
      for (int i = 0; i < antchild.Count; i++) {
        bool flag = true;
        Vector3 vector = antchild[i].position;

        Collider[] overlappedColliders = Physics.OverlapSphere(vector, 5);
        for (int j = 0; j < overlappedColliders.Length; j++) {
          GameObject rd = overlappedColliders[j].gameObject;
          if (rd.name == "Cake") {
            flag = false;
            break;
          }
        }
        if (flag) {
          Instantiate(childmodel, antchild[i].position, Quaternion.identity).name = "Ant";
          k--;
          if (k == 0) {
            break;
          }
        }
      }
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
    }
    hp = 4;
    if (pointSpawn != null)
      pointSpawn.tag = "Untagged";
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }

  public override void OnParticleCollision(GameObject other) {
    Vector3 vec = gameObject.transform.position;
    vec.y += 1.2648649f;
    GameObject g = Instantiate(deadantparticle, vec, Quaternion.identity);
    if (Manager.x == 1) {
      g.GetComponent<Renderer>().material = ochkimat;
    }
    else if (Manager.x == 2) {
      g.GetComponent<Renderer>().material = ochkimatX2;
    }
    else if (Manager.x == 4) {
      g.GetComponent<Renderer>().material = ochkimatX4;
    }
    Destroy(g, 1f);
    Manager.count += Ochki * Manager.x;
    Complexity.count += Ochki;
    int k = 5;
    for (int i = 0; i < antchild.Count; i++) {
      bool flag = true;
      Vector3 vector = antchild[i].position;

      Collider[] overlappedColliders = Physics.OverlapSphere(vector, 5);
      for (int j = 0; j < overlappedColliders.Length; j++) {
        GameObject rd = overlappedColliders[j].gameObject;
        if (rd.name == "Cake") {
          flag = false;
          break;
        }
      }
      if (flag) {
        Instantiate(childmodel, antchild[i].position, Quaternion.identity).name = "Ant";
        k--;
        if (k == 0) {
          break;
        }
      }
    }
    if (pointSpawn != null)
      pointSpawn.tag = "Untagged";
    gameObject.SetActive(false);
    hp = 4;
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }

  public override void DeadAntReastartReclama() {
    hp = 4;
    base.DeadAntReastartReclama();
  }
}