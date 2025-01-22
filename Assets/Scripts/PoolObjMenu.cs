using UnityEngine;
using System.Collections.Generic;
using YG;
using UnityEngine.UI;

public class PoolObjMenu : MonoBehaviour {

  public static PoolObjMenu ant;
  public List<GameObject> pool = new();
  public Queue<GameObject> poolQueue = new();

  public List<GameObject> speedlist;
  public List<GameObject> antanimspeedlist;

  [SerializeField] private GameObject antpref;

  public Image img;
  public Sprite ru;
  public Sprite en;
  public Text buttonTxt;

  private void Awake() {
    ant = this;
    if (YandexGame.EnvironmentData.language == "en") {
      if (PlayerPrefs.GetString("Langueg") == "Ru") {
        PlayerPrefs.SetString("Langueg", "Ru");
        buttonTxt.text = "Ру";
        img.sprite = ru;
      }
      else {
        PlayerPrefs.SetString("Langueg", "En");
        buttonTxt.text = "En";
        img.sprite = en;
      }
    }
    else if (YandexGame.EnvironmentData.language == "ru") {
      if (PlayerPrefs.GetString("Langueg") == "En") {
        PlayerPrefs.SetString("Langueg", "En");
        buttonTxt.text = "En";
        img.sprite = en;
      }
      else {
        PlayerPrefs.SetString("Langueg", "Ru");
        buttonTxt.text = "Ру";
        img.sprite = ru;
      }
    }
  }

  private void Start() {
    for (int i = 0; i < 10; i++) {
      AddQueue(antpref);
    }
    poolQueue = new Queue<GameObject>(pool);
    antanimspeedlist = new(poolQueue);
    speedlist = new(poolQueue);
  }

  public GameObject GetBulletPrefab() {
    GameObject obj = poolQueue.Dequeue();
    for (int i = 0; i < poolQueue.Count; i++) {
      poolQueue.Enqueue(obj);
      if (!obj.activeInHierarchy)
        return obj;
      obj = poolQueue.Dequeue();
    }
    poolQueue.Enqueue(obj);
    return null;
  }

  void AddQueue(GameObject antbron, string name = "Ant") {
    GameObject obj = Instantiate(antbron);
    obj.name = name;
    obj.SetActive(false);
    pool.Add(obj);
  }
}