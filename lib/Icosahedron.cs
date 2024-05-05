using System;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace L1.Icosahedron3D
{
    public class Icosahedron3D : ModelVisual3D
    {
        private static readonly Brush _defaultColor = Brushes.Gray;

        public Icosahedron3D()
        {
            DrawIcosahedron(_size, _pos);
        }

        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                DrawIcosahedron(_size, _pos);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                DrawIcosahedron(_size, _pos);
            }
        }

        private Brush _color = _defaultColor;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                DrawIcosahedron(_size, _pos);
            }
        }

        private static GeometryModel3D AddFace(Point3D point1, Point3D point2, Point3D point3, Material material)
        {
            GeometryModel3D geometryModel3D = new()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions =
                    {
                        point1,
                        point2,
                        point3,
                    },
                    TriangleIndices = new Int32Collection { 0, 1, 2 },
                    Normals = CalculateNormals(point1, point2, point3)
                },
                Material = material
            };
            return geometryModel3D;
        }

        private static Vector3DCollection CalculateNormals(Point3D p1, Point3D p2, Point3D p3)
        {
            Vector3D normal = Vector3D.CrossProduct(p2 - p1, p3 - p1);
            normal.Normalize();  // Ensure the normal is unit length
            return new Vector3DCollection { normal, normal, normal };
        }

        private void DrawIcosahedron(double size, Point3D pos)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            double a = size * Math.Sqrt(3) / 4;
            double b = size * Math.Sqrt(10 + 2 * Math.Sqrt(5)) / 4;

            Point3D[] vertices = new Point3D[12]
            {
                new Point3D(-a, b, 0),
                new Point3D(a, b, 0),
                new Point3D(-a, -b, 0),
                new Point3D(a, -b, 0),
                new Point3D(0, -a, b),
                new Point3D(0, a, b),
                new Point3D(0, -a, -b),
                new Point3D(0, a, -b),
                new Point3D(b, 0, -a),
                new Point3D(b, 0, a),
                new Point3D(-b, 0, -a),
                new Point3D(-b, 0, a)
            };

            int[,] faces = new int[,]
            {
                {0, 11, 5}, {0, 5, 1}, {0, 1, 7}, {0, 7, 10}, {0, 10, 11},
                {1, 5, 9}, {5, 11, 4}, {11, 10, 2}, {10, 7, 6}, {7, 1, 8},
                {3, 9, 4}, {3, 4, 2}, {3, 2, 6}, {3, 6, 8}, {3, 8, 9},
                {4, 9, 5}, {2, 4, 11}, {6, 2, 10}, {8, 6, 7}, {9, 8, 1}
            };

            Model3DGroup m3dg = new();

            for (int i = 0; i < 20; i++)
            {
                m3dg.Children.Add(AddFace(vertices[faces[i, 0]], vertices[faces[i, 1]], vertices[faces[i, 2]], new DiffuseMaterial(_color)));
            }

            Content = m3dg;
        }
    }
}
