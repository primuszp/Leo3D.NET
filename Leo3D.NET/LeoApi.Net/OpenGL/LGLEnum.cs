using System;

namespace LeoApi.Net.OpenGL
{
    [Serializable]
    public enum LGLErrors : uint
    {
        /// <summary>
        /// We are happy!
        /// </summary>
        NoError = 0,
        /// <summary>
        /// When hGLRC is not valid.
        /// </summary>
        InvalidOpenGLContext = 1,
        /// <summary>
        /// When OpenGL 2.0 is not supported.
        /// </summary>
        GLVersion20NotSupported = 2,
        /// <summary>
        /// When the extension GL_ARB_framebuffer_object is not supported.
        /// </summary>
        FboNotSupported = 3,
        /// <summary>
        /// Other unknown reason. We ara sad.
        /// </summary>
        ContextNotCreated = 4,
        /// <summary>
        /// When the stereo mode is not supported.
        /// </summary>
        StereoModeNotSupported = 5,
        /// <summary>
        /// When the maximum number of contexts are already created.
        /// </summary>
        ContextCountOverflow = 6,
        CantRenderContext = 7,
        InvalidRenderIndex = 8,
        InvalidContext = 9,
    }
}