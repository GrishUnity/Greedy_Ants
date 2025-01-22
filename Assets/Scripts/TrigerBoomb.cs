using UnityEngine;

public class TrigerBoomb : MonoBehaviour {

  public Peper pep;

  public void OnTriggerEnter(Collider other) {
    if(other.name == "Ant") {
      pep.PeperDestroyAnt();
    }
  }
}