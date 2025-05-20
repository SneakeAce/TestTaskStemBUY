using System;
using UnityEngine;

public class Figure : MonoBehaviour
{
    protected const float DefaultRotationValue = 0;

    protected FigureType _figureType;
    protected AnimalType _animalType;
    protected ShapeType _shapeType;
    protected Color _color;

    protected SpecialFigureConfig _specialConfig;
    protected FigureConfig _config;
    
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    
    protected bool _isInSlot;
    protected bool _isStuck;

    public FigureType FigureType => _figureType; 
    public AnimalType AnimalType => _animalType;
    public ShapeType ShapeType => _shapeType;
    public Color Color => _color;
    public FigureConfig FigureConfig => _config;
    public bool IsInSlot => _isInSlot;
    public bool IsStuck { get => _isStuck; set => _isStuck = value; }

    public event Action DetachFigure;

    public virtual void SetComponents(FigureConfig config)
    {
        _config = config;

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        SetFigureProperties();
    }

    public void SetSpecialConfig(SpecialFigureConfig specialConfig) => _specialConfig = specialConfig;

    public void MoveToSlot(Vector3 positionToMove)
    {
        _isInSlot = true;

        _collider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        transform.position = positionToMove;
        transform.rotation = Quaternion.Euler(DefaultRotationValue, DefaultRotationValue, DefaultRotationValue);

        DetachFigure?.Invoke();
    }

    public void DestroyFigure()
    {
        _isInSlot = false;

        Destroy(gameObject);
    }

    private void SetFigureProperties()
    {
        _figureType = SetFigureType();

        _animalType = _config.AnimalType;
        _shapeType = _config.ShapeType;
        _color = _config.Color;
    }

    private FigureType SetFigureType()
    {
        if (_specialConfig == null)
            return FigureType.BaseFigure;

        if (_specialConfig.SpecialFigureType == FigureType.HeavyFigure)
        {
            if (_specialConfig is HeavyFigureConfig heavyFigureConfig)
            {
                _specialConfig = heavyFigureConfig;
                _rigidbody.mass = heavyFigureConfig.Mass;
                _rigidbody.gravityScale = heavyFigureConfig.GravityScale;
            }

            return _specialConfig.SpecialFigureType;
        }
        else if (_specialConfig.SpecialFigureType == FigureType.FrozenFigure)
        {

            return _specialConfig.SpecialFigureType;
        }
        else if (_specialConfig.SpecialFigureType == FigureType.StickyFigure)
        {
            if (_specialConfig is StickyFigureConfig stickyFigureConfig)
            {
                _specialConfig = stickyFigureConfig;

                StickyFigure stickyFigure = this.gameObject.AddComponent<StickyFigure>();

                stickyFigure.SetComponents(stickyFigureConfig);
            }

            return _specialConfig.SpecialFigureType;
        }

        return FigureType.BaseFigure;
    }

}
