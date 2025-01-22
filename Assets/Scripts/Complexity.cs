using UnityEngine;

public class Complexity : MonoBehaviour {

  public static int count;

  public bool flag_volna;
  private int comp = 100;
  private int shetchik;
  private int complexity = 30;
  public static int complexity2;

  void Start() {
    flag_volna = false;
    shetchik = comp;
    complexity2 = 0;
    count = 0;
  }

  void Update() {
    if (complexity2 >= complexity) {
      PoolObj.antRef.UpdateList();
      PoolObj.MixAntPool(ref PoolObj.antRef.MainListAnt);
      complexity += 30;   
      for (int i = 0; i < PoolObj.antRef.MainListAnt.Count; i++) {
        PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed += 0.20f;
        PoolObj.antRef.MainListAnt[i].GetComponent<Animator>().speed += 0.02f;
        PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed_east += 0.15f;
      }
      if(Spawn.time_spawna_static != 0.5f) {
        if (Spawn.time_spawna_static - 0.5f > 0.5f) {
          Spawn.time_spawna_static -= 0.5f;
        }
        else {
          Spawn.time_spawna_static -= Spawn.time_spawna_static - 0.5f;
        }
      }
    }
    if (count >= shetchik) {
      for (int i = 0; i < PoolObj.antRef.MainListAnt.Count; i++) {
        comp += PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().Ochki;
        PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed -= PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed / 100;
        PoolObj.antRef.MainListAnt[i].GetComponent<Animator>().speed -= PoolObj.antRef.MainListAnt[i].GetComponent<Animator>().speed / 100;
        PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed_east -= PoolObj.antRef.MainListAnt[i].GetComponent<Ant>().speed_east / 100;
      }
      shetchik += comp;
      flag_volna = true;
    }
  }
}