using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace TKGraphics.Abstraction.DataStructures
{
    public class Vertex
    {
        //Derive from this
        public Vector3 Position { get; set; }

        public virtual float[] Vertices
        {
            get
            {
                return new[]
                {
                    Position.X, Position.Y, Position.Z
                };
            }
        }

        public Vertex(Vector3 position)
        {
            Position = position;
        }
    }
}
