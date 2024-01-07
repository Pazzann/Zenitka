using Godot;

namespace Zenitka.Scripts._2D;

public partial class Camera : Camera2D
{
    private const float ZoomSpeed = 0.1f;
    private const float PanSpeed = 13f;

    private const float MinZoom = 0.2f;
    private const float MaxZoom = 2f;

    private Rect2 _safeBounds;

    public override void _Ready()
    {
        base._Ready();

        var viewportSize = GetViewportRect().Size / Zoom;
        _safeBounds = new Rect2(GlobalPosition - 0.5f * viewportSize, viewportSize);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("switch1"))
        {
            if (IsCurrent() == false)
                MakeCurrent();
        }

        if (Input.IsActionPressed("pan_2d"))
            Input.SetDefaultCursorShape(Input.CursorShape.Drag);
        else
            Input.SetDefaultCursorShape();
    }

    public override void _Input(InputEvent iEvent)
    {
        base._Input(iEvent);

        //var zoomPoint = Input.IsActionPressed("set_zoom_point_2d") ? GetGlobalMousePosition() : Vector2.Zero;
        var zoomPoint = GetGlobalMousePosition();

        if (iEvent.IsActionPressed("zoom_in"))
            ZoomAtPoint(ZoomSpeed, zoomPoint);

        if (iEvent.IsActionPressed("zoom_out"))
            ZoomAtPoint(-ZoomSpeed, zoomPoint);

        if (Input.IsActionPressed("pan_2d") && iEvent is InputEventMouseMotion mouseEvent)
            GlobalPosition += PanSpeed * -mouseEvent.Relative.Normalized() / Zoom;

        // var minSafeY = _startPos.Y - 0.5f * GetViewportRect().Size.Y * (1f / _startZoom - 1f / Zoom.Y);
        // var maxSafeY = _startPos.Y + 0.5f * GetViewportRect().Size.Y * (1f / _startZoom - 1f / Zoom.Y);
        //
        // GlobalPosition = GlobalPosition with { Y = Mathf.Clamp(GlobalPosition.Y, minSafeY, maxSafeY) };

        var viewportSize = GetViewportRect().Size / Zoom;
        GlobalPosition = GlobalPosition.Clamp(_safeBounds.Position + 0.5f * viewportSize, _safeBounds.End - 0.5f * viewportSize);
    }

    private void ZoomAtPoint(float factor, Vector2 point)
    {
        factor = Mathf.Clamp(factor, MinZoom / Zoom.X - 1f, MaxZoom / Zoom.X - 1f);

        GlobalPosition += (point - GlobalPosition) * factor;
        Zoom += Zoom * factor;
    }
}