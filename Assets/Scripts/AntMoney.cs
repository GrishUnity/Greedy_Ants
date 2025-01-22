using UnityEngine;

public class AntMoney : Ant {

  public int hp = 2;
  public GameObject money;
  public GameObject money_chek;

  private void OnMouseDown() {
    if (!PlayInstrument.flagINstrumentVibran && !ButtonCanvas.paus_flag) {
      hp--;
      if (hp == 1) {
        money_chek.SetActive(false);
        Vector3 vec = gameObject.transform.position;
        gameObject.GetComponent<AudioSource>().Play();
        vec.y += 2;
        Instantiate(money, vec, Quaternion.identity).name = "Money";
        if (!PlayInstrument.freez) {
          speed += 1;
          gameObject.GetComponent<Animator>().speed += 0.3f;
        }
        else {
          speed += 0.25f;
          gameObject.GetComponent<Animator>().speed += 0.075f;
        }
      }
      if (hp <= 0) {
        Vector3 vec = gameObject.transform.position;
        vec.y += 4f;
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
        gameObject.SetActive(false);
        money_chek.SetActive(true);
        Manager.count += Ochki * Manager.x;
        Complexity.count += Ochki;
        hp = 2;
        if (!PlayInstrument.freez) {
          speed = 4;
          gameObject.GetComponent<Animator>().speed -= 0.3f;
        }
        else {
          speed = 1f;
          gameObject.GetComponent<Animator>().speed -= 0.075f;
        }
        Complexity.complexity2 += 1;
        Spawn.StaticDamage.SetActive(false);
      }
    }
  }

  public override void InteractionInstrument(Collider other) {
    Vector3 vec = gameObject.transform.position;
    vec.y += 4f;
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
      gameObject.SetActive(false);
      money_chek.SetActive(true);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
      Destroy(g, 1f);
    }
    else if (other.name == "ColliderAtom" || other.name == "Peper") {
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
      gameObject.SetActive(false);
      money_chek.SetActive(true);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
      hp = 2;
      if (!PlayInstrument.freez)
        speed = 4;
      else
        speed = 1f;
    }
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }

public override void OnParticleCollision(GameObject other) {
    Vector3 vec = gameObject.transform.position;
    vec.y += 4f;
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
    if(gameObject.activeInHierarchy && hp > 1) {
      vec = gameObject.transform.position;
      vec.y += 2;
      Instantiate(money, vec, Quaternion.identity).name = "Money";
    }
    Destroy(g,1f);
    gameObject.SetActive(false);
    money_chek.SetActive(true);
    Manager.count += Ochki * Manager.x;
    Complexity.count += Ochki;
    hp = 2;
    if (!PlayInstrument.freez)
      speed = 4;
    else
      speed = 1f;
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }

  public override void DeadAntReastartReclama() {
    hp = 2;
    gameObject.SetActive(false);
    money_chek.SetActive(true);
  }
}