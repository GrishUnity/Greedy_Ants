using UnityEngine;
using System.Collections.Generic;

public class Ant : AbstractAnt, IAnt {

  public string name;

  public int Ochki = 10;

  public GameObject particleEst;

  public Animator anim;

  public Material ochkimat;
  public Material ochkimatX2;
  public Material ochkimatX4;

  public GameObject deadantparticlesphera;
  public GameObject deadantparticle;

  protected float _hp_pop;

  private void Start() {
    anim = GetComponent<Animator>();
  }

  public void Update() {
    if (gameObject.activeInHierarchy) {
      if (Popcorn.listfreepop.Count != 0)
        Move();
    }      
  }

  public static GameObject SearchPoints(ref Queue<Transform> q_spawn_points_ants, string name = "") {
    Transform spawn;
    Transform spawn_scip;
    int length = Random.Range(2, 11);

    spawn = q_spawn_points_ants.Dequeue();
    while (spawn.tag == "Busy") {
      q_spawn_points_ants.Enqueue(spawn);
      spawn = q_spawn_points_ants.Dequeue();
    }

    if(name == "King") {
      while (spawn.name != name) {
        q_spawn_points_ants.Enqueue(spawn);
        spawn = q_spawn_points_ants.Dequeue();
      }
      spawn.tag = "Busy";
    }

    for (int i = 0; i < length; i++) {
      spawn_scip = q_spawn_points_ants.Dequeue();
      q_spawn_points_ants.Enqueue(spawn_scip);
    }

    q_spawn_points_ants.Enqueue(spawn);
    return spawn.gameObject;
  }

  public void Move() {
    if(!ButtonCanvas.paus_flag) {
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
  }

  public void Search() {
    particleEst.SetActive(false);
    float length = 0, min = 0;
    GameObject stop, interobj;

    stop = Popcorn.listfreepop[0];
    presled = interobj = Popcorn.listfreepop[0];
    Popcorn.listfreepop.Remove(interobj);
    if (gameObject != null && interobj != null) {
      min = length = Vector3.Distance(interobj.transform.position, gameObject.transform.position);
    }
    do {
      if (length < min) {
        min = length;
        presled = interobj;
      }
      Popcorn.listfreepop.Add(interobj);
      interobj = Popcorn.listfreepop[0];
      Popcorn.listfreepop.Remove(interobj);
      if (gameObject != null && interobj != null)
        length = Vector3.Distance(interobj.transform.position, gameObject.transform.position);
    } while (interobj != stop);

    Popcorn.listfreepop.Add(interobj);
    state = "Follow";
    anim.SetBool("isRun", true);
    anim.SetBool("isEst", false);
    if(gameObject.activeInHierarchy)
      Spawn.StaticDamage.GetComponent<ParticleSystem>().Play();
    else
      Spawn.StaticDamage.GetComponent<ParticleSystem>().Stop();
  }

  public void Run() {
    if (presled != null) {
      gameObject.transform.SetPositionAndRotation(Vector3.MoveTowards(gameObject.transform.position, presled.transform.position, (float)speed * Time.deltaTime),
        Quaternion.LookRotation(gameObject.transform.position - presled.transform.position));
    }
    else {
      state = "Search";
    }
  }

  public void Eats() {
    if (stolknul != null) {
      particleEst.SetActive(true);
      if(!Spawn.StaticDamage.GetComponent<AudioSource>().isPlaying && !ButtonCanvas.paus_flag) {
        Spawn.StaticDamage.GetComponent<AudioSource>().Play();
      }
      else if(ButtonCanvas.paus_flag) {
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

  public virtual void OnCollisionEnter(Collision collision) {
    if(presled == collision.gameObject) {
      stolknul = collision.gameObject;
      state = "East";
      anim.SetBool("isRun", false);
      anim.SetBool("isEst", true);
    }
  }

  public void OnTriggerEnter(Collider other) {
    InteractionInstrument(other);
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
      gameObject.SetActive(false);
      Manager.count += Ochki * Manager.x;
      Complexity.count += Ochki;
      Spawn.StaticDamage.SetActive(false);
      Spawn.StaticDamage.GetComponent<AudioSource>().Stop();
      Complexity.complexity2 += 1;
    }
  }

  public virtual void InteractionInstrument(Collider other) {
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
    }
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }

  public virtual void OnParticleCollision(GameObject other) {
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
    gameObject.SetActive(false);
    Manager.count += Ochki * Manager.x;
    Complexity.count += Ochki;
    Spawn.StaticDamage.SetActive(false);
    Complexity.complexity2 += 1;
  }

  public virtual void DeadAntReastartReclama() {
    gameObject.SetActive(false);
  }
}