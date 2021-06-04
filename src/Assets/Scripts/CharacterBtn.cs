using TMPro;
using UnityEngine;

public class CharacterBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    private GUIController _guiController;
    
    
    public void Init(string characterName, GameObject parent, GUIController controller)
    {
        Transform myTransform;
        _guiController = controller;
        (myTransform = transform).SetParent(parent.transform);
        myTransform.localScale=  new Vector3(1, 1, 1);
        name.text = characterName;
        
    }

    public void OnClick()
    {
        _guiController.SelectCharacter(name.text);
    }
}
