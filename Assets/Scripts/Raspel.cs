using UnityEngine;

public class Raspel : MonoBehaviour {

  public bool flagpshik = false;

  private GameObject rspdelet;

  public static Raspel rsp;

  private void Awake() => rsp = this;

  private Vector3 vec;
  public bool flagwork = false;
  public int flagspawn = 10;
  private float timer = 5f;

  public ParticleSystem smoke;

  void Update() {
    if(!ButtonCanvas.paus_flag) {
      vec = Return_Vector_Spawna();
      if (flagspawn == 1 /*&& Input.touchCount == 1*/ && flagwork) {
        gameObject.transform.position = vec;
        if (!flagpshik) {
          gameObject.GetComponent<AudioSource>().Play();
          flagpshik = true;
        }
        if (Input.touchCount == 1) {
          if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            smoke.Stop();
            gameObject.GetComponent<AudioSource>().Pause();
          }
          else if (Input.GetTouch(0).phase == TouchPhase.Began) {
            smoke.Play();
            gameObject.GetComponent<AudioSource>().Play();
          }
        }
      }
      else {
        gameObject.transform.position = new Vector3(0, -100, 0);
        gameObject.GetComponent<AudioSource>().Pause();
        flagpshik = false;
      }
      if (!BlockInstrument.flagBlock) {
        timer -= Time.deltaTime;
      }
      if (timer <= 0) {
        flagwork = false;
        Destroy(gameObject, 3);
      }
    }
  }

  public void DestroyY() {
    flagwork = false;
    Destroy(rspdelet, 3);
  }

  public void Spawn() {
    Vector3 vec = Return_Vector_Spawna();
    if(flagspawn == 1) {
      Manager.money -= 20;
      flagwork = true;
      rspdelet = Instantiate(gameObject, vec, Quaternion.identity);
      rsp = rspdelet.GetComponent<Raspel>();
      rspdelet.name = "Raspel";
      rspdelet.GetComponent<AudioSource>().Play();
      flagpshik = true;
    }
  }

  public Vector3 Return_Vector_Spawna() {
    Vector3 vec;
    vec = new Vector3(0, 0, 0);
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit placeInfo;
    if(!BlockInstrument.flagBlock) {
      if (Physics.Raycast(ray, out placeInfo) && placeInfo.collider.gameObject.name != "buttoncollider" && placeInfo.collider.gameObject.name != "buttoncolliderpause") {
        vec = new Vector3(placeInfo.point.x, GameObject.Find("Table").transform.position.y + 1f, placeInfo.point.z);
        flagspawn = 1;
      }
      else if (placeInfo.collider?.gameObject?.name == "buttoncollider") {
        flagspawn = -1;
      }
      else {
        flagspawn = 0;
      }
    }
    else 
      flagspawn = 0;
    return vec;
  }
}