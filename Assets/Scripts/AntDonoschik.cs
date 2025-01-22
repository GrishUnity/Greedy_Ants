using UnityEngine;

public class AntDonoschik : Ant {

  public Material mat;
  public Color normal, nujnij;

  private new void Update() {
    if (gameObject.activeInHierarchy) {
      if (Popcorn.listfreepop.Count != 0)
        Move();
    }
    mat.color = Color.Lerp(normal, nujnij, Mathf.Abs(Mathf.Sin(2 * Time.time)));
  }

  public new void Move() {
    switch (state) {
      case "Search":
        Search();
        break;
      case "Follow":
        Run();
        break;
      case "East":
        Eats();
        break;
      default:
        break;
    }
  }

  private new void Eats() {
    if (stolknul != null) {
      particleEst.SetActive(true);
      if (!Spawn.StaticDamage.GetComponent<AudioSource>().isPlaying && !ButtonCanvas.paus_flag) {
        Spawn.StaticDamage.GetComponent<AudioSource>().Play();
      }
      else if (ButtonCanvas.paus_flag) {
        Spawn.StaticDamage.GetComponent<AudioSource>().Stop();
      }
      stolknul.GetComponent<Popcorn>().hp -= Time.deltaTime * speed_east;
      if (gameObject.activeInHierarchy)
        Spawn.StaticDamage.SetActive(true);
      if (stolknul.GetComponent<Popcorn>().hp <= 2 && stolknul.GetComponent<Popcorn>().hp > 1) {
        if (stolknul.GetComponent<Popcorn>().cake.activeInHierarchy) {
          state = "Search";
        }
      }
      else if (stolknul.GetComponent<Popcorn>().hp <= 1 && stolknul.GetComponent<Popcorn>().hp > 0) {
        if (stolknul.GetComponent<Popcorn>().cake12.activeInHierarchy) {
          state = "Search";
        }
      }
      else if (stolknul.GetComponent<Popcorn>().hp <= 0) {
        Destroy(stolknul);
        if (Popcorn.listfreepop.Contains(stolknul))
          Popcorn.listfreepop.Remove(stolknul);
        state = "Search";
      }
    }
    else
      state = "Search";
  }

  private void OnMouseDown() {
    if (!PlayInstrument.flagINstrumentVibran && !ButtonCanvas.paus_flag) {
      Vector3 vec = gameObject.transform.position;
      vec.y += 10.8f;
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
      Spawn.StaticDamage.SetActive(false);
      Complexity.complexity2 += 1;
    }
  }

  public override void InteractionInstrument(Collider other) {
    Vector3 vec = gameObject.transform.position;
    vec.y += 10.8f;
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
      Complexity.complexity2 += 1;
      Spawn.StaticDamage.SetActive(false);
    }
  }

  public override void OnParticleCollision(GameObject other) {
    Vector3 vec = gameObject.transform.position;
    vec.y += 10.8f;
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
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }
}