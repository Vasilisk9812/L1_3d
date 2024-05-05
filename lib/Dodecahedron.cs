using System;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace L1.Dodecahedron3D
{
    public class Dodecahedron3D : ModelVisual3D
    {
        private static readonly Brush _defaultColor = Brushes.Gray;

        public Dodecahedron3D()
        {
            DrawDodecahedron(_size, _pos, _color);
        }

        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                DrawDodecahedron(_size, _pos, _color);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                DrawDodecahedron(_size, _pos, _color);
            }
        }

        private Brush _color = _defaultColor;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                DrawDodecahedron(_size, _pos, _color);
            }
        }

        private static GeometryModel3D AddFace(Point3D p1, Point3D p2, Point3D p3, Point3D p4, Point3D p5, Brush color)
        {
            GeometryModel3D geometryModel3D = new GeometryModel3D
            {
                Geometry = new MeshGeometry3D
                {
                    Positions = new Point3DCollection { p1, p2, p3, p4, p5 },
                    TriangleIndices = new Int32Collection { 0, 1, 2, 0, 2, 3, 0, 3, 4 }
                },
                Material = new DiffuseMaterial(color)
            };

            return geometryModel3D;
        }

        private void DrawDodecahedron(double size, Point3D pos, Brush color)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            double a = size / 2;
            double b = a / phi;

            Point3D[] vertices = new Point3D[20]
            {
                new Point3D(0, a, b),
                new Point3D(0, a, - b),
                new Point3D(0, -a, - b),
                new Point3D(0, -a, b),
                new Point3D(b, a,0),
                new Point3D(- b, a, 0),
                new Point3D(- b, - a, 0),
                new Point3D(b, - a, 0),
                new Point3D(b, 0, a),
                new Point3D(b, 0, - a),
                new Point3D(- b, 0, - a),
                new Point3D(- b, 0, a),
                new Point3D(pos.X + (1.0 / phi) * a, pos.Y + (1.0 / phi) * a, pos.Z + (1.0 / phi) * a),
                new Point3D(pos.X + (1.0 / phi) * a, pos.Y + (1.0 / phi) * a, pos.Z - (1.0 / phi) * a),
                new Point3D(pos.X + (1.0 / phi) * a, pos.Y - (1.0 / phi) * a, pos.Z + (1.0 / phi) * a),
                new Point3D(pos.X + (1.0 / phi) * a, pos.Y - (1.0 / phi) * a, pos.Z - (1.0 / phi) * a),
                new Point3D(pos.X - (1.0 / phi) * a, pos.Y + (1.0 / phi) * a, pos.Z + (1.0 / phi) * a),
                new Point3D(pos.X - (1.0 / phi) * a, pos.Y + (1.0 / phi) * a, pos.Z - (1.0 / phi) * a),
                new Point3D(pos.X - (1.0 / phi) * a, pos.Y - (1.0 / phi) * a, pos.Z + (1.0 / phi) * a),
                new Point3D(pos.X - (1.0 / phi) * a, pos.Y - (1.0 / phi) * a, pos.Z - (1.0 / phi) * a)
            };

            Model3DGroup m3dg = new();

            // Добавление граней
            for (int i = 0; i < vertices.Length; i += 5)
            {
                m3dg.Children.Add(AddFace(vertices[i], vertices[i + 1], vertices[i + 2], vertices[i + 3], vertices[i + 4], color));
            }

            Content = m3dg;
        }
    }
}
