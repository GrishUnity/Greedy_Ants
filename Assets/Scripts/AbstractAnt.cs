using UnityEngine;

public abstract class AbstractAnt : MonoBehaviour {

  public float speed = 5f;
  public float speed_east = 3f;
  public string state = "Search";

  public GameObject presled;
  protected GameObject stolknul;
}