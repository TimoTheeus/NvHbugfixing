using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    protected float _zoom; // Camera Zoom
    public Matrix scaleMatrix;
    public Matrix _transform; // Matrix Transform
    public Vector2 _pos; // Camera Position
    protected float _rotation; // Camera Rotation
    protected Rectangle bounds;

    public Rectangle Bounds
    {
        get { return bounds; }
        set { bounds = value; }
    }
    public Camera()
    {
        _zoom = 1.0f;
        _rotation = 0.0f;
        _pos = Vector2.Zero;
    }
    // Sets and gets zoom
    public float Zoom
    {
        get { return _zoom; }
        set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
    }
    public Matrix ScaleMatrix
    {
        get { return scaleMatrix;  }
        set { scaleMatrix = value; }
    }

    public float Rotation
    {
        get { return _rotation; }
        set { _rotation = value; }
    }

    // Auxiliary function to move the camera
    public void Move(Vector2 amount)
    {
        _pos += amount;
        _pos.X=Math.Max(_pos.X, bounds.Left);
        _pos.X = Math.Min(_pos.X, bounds.Right);
        _pos.Y = Math.Min(_pos.Y, bounds.Bottom);
        _pos.Y = Math.Max(_pos.Y, bounds.Top);
    }
    // Get set position
    public Vector2 Pos
    {
        get { return _pos; }
        set { _pos = value; }
    }
    public Matrix get_transformation(GraphicsDevice graphicsDevice)
    {
        _transform =
          Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                     Matrix.CreateRotationZ(Rotation) *
                                     Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                     ScaleMatrix;
        return _transform;
    }
}
