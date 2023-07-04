using UnityEngine;

namespace Core
{
    public class OutlineProp : MonoBehaviour
    {
        [SerializeField] private bool _draggable;
        [SerializeField] private float _outlineWidth;
        [SerializeField] private Color _correctOutlineColor = Color.green;
        [SerializeField] private Color _wrongOutlineColor = Color.red;
        private Color _currentColor;
        private readonly Color _draggableColor = Color.cyan;
        private Material _outline;

        void Start()
        {
            _outline = Instantiate(GetComponent<Renderer>().sharedMaterial);
            _outline.SetFloat("_" + "OutlineWidth", _outlineWidth);

            if (_draggable)
                _outline.SetColor("_OutlineColor", _draggableColor);

            _currentColor = _outline.GetColor("_OutlineColor");
            GetComponent<Renderer>().sharedMaterial = _outline;
        }

        public void SetDraggableOutline()
        {
            _draggable = true;
            _outline.SetColor("_OutlineColor", _draggableColor);
        }

        public void SetBaseColor()
        {
            _outline.SetColor("_OutlineColor", _currentColor);
        }
        
        public void ChangeColorCorrect()
        {
            _outline.SetColor("_OutlineColor", _correctOutlineColor);
        }
        
        public void ChangeColorWrong()
        {
            _outline.SetColor("_OutlineColor", _wrongOutlineColor);
        }
    }
}