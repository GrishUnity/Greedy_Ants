using UnityEngine;

public class AudioPauseOf : MonoBehaviour {

  public static GameObject[] listAudio;

  private void Start() {
    listAudio = null;
  }

  public static void OfAudioPause() {
    if (listAudio == null) {
      listAudio = GameObject.FindGameObjectsWithTag("Effect");
      for (int i = 0; i < listAudio.Length; i++) {
        if (listAudio[i].activeInHierarchy && listAudio[i].GetComponent<AudioSource>().isPlaying)
          listAudio[i].GetComponent<AudioSource>().Pause();
      }
    }
    if (Meteor.listaudiometeor != null && Meteor.listaudiometeor.Count > 0)
      Meteor.listaudiometeor[0].Pause();
  }

  public static void OnAudioPause() {
    FonMusic mscfon = GameObject.FindGameObjectWithTag("Music")?.GetComponent<FonMusic>();
    if (PlayerPrefs.GetInt("Music") == 0)
      mscfon.src.volume = 0.5f;//////////////////////////////////////////////////////////
    if (listAudio != null) {
      for (int i = 0; i < listAudio.Length; i++) {
        listAudio[i]?.GetComponent<AudioSource>()?.Play();
      }
    }
    if (Meteor.listaudiometeor != null && Meteor.listaudiometeor.Count > 0)
      Meteor.listaudiometeor[0].Play();
    listAudio = null;
  }
}