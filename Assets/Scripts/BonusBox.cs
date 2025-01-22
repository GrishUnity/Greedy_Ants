using UnityEngine;

public class BonusBox : MonoBehaviour {

  public AudioClip clipAdd;
  public AudioClip clipSub;
  public AudioClip clip;

  public bool flaglomaetsa = false;

  public bool flag = false;

  public static bool flagX;
  public GameObject money;

  private float timer = 7f;

  public GameObject particlehealcake;
  public GameObject particlex2;
  public GameObject particlex4;

  public void GetBonus() {
    int chanse = Random.Range(1, 3);
    if(chanse == 1) { //бонуска
      chanse = Random.Range(1, 101);
      if (chanse >= 1 && chanse <= 30) { // 30%
        Vector3 vec = gameObject.transform.position;
        vec.y += 1.25f;
        Instantiate(money, vec, Quaternion.identity).GetComponent<Money>().m = 10;
        gameObject.GetComponent<AudioSource>().PlayOneShot(clipAdd);
      }
      else if (chanse >= 31 && chanse <= 51) { // 20%
        int chanse1 = Random.Range(1, 101);
        Vector3 vec = gameObject.transform.position;
        vec.y += 1.62742f;
        GameObject a;
        if (chanse1 <= 30) {
          a = Instantiate(particlex4, vec, Quaternion.identity);
          Manager.x *= 4;
        }
        else {
          a = Instantiate(particlex2, vec, Quaternion.identity);
          Manager.x *= 2;
        }
        flagX = true;
        gameObject.GetComponent<AudioSource>().PlayOneShot(clipAdd);
        Destroy(a, 1.5f);
      }
      else if (chanse >= 52 && chanse <= 67) { // 15%
        GameObject g = Instantiate(particlehealcake, new Vector3(-1.55555558f, gameObject.transform.position.y + 0.25f, -4.43432999f), Quaternion.identity);
        Destroy(g, 4.5f);
        for (int i = 0; i < Popcorn.listfreepop.Count; i++) {
          Popcorn.listfreepop[i].GetComponent<Popcorn>().hp = 3;
        }
      }
      else { // 32%
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
      }
    }
    else { //дебонус
      chanse = Random.Range(1, 101);
      if (chanse >= 1 && chanse <= 40) { // 40%
        GameObject g = PoolObj.antRef.GetBulletPrefab();
        if (g != null) {
          g.transform.position = gameObject.transform.position;
          if (g.GetComponent<AntMomKnigth>() != null)
            g.GetComponent<AntMomKnigth>().pointSpawn = null;
          g.SetActive(true);
          g.GetComponent<Ant>().state = "Search";
        }
        gameObject.GetComponent<AudioSource>().PlayOneShot(clipSub);
      }
      else if (chanse >= 41 && chanse <= 71) { // 30%
        Fleshka.flagStart = true;
        Fleshka.startTime = Time.time;
      }
      else if (chanse >= 71 && chanse <= 86) { // 15 %
        Vector3 vec = gameObject.transform.position;
        vec.y += 1.62742f;
        BlockInstrument.blok.gameObject.transform.position = vec;
        BlockInstrument.blok.gameObject.SetActive(true);
        BlockInstrument.blok.flag = true;
        gameObject.GetComponent<AudioSource>().PlayOneShot(clipSub);
      }
      else { // 13 %
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
      }
    } 
  }

  private void Update() {
    timer -= Time.deltaTime;
    if (timer <= 0) {
      flaglomaetsa = true;
      gameObject.GetComponent<Animation>().Play("BoxOfPandora_Crash");
      timer = 7f;
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.name == "Ant") {
      gameObject.GetComponent<Animation> ().Play("BoxOfPandora_Crash");
      timer = 7f;
      flaglomaetsa = true;
    }
    else {
      gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }
  }

  private void OnMouseDown() {
    if(!flaglomaetsa && !flag && !ButtonCanvas.paus_flag && !PlayInstrument.flagINstrumentVibran) {
      flag = true;
      gameObject.layer = 6;
      GetBonus();
      gameObject.GetComponent<Animation>().Play("BoxOfPandora_Crash");
      timer = 7f;
    }
  }
}