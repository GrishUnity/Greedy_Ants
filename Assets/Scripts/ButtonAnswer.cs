using UnityEngine;
using UnityEngine.UI;

public class ButtonAnswer : MonoBehaviour {

  public AudioClip clipPerevorot;
  private ZvukButton clickButton;
  public AudioClip clip;

  public Text txt;
  public GameObject on;

  public Text opisanije;
  public GameObject AnswerPanel;
  public Text nameInstrument;

  public void Start() {
    GameObject g1 = GameObject.FindGameObjectWithTag("ZvukButton");
    clickButton = g1.GetComponent<ZvukButton>();
  }

  public void PanelVisible() {
    clickButton.ClickButton(clip);
    nameInstrument.text = txt.text;
    if (PlayerPrefs.GetString("Langueg") == "Ru") {
      switch (txt.text) {
        case "���������":
          opisanije.text = "��������� �������� �������� �� 5 ������, � ��� ����� �� ��� ����� �������";
          break;
        case "�����������":
          opisanije.text = "������� �� ������, ����������� ��� ������� ���������� ��������";
          break;
        case "���������":
          opisanije.text = "� ������������ ������� �������� ������ ��������� � ������� 5 ������, ��� ��������� ��������� � �������, ������� ������������";
          break;
        case "��������":
          opisanije.text = "���������� ��� �������� � ��������, � ������� 7 ������ �������� ����������������, ��� ������� ������� ��������� � ������� ��������, ����������, ������ ��������� ��� ��� �������� ������ ������ �������� � ������ �� ��������";
          break;
        case "�����":
          opisanije.text = "������������ �������� � �������� ���� ������ � ������� 5 ������, ��� ��������������� ������� � ������, ������� ������������";
          break;
        case "�������":
          opisanije.text = "�������� ���� �������� � ������, � ������������ 50% ����� ���������� ���� �� �������";
          break;
      }
    }
    else {
      switch (txt.text) {
        case "Freezing":
          opisanije.text = "Slows down the speed of ants by 5 seconds, at which time it is easier to hit them";
          break;
        case "The sprayer":
          opisanije.text = "By swiping across the screen, a gas is sprayed that destroys the ants";
          break;
        case "Meteorites":
          opisanije.text = "Meteorites begin to fall within a certain radius within 5 seconds, when a meteorite hits an ant, the ant is destroyed";
          break;
        case "The bomb":
          opisanije.text = "Explodes upon contact with an ant, within 7 seconds the bomb self-destructs, all ants that are in range explode, be careful as the bomb spoils the pies that fall within its radius of action";
          break;
        case "Sphere":
          opisanije.text = "Prevents ants and protects your pies for 5 seconds, when the ant comes into contact with the sphere, the ant is destroyed";
          break;
        case "Dynamite":
          opisanije.text = "Explodes all the ants in the area, with a 50% chance it can destroy one of the pies";
          break;
      }
    }
    AnswerPanel.SetActive(true);
  }

  public void PanelNotVisible() {
    clickButton.ClickButton(clip);
    AnswerPanel.GetComponent<Animation>().Play("PanelAnswerInVisible");
  }
} 