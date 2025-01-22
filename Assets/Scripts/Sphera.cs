using UnityEngine;
using UnityEngine.UI;

public class Sphera : MonoBehaviour {

  public GameObject instrumet;

  public float timer = 7.6f;
  public static bool flag;

  public GameObject table;

  public void Start() {
    gameObject.GetComponent<AudioSource>().Play();
    gameObject.GetComponent<Animation>().Play("SphereSpawn");
  }

  void Update() {
    if(instrumet != null) {
      instrumet.GetComponent<Image>().fillAmount -= 1 / 7.6f * Time.deltaTime;
    }
    timer -= Time.deltaTime;
    if (timer <= 1.3f) {
      if(!gameObject.GetComponent<Animation>().isPlaying)
        gameObject.GetComponent<Animation>().Play("SphereDead");
    }
  }

  private void OnTriggerEnter(Collider other) {
    if(other.name == "Ant")
      gameObject.GetComponent<AudioSource>().Play();
  }

  public void Spawn(Vector3 vec) {
    flag = true;
    Manager.money -= 25;
    GameObject gg = Instantiate(gameObject, new Vector3(-2, vec.y -0.854755f, -4), gameObject.transform.rotation);
    gg.name = "Sphera";
  }

  public void Dead() {
    flag = false;
    instrumet.GetComponent<Image>().fillAmount = 1;
    instrumet.gameObject.SetActive(false);
    Destroy(gameObject);
  }
}