using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

    private int TOTAL_BUTTONS = 2;

    private Button soundButton;
    private int correctButtonIndex;
    private string correctAnimalName;
    private AudioSource source;
    private AudioClip correctAnimalSound;
    public Button[] buttons;
    public Sprite[] animals;
    public AudioClip[] sounds;
    public List<Sprite> gameAnimals = new List<Sprite>();

    private Text textField;

    void Awake()
    {
        animals = Resources.LoadAll<Sprite>("Sprites/Animals");
        sounds = Resources.LoadAll<AudioClip>("Sounds");
    }

	void Start () 
    {
        GetButtons();
        AddListeners();
        Reload();
	}

    void Reload()
    {
        destroySound();
        AddAnimals();
        AddSound();
        Invoke("clearText", 2);
    }

    void clearText()
    { this.textField.text = "";
    }

    void AddAnimals()
    {
        int randomAnimalIndex = Random.Range(0, animals.Length);
        this.correctAnimalName = animals[randomAnimalIndex].name;

		//Adicione o sprite do animal correto ao botão correto gerado aleatoriamente
        int randomButtonIndex = Random.Range(0, buttons.Length);
        correctButtonIndex = randomButtonIndex;
        buttons[correctButtonIndex].image.sprite = animals[randomAnimalIndex];

		//Adicione animais errados aos botões restantes
        for (int i = 0; i < buttons.Length; ++i)
        {
            if (i != correctButtonIndex)
            {
                int incorrectAnimalIndex;
                do{incorrectAnimalIndex = Random.Range(0, animals.Length);
                } while (randomAnimalIndex == incorrectAnimalIndex);

                buttons[i].image.sprite = animals[incorrectAnimalIndex];
            }
        }
    }

    void AddSound()
    {
		//atribui o som relevante ao botão de som
        foreach(AudioClip snd in sounds){
            if (snd.name == correctAnimalName + "_sound")
            {
                correctAnimalSound = snd;
            }
        }
    }

    void GetButtons()
    {
        buttons = new Button[this.TOTAL_BUTTONS];

        GameObject leftButton = GameObject.Find("leftAnimal");
        buttons[0] = leftButton.GetComponent<Button>();
        GameObject rightButton = GameObject.Find("rightAnimal");
        buttons[1] = rightButton.GetComponent<Button>();

        GameObject soundButtonObject = GameObject.Find("playSound");
        this.soundButton = soundButtonObject.GetComponent<Button>();
        this.soundButton.onClick.AddListener(() => playSound());

        GameObject textObject = GameObject.Find("textField");
        this.textField = textObject.GetComponent<Text>();
        clearText();
    }

    void AddListeners()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => clickedButton());
        }
    }

    void clickedButton()
    {
		//Verifica se a resposta esta correta e responde em conformidade e recarrega se correto
        string btnName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (btnName == buttons[correctButtonIndex].name)
        {
			// Debug.Log ("Você clicou no animal correto");
			this.textField.text = "Você clicou no animal correto";
            Reload();
        }
        else
        {
			// Debug.Log ("Você clicou no animal errado");
			this.textField.text = "Você clicou no animal errado";
        }

    }

    void playSound()
    {
        destroySound();
        source = gameObject.AddComponent<AudioSource>();
        source.clip = this.correctAnimalSound;
        source.Play();
    }

    void destroySound(){
        if (source)
            Destroy(source);
    }
	
}
