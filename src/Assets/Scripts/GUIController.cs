using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private  TransparentWindow windowHandler;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private  GameObject animScrollView;
    [SerializeField] private  GameObject charScrollView;
    [SerializeField] private  GameObject charListContainer;
    [SerializeField] private  GameObject skinScrollView;
    [SerializeField] private List<GameObject> animButtons;
    [SerializeField] private List<TMP_Text> animButtonsTexts;
    [SerializeField] private List<GameObject> skinButtons;
    [SerializeField] private List<TMP_Text> skinButtonsTexts;
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private GameObject messageText;
    [SerializeField] private  GameObject scaleSlider;
    [SerializeField] private  TMP_Text sliderLabel;

    public bool canDisplayMenu = false;
    private List<GameObject> _scrollViews;
    private List<Character> _listOfCharacters;
    private SpineSkelController spineSkel;
    private List<CharacterSkins> _characterSkins;
    public UnityEngine.UI.Slider _slider;
    


    private void Start()
    {
        spineSkel = GetComponent<SpineSkelController>();
        _slider = scaleSlider.GetComponent<UnityEngine.UI.Slider>();
        _scrollViews= new List<GameObject>{animScrollView, charScrollView, skinScrollView};
        foreach (var btn in animButtons)
        {
            animButtonsTexts.Add(btn.GetComponentInChildren<TMP_Text>());
        }
        foreach (var btn in skinButtons)
        {
            skinButtonsTexts.Add(btn.GetComponentInChildren<TMP_Text>());
        }
    }

    public void OpenCloseScrollView(GameObject currentScrollView)
    {
        if(currentScrollView.name=="Manager")
            currentScrollView= new GameObject();
        foreach (var scrollView in _scrollViews.Where(scrollView => scrollView!= currentScrollView))
        {
            scrollView.SetActive(false);
        }
        if(currentScrollView!=null)
            currentScrollView.SetActive(!currentScrollView.activeSelf);
    }

    public void InstantiateContentLists(Data characters)
    {
        _listOfCharacters = characters.Characters;
        foreach (var character in _listOfCharacters)
        {
            GameObject charBtn = Instantiate(btnPrefab);
            charBtn.GetComponent<CharacterBtn>().Init(character.Name, charListContainer,this);
        }
    }

    public void SelectCharacter(string characterName)
    {
        
        var clickedCharacter = _listOfCharacters.FirstOrDefault(c => c.Name == characterName);
        if (clickedCharacter != null&& clickedCharacter.Playable)
        {
            _characterSkins = clickedCharacter.Skins;
            spineSkel.LoadCharacter(clickedCharacter.Skins[0].Addressable);
            
            for (var i = 0; i < skinButtons.Count; i++)
            {
                if (i < clickedCharacter.Skins.Count)
                {
                    skinButtons[i].SetActive(enabled);
                    skinButtonsTexts[i].text = clickedCharacter.Skins[i].Name;
                }
                else
                {
                    skinButtons[i].SetActive(false);
                }
            }
            messageText.SetActive(false);
        }

        
    }

    public void EnableAnimationBtns(List<string> animations)
    {
        for (var i = 0; i < animButtons.Count; i++)
        {
            if (i < animations.Count)
            {
                animButtons[i].SetActive(enabled);
                animButtonsTexts[i].text = animations[i];
            }
            else
            {
                animButtons[i].SetActive(false);
            }
        }
    }
    public void SwitchSkin(TMP_Text skinNameTmpText)
    {
        
        var clickedSkin = _characterSkins.FirstOrDefault(c => c.Name == skinNameTmpText.text);
        spineSkel.LoadCharacter(clickedSkin.Addressable);
    }
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
    public void OnMinimizeButtonClick()
    {

        windowHandler.minimize();
    }
    public void CloseMenu()
    {
        mainMenu.SetActive(false);
        OpenCloseScrollView(new GameObject("Manager"));
    }

    public void DisplayMenu()
    {
        if(canDisplayMenu)
            mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void UpdateSliderValue()
    {
        sliderLabel.text = _slider.value.ToString(CultureInfo.InvariantCulture);
        spineSkel.SetScale(_slider.value);
    }
}
