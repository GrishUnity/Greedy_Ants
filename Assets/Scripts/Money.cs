using UnityEngine;

public class Money : MonoBehaviour {

  public GameObject presled;

  public GameObject particleFollow;

  private float timer = 5f;

  public int m = 5;

  private void Awake() => GetComponent<Animation>().Play("Money");

  private void Update() {
    timer -= Time.deltaTime;
    if (presled != null) {
      if (gameObject.transform.position == presled.transform.position) {
        Destroy(gameObject);
      }
      else 
        gameObject.transform.SetPositionAndRotation(Vector3.MoveTowards(gameObject.transform.position, presled.transform.position, 75 * Time.deltaTime),
          Quaternion.LookRotation(gameObject.transform.position - presled.transform.position));
    }
    else if (timer <= 0)
      GetComponent<Animation>().Play("MoneyDead");
  }

  private void OnMouseDown() {
    if (!ButtonCanvas.paus_flag && !PlayInstrument.flagINstrumentVibran) {
      presled = GameObject.Find("MoneyCanvas");
      particleFollow.SetActive(true);
      particleFollow.GetComponent<AudioSource>().Play();
      gameObject.transform.localScale /= 2;
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.name == "Table") {
      gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    if (collision.gameObject.name == "MoneyCanvas") {
      Manager.money += m;
      m = 5;
      Destroy(gameObject);
    }
  }

  public void Delete() => Destroy(gameObject);
}