using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace L1.Octahedron3D
{
    public class Octahedron3D : ModelVisual3D
    {
        private static readonly Brush _defaultColor = Brushes.Gray;

        public Octahedron3D()
        {
            DrawOctahedron(_size, _pos, _bottom, _top);
        }

        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                DrawOctahedron(_size, _pos, _bottom, _top);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                DrawOctahedron(_size, _pos, _bottom, _top);
            }
        }

        private Brush _bottom = _defaultColor;  
        public Brush Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;
                DrawOctahedron(_size, _pos, _bottom, _top);
            }
        }

        private Brush _top = _defaultColor;
        public Brush Top
        {
            get => _top;
            set
            {
                _top = value;
                DrawOctahedron(_size, _pos, _bottom, _top);
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

                },
                Material = material
            };
            return geometryModel3D;
        }

        private void DrawOctahedron(double size, Point3D pos, Brush bottom, Brush top)
        {
            double height = size * Math.Sqrt(2.0) / 1.0;

            Point3D topVertex = new Point3D(pos.X, pos.Y + height, pos.Z);
            Point3D bottomVertex = new Point3D(pos.X, pos.Y - height, pos.Z);
            Point3D[] baseVertices = new Point3D[4]
            {
                new Point3D(pos.X - size, pos.Y, pos.Z / Math.Sqrt(2)),
                new Point3D(pos.X, pos.Y, pos.Z + size / Math.Sqrt(2)),
                new Point3D(pos.X + size, pos.Y, pos.Z / Math.Sqrt(2)),
                new Point3D(pos.X, pos.Y, pos.Z - size / Math.Sqrt(2)),
            };

            Model3DGroup m3dg = new();

            for (int i = 0; i < baseVertices.Length; i++)
            {
                int nextIndex = (i + 1) % baseVertices.Length;
                m3dg.Children.Add(AddFace(topVertex, baseVertices[i], baseVertices[nextIndex], new DiffuseMaterial(top)));
            }

            for (int i = 0; i < baseVertices.Length; i++)
            {
                int nextIndex = (i + 1) % baseVertices.Length;
                m3dg.Children.Add(AddFace(bottomVertex, baseVertices[nextIndex], baseVertices[i], new DiffuseMaterial(bottom)));
            }

            Content = m3dg;
        }
    }
}
