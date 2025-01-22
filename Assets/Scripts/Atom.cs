using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour {

  public Collider col;
  private Vector3 backPoint;
  public bool flagboom = false;
  public GameObject vzrivcanvas;
  public GameObject vzrivparticle;

  public GameObject particleDeadCakeDinamit;

  private GameObject g;
  private Rigidbody _rigidbody;

  public float Force = 1;
  public float Radius = 55;

  private void Start() {
    backPoint = gameObject.transform.position;
    _rigidbody = gameObject.GetComponent<Rigidbody>();
    _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -50f, _rigidbody.velocity.z);
  }

  public void OnTriggerEnter(Collider other) {
    if (!flagboom) {
      if (other.gameObject.name != "Sphera") {
        if (Random.Range(1, 3) == 2) {
          Vector3 vec = Popcorn.listfreepop[0].transform.position;
          vec.y += 2f;
          GameObject g = Instantiate(particleDeadCakeDinamit, vec, Quaternion.identity);
          Destroy(g, 2f);
          Destroy(Popcorn.listfreepop[0], 1.5f);
        }
      }
      vzrivcanvas.SetActive(true);
      vzrivcanvas.GetComponent<ParticleSystem>().Play();
      g = Instantiate(vzrivparticle, new Vector3(-1.555556f, other.transform.position.y + 7f, -7.18433f), new Quaternion(0.175541356f, 0, 0, 0.984472096f));
      g.GetComponent<AudioSource>().Play();
      Destroy(g, 3f);
      Explode();
    }
  }

  public void DestroyAtom() {
    vzrivcanvas.SetActive(false);
    gameObject.SetActive(false);
    gameObject.transform.position = backPoint;
    _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -50f, _rigidbody.velocity.z);
    flagboom = false;
  }

  public void Explode() {
    if (!flagboom) {
      flagboom = true;
      Collider[] overlappedColliders = Physics.OverlapSphere(new Vector3(-2, 0, -3.75f), Radius);
      List<GameObject> obj = new List<GameObject>();

      for (int i = 0; i < overlappedColliders.Length; i++) {

        GameObject rd = overlappedColliders[i].gameObject;

        if (rd != null && rd.activeInHierarchy) {
          if(!obj.Contains(rd)) {
            if (rd.name == "Ant") {
              col.name = "ColliderAtom";
              rd.GetComponent<Ant>().InteractionInstrument(col);
            }
            else if (rd.name == "Box") {
              rd.GetComponent<Animation>().Play("BoxOfPandora_Crash");
            }
            else if (rd.name == "Money") {
              Destroy(rd);
            }
            else if(rd.name == "Peper") {
              rd.GetComponent<Peper>().PeperDestroyAnt();
            }
            obj.Add(rd);
          }
        }
      }

    }
  }
}