using UnityEngine;

public class Figure : MonoBehaviour
{
    private AnimalType _animalType;
    private ShapeType _shapeType;
    private Color _color;

    private FigureConfig _config;

    public AnimalType AnimalType => _animalType;
    public ShapeType ShapeType => _shapeType;
    public Color Color => _color;

    public void SetComponents(FigureConfig config)
    {
        _config = config;

        SetFigureProperties();
    }

    private void SetFigureProperties()
    {
        _animalType = _config.AnimalType;
        _shapeType = _config.ShapeType;
        _color = _config.Color;
    }
}
