using System;
using System.Runtime.InteropServices;

namespace LeoApi.Net.OpenGL
{
    /// <summary>
    /// Represent an LGL context.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct LGLContext
    {
        /// <summary>
        /// Keeps the struct from being garbage collected prematurely.
        /// </summary>
        private IntPtr Data;
    }

    /// <summary>
    /// Contains parameters for a projection.
    /// <para>These parameters hold the left bottom, right bottom and left top corners of the projection plane and the position of the camera.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct LGLProjection
    {
        public double LeftBottomX, LeftBottomY, LeftBottomZ;
        public double RightBottomX, RightBottomY, RightBottomZ;
        public double LeftTopX, LeftTopY, LeftTopZ;
        public double FocusX, FocusY, FocusZ;
    }
}
