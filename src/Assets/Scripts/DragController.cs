using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IDragHandler, IPointerUpHandler,IPointerDownHandler, IPointerClickHandler
{
    private RectTransform _dragRectTransform;
    private Canvas _canvas;
    [SerializeField] private SpineSkelController _skelController;
    [SerializeField] private GUIController _gui;
    

    private void Start()
    {
        _dragRectTransform = transform.GetComponent<RectTransform>();
        var parent = transform;
        while (_canvas == null)
        {
            _canvas = parent.GetComponentInParent<Canvas>();
            parent = parent.parent;
        }
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        
        if(eventData.button == PointerEventData.InputButton.Left){
            _dragRectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            _skelController.PlayOnClick();
        }else if (eventData.button == PointerEventData.InputButton.Right)
        {
            _gui.DisplayMenu();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //
    }
}
