using System;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace L1.TriangularPrism3D
{
    public class TriangularPrism3D : ModelVisual3D
    {
        private static readonly Brush _defaultColor = Brushes.Gray;

        public TriangularPrism3D()
        {
            DrawTriangularPrism(_size, _height, _color);
        }

        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                DrawTriangularPrism(_size, _height, _color);
            }
        }

        private double _height = 1.0;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                DrawTriangularPrism(_size, _height, _color);
            }
        }

        private Brush _color = _defaultColor;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                DrawTriangularPrism(_size, _height, _color);
            }
        }

        private static GeometryModel3D AddFace(Point3D[] vertices, Material material)
        {
            Point3DCollection positions = new Point3DCollection(vertices);
            Int32Collection triangleIndices = new Int32Collection()
            {
                0, 1, 2, // Front triangle
                3, 4, 5, // Back triangle
                6, 7, 8, // Bottom triangle
                9, 10, 11, // Side triangle 1
                12, 13, 14, // Side triangle 2
                15, 16, 17 // Side triangle 3
            };

            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions = positions;
            mesh.TriangleIndices = triangleIndices;

            GeometryModel3D geometryModel3D = new GeometryModel3D()
            {
                Geometry = mesh,
                Material = material
            };

            return geometryModel3D;
        }

        private void DrawTriangularPrism(double size, double height, Brush color)
        {
            double halfSize = size / 2.0;
            double halfHeight = height / 2.0;

            Point3D[] vertices = new Point3D[18]
            {
                new Point3D(0, halfHeight, -halfSize), // Vertex 0
                new Point3D(-halfSize, -halfHeight, halfSize), // Vertex 1
                new Point3D(halfSize, -halfHeight, halfSize), // Vertex 2
                new Point3D(0, halfHeight, halfSize), // Vertex 3
                new Point3D(-halfSize, -halfHeight, -halfSize), // Vertex 4
                new Point3D(halfSize, -halfHeight, -halfSize), // Vertex 5
                new Point3D(-halfSize, -halfHeight, halfSize), // Vertex 6
                new Point3D(halfSize, -halfHeight, halfSize), // Vertex 7
                new Point3D(0, -halfHeight, halfSize), // Vertex 8
                new Point3D(-halfSize, -halfHeight, -halfSize), // Vertex 9
                new Point3D(halfSize, -halfHeight, -halfSize), // Vertex 10
                new Point3D(0, -halfHeight, -halfSize), // Vertex 11
                new Point3D(-halfSize, -halfHeight, halfSize), // Vertex 12
                new Point3D(-halfSize, -halfHeight, -halfSize), // Vertex 13
                new Point3D(-halfSize, halfHeight, 0), // Vertex 14
                new Point3D(halfSize, -halfHeight, halfSize), // Vertex 15
                new Point3D(halfSize, -halfHeight, -halfSize), // Vertex 16
                new Point3D(halfSize, halfHeight, 0) // Vertex 17
            };

            Model3DGroup m3dg = new();

            m3dg.Children.Add(AddFace(new Point3D[] { vertices[0], vertices[1], vertices[2] }, new DiffuseMaterial(color))); // Front triangle
            m3dg.Children.Add(AddFace(new Point3D[] { vertices[3], vertices[4], vertices[5] }, new DiffuseMaterial(color))); // Back triangle
            m3dg.Children.Add(AddFace(new Point3D[] { vertices[6], vertices[7], vertices[8] }, new DiffuseMaterial(color))); // Bottom triangle
            m3dg.Children.Add(AddFace(new Point3D[] { vertices[3], vertices[10], vertices[11] }, new DiffuseMaterial(color))); // Side triangle 1
            m3dg.Children.Add(AddFace(new Point3D[] { vertices[12], vertices[13], vertices[0] }, new DiffuseMaterial(color))); // Side triangle 2
            m3dg.Children.Add(AddFace(new Point3D[] { vertices[14], vertices[15], vertices[16] }, new DiffuseMaterial(color))); // Side triangle 3

            Content = m3dg;
        }
    }
}
