using System;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace LeoApi.Net.OpenGL
{
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public static partial class LGL
    {
        #region Functions

        #region OpenGL

        public static IntPtr GetCurrentDC()
        {
            return wglGetCurrentDC();
        }

        public static IntPtr GetCurrentContext()
        {
            return wglGetCurrentContext();
        }

        #endregion

        #region Leonar3Do OpenGL functions

        /// <summary>
        /// Initializes LGL.
        /// </summary>
        public static void Initialize()
        {
            lglInitialize();
        }

        /// <summary>
        /// Creates an LGL Context.
        /// <para>For creating an LGL context, an active OpenGL Rendering Context is needed.</para>
        /// <para>Furthermore, the OpenGL Context must has a double-buffered RGBA pixel-format with a depth-buffer.</para>
        /// </summary>
        /// <param name="hdc">Handle to a device context.</param>
        /// <param name="hglrc">Handle to an active OpenGL Rendering Context.</param>
        /// <returns>LGL Context</returns>
        public static LGLContext CreateContext(IntPtr hdc, IntPtr hglrc)
        {
            return lglCreateContext(hdc, hglrc);
        }

        /// <summary>
        /// Create an LGL context with the currently active OpenGL rendering context and its device.
        /// <para>For creating an LGL context, an active OpenGL Rendering Context is needed.</para>
        /// <para>Furthermore, the OpenGL Context must has a double-buffered RGBA pixel-format with a depth-buffer.</para>
        /// </summary>
        /// <returns>LGL Context</returns>
        public static LGLContext CreateContext()
        {
            return lglCreateContext(wglGetCurrentDC(), wglGetCurrentContext());
        }

        /// <summary>
        /// Destroys an LGL Context.
        /// </summary>
        /// <param name="context">LGL Context</param>
        public static void DeleteContext(LGLContext context)
        {
            lglDeleteContext(context);
        }

        /// <summary>
        /// Returns the last error code caused by an LGL function.
        /// </summary>
        /// <returns>Error code</returns>
        public static uint GetLastError()
        {
            return lglGetLastError();
        }

        /// <summary>
        /// Returns the last error code caused by an LGL function.
        /// </summary>
        /// <returns>Return LGLErrors enum code</returns>
        public static LGLErrors GetLastLGLError()
        {
            return (LGLErrors)lglGetLastError();
        }

        /// <summary>
        /// Sets the last error code.
        /// </summary>
        /// <param name="error">Error code.</param>
        public static void SetLastError(uint error)
        {
            lglSetLastError(error);
        }

        /// <summary>
        /// Sets the last error code.
        /// </summary>
        /// <param name="error">LGLErrors enum code.</param>
        public static void SetLastError(LGLErrors error)
        {
            lglSetLastError((uint)error);
        }

        /// <summary>
        /// Returns the number of views to render.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <returns>Number of views to render.</returns>
        public static uint GetRenderCount(LGLContext context)
        {
            return lglGetRenderCount(context);
        }

        /// <summary>
        /// Issues starting of rendering to the destination framebuffer of the view associated with index.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <param name="index">The index of the view to be rendered.</param>
        /// <param name="matrix">Pointer to 16 consecutive double values, which are set by the function as the elements of a 4x4 column-major matrix.</param>
        /// <returns></returns>
        public static unsafe bool StartRender(LGLContext context, uint index, [In] double* matrix)
        {
            return lglStartRender(context, index, matrix);
        }

        /// <summary>
        /// Issues starting of rendering to the destination framebuffer of the view associated with index.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <param name="index">The index of the view to be rendered.</param>
        /// <param name="matrix">Pointer to 16 consecutive double values, which are set by the function as the elements of a 4x4 column-major matrix.</param>
        /// <returns></returns>
        public static unsafe bool StartRender(LGLContext context, uint index, [In] IntPtr matrix)
        {
            return lglStartRender(context, index, matrix);
        }

        /// <summary>
        /// Issues starting of rendering to the destination framebuffer of the view associated with index.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <param name="index">The index of the view to be rendered.</param>
        /// <param name="matrix">Pointer to 16 consecutive double values, which are set by the function as the elements of a 4x4 column-major matrix.</param>
        /// <returns></returns>
        public static bool StartRender(LGLContext context, uint index, [In] double[] matrix)
        {
            unsafe
            {
                fixed (double* matrix_ptr = matrix)
                {
                    return lglStartRender(context, index, matrix_ptr);
                }
            }
        }

        /// <summary>
        /// Issues starting of rendering to the destination framebuffer of the view associated with index.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <param name="index">The index of the view to be rendered.</param>
        /// <param name="matrix">Pointer to 16 consecutive double values, which are set by the function as the elements of a 4x4 column-major matrix.</param>
        /// <returns></returns>
        public static bool StartRender<T>(LGLContext context, uint index, [In] T matrix) where T : struct
        {
            GCHandle matrix_ptr = GCHandle.Alloc(matrix, GCHandleType.Pinned);
            try
            {
                return lglStartRender(context, index, matrix_ptr.AddrOfPinnedObject());
            }
            finally
            {
                matrix_ptr.Free();
            }
        }

        /// <summary>
        /// Creates the proper output that to be derived from all rendered views.
        /// <para>This will refresh the contents of the default framebuffers associated with the LGLContext, that is it calls SwapBuffers() for each device.</para>
        /// <para>Therefore, you shouldn't call SwapBuffers() at any time.</para>
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        public static void FinishRender(LGLContext context)
        {
            lglFinishRender(context);
        }

        /// <summary>
        /// Returns the projection parameters for a given view.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <param name="index">The index of the view.</param>
        /// <param name="proj">Pointer to an LGLProjection structure that will be filled by the function.</param>
        public static void GetProjection(LGLContext context, uint index, out LGLProjection proj)
        {
            lglGetProjection(context, index, out proj);
        }

        /// <summary>
        /// Returns the dimensions of the framebuffer associated with index.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <param name="index">The index of the view.</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void GetRenderSize(LGLContext context, uint index, [Out] out uint width, [Out] out uint height)
        {
            lglGetRenderSize(context, index, out width, out height);
        }

        /// <summary>
        /// Sets the near and far clipping distances.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <param name="neard">Near clipping distance.</param>
        /// <param name="fard">Far clipping distance.</param>
        public static void SetClippingPlane(LGLContext context, [In] double neard, [In] double fard)
        {
            lglSetClippingPlane(context, neard, fard);
        }

        /// <summary>
        /// Returns the near and far clipping distances.
        /// </summary>
        /// <param name="context">Represent an LGL context.</param>
        /// <param name="neard">Near clipping distance.</param>
        /// <param name="fard">Far clipping distance.</param>
        public static void GetClippingPlane(LGLContext context, [Out] out double neard, [Out] out double fard)
        {
            lglGetClippingPlane(context, out neard, out fard);
        }

        #endregion

        #endregion
    }
}
