using UnityEngine;

public class ActionSlot : MonoBehaviour
{
    private AnimalType _currentFigureAnimalType;
    private ShapeType _currentFigureShapeType;
    private Color _currentFigureColor;

    private Figure _currentFigure;
    private Vector2 _currentPositionSlot;
    private RectTransform _rectTransformSlot;

    private bool _isEmptySlot;

    public AnimalType CurrentFigureAnimalType { get => _currentFigureAnimalType; }
    public ShapeType CurrentFigureShapeType { get => _currentFigureShapeType; }
    public Color CurrentFigureColor { get => _currentFigureColor; }
    public Vector2 CurrentPositionSlot { get => _currentPositionSlot; }
    public Figure CurrentFigure { get => _currentFigure; }
    public bool IsEmptySlot { get => _isEmptySlot; }

    private void Awake()
    {
        _isEmptySlot = true;
        _rectTransformSlot = GetComponent<RectTransform>();
    }

    public void SetFigure(Figure figure, Camera camera)
    {
        Debug.Log($"This Slot name = {this.gameObject.name}");
        _currentFigure = figure;

        _currentFigureAnimalType = figure.AnimalType;
        _currentFigureShapeType = figure.ShapeType;
        _currentFigureColor = figure.Color;

        _isEmptySlot = false;

        _currentPositionSlot = PositionInWorldSpace(camera);

    }

    public void ClearSlot()
    {
        _currentFigure.DestroyFigure();
        _currentFigure = null;

        _currentFigureAnimalType = AnimalType.None;
        _currentFigureShapeType = ShapeType.None;
        _currentFigureColor = Color.clear;

        _isEmptySlot = true;
    }

    private Vector2 PositionInWorldSpace(Camera camera)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, transform.position);

        Vector3 worldPosition;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransformSlot, screenPoint, camera, out worldPosition);

        return worldPosition;
    }
}
