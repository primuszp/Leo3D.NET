using System;
using System.Security;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace LeoApi.Net.OpenGL
{
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public static partial class LGL
    {
        #region Leonar3do API Native Library

        private const string LeoNativeLibrary = "LeoAPI.dll";

        #endregion

        #region OpenGL API Native Library

        private const string OpenGLNativeLibrary = "opengl32.dll";

        #endregion

        #region Calling Convention

        /// <summary>
        /// Specifies the calling convention.
        /// </summary>
        /// <remarks>
        /// Specifies <see cref="CallingConvention.StdCall" /> for Windows and Linux.
        /// </remarks>
        private const CallingConvention Convention = CallingConvention.StdCall;

        #endregion

        #region OpenGL base functions

        [DllImport(OpenGLNativeLibrary, CallingConvention = Convention, ExactSpelling = true, SetLastError = true), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr wglGetCurrentDC();

        [DllImport(OpenGLNativeLibrary, CallingConvention = Convention, ExactSpelling = true, SetLastError = true), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr wglGetCurrentContext();

        #endregion

        #region Leonar3Do OpenGL functions

        // LEOAPI void LEOAPI_CALL lglInitialize();
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern void lglInitialize();

        // LEOAPI LGLContext LEOAPI_CALL lglCreateContext(void *hDC, void *hGLRC);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static unsafe extern LGLContext lglCreateContext([In] IntPtr hdc, [In] IntPtr hglrc);

        // LEOAPI void LEOAPI_CALL lglDeleteContext(LGLContext context);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern void lglDeleteContext(LGLContext context);

        // LEOAPI unsigned LEOAPI_CALL lglGetLastError();
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern uint lglGetLastError();

        // LEOAPI void LEOAPI_CALL lglSetLastError(unsigned lglError);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern void lglSetLastError(uint lglError);

        // LEOAPI unsigned LEOAPI_CALL lglGetRenderCount(LGLContext context);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern uint lglGetRenderCount(LGLContext context);

        // LEOAPI bool LEOAPI_CALL lglStartRender(LGLContext context, unsigned index, double *matrix);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static unsafe extern bool lglStartRender(LGLContext context, uint index, [In] double* matrix);

        // LEOAPI bool LEOAPI_CALL lglStartRender(LGLContext context, unsigned index, double *matrix);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static unsafe extern bool lglStartRender(LGLContext context, uint index, [In] IntPtr matrix);

        // LEOAPI void LEOAPI_CALL lglFinishRender(LGLContext context);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern void lglFinishRender(LGLContext context);

        // LEOAPI void LEOAPI_CALL lglGetProjection(LGLContext context, unsigned index, LGLProjection *proj);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern void lglGetProjection(LGLContext context, uint index, out LGLProjection proj);

        // LEOAPI void LEOAPI_CALL lglGetRenderSize(LGLContext context, unsigned index, unsigned *width, unsigned *height);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern void lglGetRenderSize(LGLContext context, uint index, [Out] out uint width, [Out] out uint height);

        // LEOAPI void LEOAPI_CALL lglSetClippingPlane(LGLContext context, double neard, double fard);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern void lglSetClippingPlane(LGLContext context, [In] double neard, [In] double fard);

        // LEOAPI void LEOAPI_CALL lglGetClippingPlane(LGLContext context, double *neard, double *fard);
        [DllImport(LeoNativeLibrary, CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        private static extern void lglGetClippingPlane(LGLContext context, [Out] out double neard, [Out] out double fard);

        #endregion
    }
}
