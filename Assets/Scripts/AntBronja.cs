using UnityEngine;

public class AntBronja : Ant {

  private int hp = 3;
  public GameObject shet;
  public GameObject shlem;

  public GameObject particle1;
  public GameObject particle2;

  private void OnMouseDown() {
    if (!PlayInstrument.flagINstrumentVibran && !ButtonCanvas.paus_flag) {
      hp--;
      if (hp == 2) {
        shet.SetActive(false);
        particle1.SetActive(true);
        particle1.GetComponent<AudioSource>().Play();
        if (!PlayInstrument.freez) {
          speed += 1;
          gameObject.GetComponent<Animator>().speed += 0.25f;
        }
        else {
          speed += 0.25f;
          gameObject.GetComponent<Animator>().speed += 0.0625f;
        }
      }
      else if (hp == 1) {
        shlem.SetActive(false);
        particle2.SetActive(true);
        particle2.GetComponent<AudioSource>().Play();
        if (!PlayInstrument.freez) {
          speed += 1;
          gameObject.GetComponent<Animator>().speed += 0.25f;
        }
        else {
          speed += 0.25f;
          gameObject.GetComponent<Animator>().speed += 0.0625f;
        }

      }
      else if (hp <= 0) {
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
        shet.SetActive(true);
        shlem.SetActive(true);
        Manager.count += Ochki * Manager.x;
        Complexity.count += Ochki;
        hp = 3;
        particle1.SetActive(false);
        particle2.SetActive(false);
        if (!PlayInstrument.freez) {
          speed = 3;
          gameObject.GetComponent<Animator>().speed -= 0.5f;
        }
        else {
          speed = 0.75f;
          gameObject.GetComponent<Animator>().speed -= 0.125f;
        }
        Spawn.StaticDamage.SetActive(false);
        Complexity.complexity2 += 1;
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
      Destroy(g, 1f);
      gameObject.SetActive(false);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
      shet.SetActive(true);
      shlem.SetActive(true);
      hp = 3;
      if (!PlayInstrument.freez)
        speed = 3;
      else
        speed = 0.75f;
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
      Destroy(g, 1f);
      gameObject.SetActive(false);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
      shet.SetActive(true);
      shlem.SetActive(true);
      hp = 3;
      if (!PlayInstrument.freez)
        speed = 3;
      else
        speed = 0.75f;
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
    Destroy(g, 1f);
    gameObject.SetActive(false);
    shet.SetActive(true);
    shlem.SetActive(true);
    Manager.count += Ochki * Manager.x;
    Complexity.count += Ochki;
    hp = 3;
    if (!PlayInstrument.freez)
      speed = 3;
    else
      speed = 0.75f;
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }
  public override void DeadAntReastartReclama() {
    gameObject.SetActive(false);
    hp = 3;
    shet.SetActive(true);
    shlem.SetActive(true);
    particle1.SetActive(false);
    particle2.SetActive(false);
  }
}