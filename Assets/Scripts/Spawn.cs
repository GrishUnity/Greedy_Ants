using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {

  public Complexity comp;

  public AudioClip clipGame;
  public FonMusic mscfon;

  public GameObject textVolna;

  public GameObject Damage;
  public static GameObject StaticDamage;

  public GameObject box;
  public GameObject spawnbox;

  private GameObject bullet;
  public static float time_spawna_static;
  public static float time_spawna;
  private float time_spawna_volna = 3f;

  private float timerboxspawn = 10f;
  public List<Transform> listbox;

  public static Queue<Transform> q_spawn_points_ants;

  public Popcorn pop;
  public Text[] price;
  public Text[] button;
  public List<Transform> spawnpointant;

  public GameObject[] image;
  public Sprite freez, smoke, meteor, sphera, dinamit, pep;

  private void Start() {
    mscfon = GameObject.FindGameObjectWithTag("Music")?.GetComponent<FonMusic>();
    if (PlayerPrefs.GetInt("Music") == 0) {
      mscfon.PlayClip(clipGame);
      mscfon.src.volume = 0.5f;///////////////////////////////////////////////////////
    }
    else {
      mscfon.PlayClip(clipGame);
      mscfon.src.volume = 0f;
    }
    comp.flag_volna = false;
    time_spawna = 3f;
    time_spawna_static = 2f;
    StaticDamage = Damage;
    Time.timeScale = 1f;
    q_spawn_points_ants = new Queue<Transform>(spawnpointant);

    Transform spawn = null;
    int randomIndex = Random.Range(1, 16);
    for (int i = 0; i < randomIndex; i++) {
      spawn = q_spawn_points_ants.Dequeue();
      q_spawn_points_ants.Enqueue(spawn);
    }

    for (int i = 0; i < 3; i++) {
      if (PlayerPrefs.GetString("Langueg") == "Ru") {
        button[i].text = Button_Vibor.list[i];
        switch (button[i].text) {
          case "Заморозка":
            price[i].text = "5";
            image[i].GetComponent<Image>().sprite = freez;
            break;
          case "Распылитель":
            price[i].text = "20";
            image[i].GetComponent<Image>().sprite = smoke;
            break;
          case "Метеориты":
            price[i].text = "10";
            image[i].GetComponent<Image>().sprite = meteor;
            break;
          case "Сфера":
            price[i].text = "25";
            image[i].GetComponent<Image>().sprite = sphera;
            break;
          case "Бомбочка":
            price[i].text = "15";
            image[i].GetComponent<Image>().sprite = pep;
            break;
          case "Динамит":
            price[i].text = "35";
            image[i].GetComponent<Image>().sprite = dinamit;
            break;
          default:
            price[i].text = "";
            break;
          }
        }
      else {
        button[i].text = Button_Vibor.list[i];
        switch (button[i].text) {
          case "Freezing":
            price[i].text = "5";
            image[i].GetComponent<Image>().sprite = freez;
            break;
          case "The sprayer":
            price[i].text = "20";
            image[i].GetComponent<Image>().sprite = smoke;
            break;
          case "Meteorites":
            price[i].text = "10";
            image[i].GetComponent<Image>().sprite = meteor;
            break;
          case "Sphere":
            price[i].text = "25";
            image[i].GetComponent<Image>().sprite = sphera;
            break;
          case "The bomb":
            price[i].text = "15";
            image[i].GetComponent<Image>().sprite = pep;
            break;
          case "Dynamite":
            price[i].text = "35";
            image[i].GetComponent<Image>().sprite = dinamit;
            break;
          default:
            price[i].text = "";
            break;
        }
      }
    }
  }

  void Update() {
    if(!comp.flag_volna) {
      time_spawna -= Time.deltaTime;
    }
    timerboxspawn -= Time.deltaTime;
    if(timerboxspawn <= 0) {
      if(Random.Range(1, 2) == 1) {
        SpawnBox();
        timerboxspawn = 15f;
      }
    }
    if (comp.flag_volna) {
      textVolna.SetActive(true);
      time_spawna_volna -= Time.deltaTime;
      if (time_spawna_volna <= 0) {
        List<Transform> list = new List<Transform>(q_spawn_points_ants);
        for (int i = 0; i < list.Count; i++) {
          if (list[i].tag != "Busy") {
            bullet = PoolObj.antRef.GetBulletPrefabVolna();
            if(bullet != null) {
              if (bullet.GetComponent<AntMomKnigth>() != null) {
                bullet.GetComponent<AntMomKnigth>().pointSpawn = list[i].gameObject;
                list[i].tag = "Busy";
                bullet.transform.position = list[i].position;
                bullet.SetActive(true);
                bullet.GetComponent<Ant>().state = "Search";
              }
              else {
                bullet.transform.position = list[i].position;
                bullet.SetActive(true);
                bullet.GetComponent<Ant>().state = "Search";
              }
            }
          }
        }
        comp.flag_volna = false;
        time_spawna_volna = 3f;
        time_spawna += 3f;
      }
    }
    else if (time_spawna <= 0) {
      bullet = PoolObj.antRef.GetBulletPrefab();
      IsValidObjectNull(bullet);
      time_spawna = time_spawna_static;
    }
  }

  private void IsValidObjectNull(GameObject bullet) {
    if (bullet != null) {
      if (bullet.gameObject.GetComponent<AntMomKnigth>() != null) {
        GameObject spawnPoint = Ant.SearchPoints(ref q_spawn_points_ants, "King");
        bullet.transform.position = spawnPoint.transform.position;
        bullet.GetComponent<AntMomKnigth>().pointSpawn = spawnPoint;
      }
      else {
        bullet.transform.position = Ant.SearchPoints(ref q_spawn_points_ants).transform.position;
      }
      bullet.SetActive(true);
      bullet.GetComponent<Ant>().state = "Search";
    }
  }

  void SpawnBox() {
    bool flag = true;
    Vector3 vec = listbox[Random.Range(0, listbox.Count)].position;

    Collider[] overlappedColliders = Physics.OverlapSphere(vec, 2);
    for (int i = 0; i < overlappedColliders.Length; i++) {
      GameObject rd = overlappedColliders[i].gameObject;
      if (rd.name == "Ant" || rd.name == "AntP" || rd.name == "Money" || rd.name == "Peper") {
        flag = false;
        break;
      }
    }

    if(flag) {
      GameObject g = Instantiate(box, vec, new Quaternion(0, 0.999986053f, 0, 0.00528121181f));
      g.name = "Box";
      g.GetComponent<AudioSource>().Play();
      vec.y += 4f;
      g = Instantiate(spawnbox, vec, Quaternion.identity);
      Destroy(g, 2.5f);
    }
  }
}