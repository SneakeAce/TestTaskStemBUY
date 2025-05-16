using UnityEngine;

public abstract class Figure : MonoBehaviour
{
    protected const float DefaultRotationValue = 0;
    
    protected AnimalType _animalType;
    protected ShapeType _shapeType;
    protected Color _color;
    
    protected FigureConfig _config;
    
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    
    protected bool _isInSlot;

    public AnimalType AnimalType => _animalType;
    public ShapeType ShapeType => _shapeType;
    public Color Color => _color;
    public FigureConfig FigureConfig => _config;

    public bool IsInSlot { get => _isInSlot; }

    public virtual void SetComponents(FigureConfig config)
    {
        _config = config;

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        SetFigureProperties();
    }

    public void MoveToSlot(Vector3 positionToMove)
    {
        _isInSlot = true;

        _collider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        transform.position = positionToMove;
        transform.rotation = Quaternion.Euler(DefaultRotationValue, DefaultRotationValue, DefaultRotationValue);
    }

    public void DestroyFigure()
    {
        _isInSlot = false;

        Destroy(gameObject);
    }

    private void SetFigureProperties()
    {
        _animalType = _config.AnimalType;
        _shapeType = _config.ShapeType;
        _color = _config.Color;
    }

}
