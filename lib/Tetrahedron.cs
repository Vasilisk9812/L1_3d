using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace L1.Tetrahedron3D
{
    public class Tetrahedron3D : ModelVisual3D
    {
        private static readonly Brush _defaultColor = Brushes.Gray;

        public Tetrahedron3D()
        {
            DrawTetrahedron(_size, _pos);
        }

        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                DrawTetrahedron(_size, _pos);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                DrawTetrahedron(_size, _pos);
            }
        }

        private Brush _color = _defaultColor;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                DrawTetrahedron(_size, _pos);
            }
        }

        private static GeometryModel3D AddFace(Point3D point1, Point3D point2, Point3D point3, Material material)
        {
            GeometryModel3D geometryModel3D = new()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = { point1, point2, point3 },
                    TriangleIndices = new Int32Collection { 0, 1, 2 },
                    Normals = CalculateNormals(point1, point2, point3)
                },
                Material = material
            };
            return geometryModel3D;
        }

        private void DrawTetrahedron(double size, Point3D pos)
        {
            double height = size * Math.Sqrt(2.0 / 3.0);

            Point3D topVertex = new Point3D(pos.X, pos.Y + height, pos.Z);
            Point3D[] baseVertices = new Point3D[3]
            {
                new Point3D(pos.X - size, pos.Y, pos.Z - size / Math.Sqrt(3)),
                new Point3D(pos.X + size, pos.Y, pos.Z - size / Math.Sqrt(3)),
                new Point3D(pos.X, pos.Y, pos.Z + 2 * size / Math.Sqrt(3)),
            };

            Model3DGroup m3dg = new();

            m3dg.Children.Add(AddFace(baseVertices[0], baseVertices[2], baseVertices[1], new DiffuseMaterial(_color)));
            
            for (int i = 0; i < baseVertices.Length; i++)
            {
                int nextIndex = (i + 1) % baseVertices.Length;
                m3dg.Children.Add(AddFace(topVertex, baseVertices[i], baseVertices[nextIndex], new DiffuseMaterial(_color)));
            }

            Content = m3dg;
        }
        private static Vector3DCollection CalculateNormals(Point3D p1, Point3D p2, Point3D p3)
        {
            Vector3D normal = Vector3D.CrossProduct(p2 - p1, p3 - p1);
            normal.Normalize();


            return new Vector3DCollection { normal, normal, normal };
        }
    }
}
