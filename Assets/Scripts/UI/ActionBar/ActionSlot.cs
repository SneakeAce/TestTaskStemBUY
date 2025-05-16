using UnityEngine;

public class ActionSlot : MonoBehaviour
{
    private AnimalType _currentFigureAnimalType;
    private ShapeType _currentFigureShapeType;
    private Color _currentFigureColor;

    private Figure _currentFigure;
    private Vector3 _currentPositionSlot;

    private bool _isEmptySlot;

    public AnimalType CurrentFigureAnimalType { get => _currentFigureAnimalType; }
    public ShapeType CurrentFigureShapeType { get => _currentFigureShapeType; }
    public Color CurrentFigureColor { get => _currentFigureColor; }
    public Vector3 CurrentPositionSlot { get => _currentPositionSlot; }
    public Figure CurrentFigure { get => _currentFigure; }
    public bool IsEmptySlot { get => _isEmptySlot; }

    private void Awake()
    {
        _isEmptySlot = true;
        _currentPositionSlot = transform.position;
    }

    public void SetFigure(Figure figure)
    {
        Debug.Log($"This Slot name = {this.gameObject.name}");
        _currentFigure = figure;

        _currentFigureAnimalType = figure.AnimalType;
        _currentFigureShapeType = figure.ShapeType;
        _currentFigureColor = figure.Color;

        _isEmptySlot = false;
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
}
