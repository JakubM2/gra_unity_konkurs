using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour {

    private GameMaster gm;

    //strings
    public string A = "Witaj Czarodzieju! [Enter]";
    public string B = "drugi [Enter]";
    public string C = "trzeci [Enter]";
    public string D = "czwarty [Enter]";
    public string E = "piąty [Enter]";

    //value
    int DialoguesIndex = 0;


    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void Update()
    {
        switch(DialoguesIndex)
        {
            default:
            case 0:
                gm.DialoguesText.text = A;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    addone();
                    Debug.Log("Udało się");
                }
                break;
            case 1:
                gm.DialoguesText.text = B;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    addone();
                }
                break;
            case 2:
                gm.DialoguesText.text = C;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    addone();
                }
                break;
            case 3:
                gm.DialoguesText.text = D;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    addone();
                }
                break;
            case 4:
                gm.DialoguesText.text = E;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    addone();
                }
                break;
            case 5:
                gm.DialoguesText.text = " ";
                //Destroy(gm.DialoguesText); //opcjonalnie nie niszczyć, a dawac wartość " " do wyświetlenia
                //Destroy(this);
                break;
        }
    }

    public void addone()
    {
        DialoguesIndex += 1;
    }
}