using UnityEngine;

public class AntChild : Ant {

  private new void Update() {
    if (PlayInstrument.freez && speed == 7) {
      speed /= 4f;
      speed_east /= 4f;
      gameObject.GetComponent<Animator>().speed /= 4f;
    }
    else if(!PlayInstrument.freez && speed == 1.75f) {
      speed *= 4f;
      speed_east *= 4f;
      gameObject.GetComponent<Animator>().speed *= 4f;
    }
    if (Popcorn.listfreepop.Count != 0) {
      Move();
    }
  }

  private void OnMouseDown() {
    if (!PlayInstrument.flagINstrumentVibran && !ButtonCanvas.paus_flag) {
      Vector3 vec = gameObject.transform.position;
      vec.y += 1.2648649f;
      GameObject g = Instantiate(deadantparticle, vec, Quaternion.identity);
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
      Destroy(gameObject);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
      Spawn.StaticDamage.SetActive(false);
      Complexity.complexity2 += 1;
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
      Destroy(g, 1f);
      Destroy(gameObject);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
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
      Destroy(gameObject);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
    }
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
    Destroy(gameObject);
    Manager.count += Ochki * Manager.x;
    Complexity.count += Ochki;
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }
}