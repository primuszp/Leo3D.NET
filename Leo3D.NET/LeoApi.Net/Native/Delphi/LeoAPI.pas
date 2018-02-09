{****************************************************************************}
{                                                                            }
{    Leonar3Do Application Programming Interface Unit                        }
{                                                                            }
{    Warning! This document is an integral and inseparable part of the       }
{    Leonar3Do Software Development Kit version: 1.0., protected by the      }
{    Copyright 2010 of 3D for All Ltd. (www.Leonar3Do.com).                  }
{    All rights are reserved. Unauthorized reproduction or distribution      }
{    of Leonar3Do Software Development Kit version: 1.0., or any portion     }
{    of it, may result in severe civil and criminal penalties, and will      }
{    be prosecuted to the maximum extent possible under the governing law    }
{    specified it in the „License Terms for 3D for All Ltd.’s Leonar3Do      }
{    Software Development Kit version: 1.0”.                                 }
{                                                                            }
{****************************************************************************}

unit LeoAPI;

interface

uses
  Windows, OpenGL;

const
  LeoAPIDll='LeoAPI.dll';

  
///  LGL ERRORS

  LGL_NO_ERROR=0;
  LGL_INVALID_OPENGL_CONTEXT=1;
  LGL_GL_VERSION_2_0_NOT_SUPPORTED=2;
  LGL_FBO_NOT_SUPPORTED=3;
  LGL_CONTEXT_NOT_CREATED=4;
  LGL_STEREO_MODE_NOT_SUPPORTED=5;
  LGL_CONTEXT_COUNT_OVERFLOW=6;
  LGL_CANT_RENDER_CONTEXT=7;
  LGL_INVALID_RENDER_INDEX=8;
  LGL_INVALID_CONTEXT=9;

type
  TLGLContext=Pointer;

  TPoint3D=record
    x,y,z:Double;
  end;

  TLGLProjection=record
    ScrLB,ScrRB,ScrLT:TPoint3D;
    Focus:TPoint3D;
  end;

  PLGLProjection=^TLGLProjection;

  TProcedure=procedure; stdcall;


///
///  Leonar3Do basic functions
///

  procedure leoSetAfterRecieve(p:TProcedure); stdcall; external LeoAPIDll name 'leoSetAfterRecieve';
  procedure leoInitialize; stdcall; external LeoAPIDll name 'leoInitialize';
  function leoGetAPIVersion:Double; stdcall; external LeoAPIDll name 'leoGetAPIVersion';
  function leoIsConnected:Boolean; stdcall; external LeoAPIDll name 'leoIsConnected';

  function leoGetMonitorWidth:Double; stdcall; external LeoAPIDll name 'leoGetMonitorWidth';
  function leoGetMonitorHeight:Double; stdcall; external LeoAPIDll name 'leoGetMonitorHeight';
  function leoGetMonitorIndex:LongInt; stdcall; external LeoAPIDll name 'leoGetMonitorIndex';
  procedure leoSetStereo(Stereo:Boolean); stdcall; external LeoAPIDll name 'leoSetStereo';
  function leoIsStereoSwitchedOn:Boolean; stdcall; external LeoAPIDll name 'leoIsStereoSwitchedOn';
  function leoIsBirdVisible:Boolean; stdcall; external LeoAPIDll name 'leoIsBirdVisible';
  procedure leoGetBirdPosition(var OrigoX,OrigoY,OrigoZ,RotateX,RotateY,RotateZ:double); stdcall; external LeoAPIDll name 'leoGetBirdPosition';
  function leoGetSmallButtonState:Boolean; stdcall; external LeoAPIDll name 'leoGetSmallButtonState';
  function leoGetBigButtonState:Double; stdcall; external LeoAPIDll name 'leoGetBigButtonState';
  function leoAreGlassesVisible:Boolean; stdcall; external LeoAPIDll name 'leoAreGlassesVisible';
  procedure leoGetGlassesPosition(var OrigoX,OrigoY,OrigoZ,RotateX,RotateY,RotateZ:double); stdcall; external LeoAPIDll name 'leoGetGlassesPosition';
  procedure leoGetEyesPosition(var LeftX,LeftY,LeftZ,RightX,RightY,RightZ:double); stdcall; external LeoAPIDll name 'leoGetEyesPosition';
  procedure leoSetBirdVibration(Vibration:double); stdcall; external LeoAPIDll name 'leoSetBirdVibration';

  function leoGetLeonar3DoVersion:Double; stdcall; external LeoAPIDll name 'leoGetLeonar3DoVersion';
  function leoGetApplicationsCount:LongInt; stdcall; external LeoAPIDll name 'leoGetApplicationsCount';
  procedure leoSetApplicationName(Name:PChar); stdcall; external LeoAPIDll name 'leoSetApplicationName';
  procedure leoSetApplicationVersion(Version:double); stdcall; external LeoAPIDll name 'leoSetApplicationVersion';



///
///  Leonar3Do OpenGL functions
///

 procedure lglInitialize; stdcall; external LeoAPIDll name 'lglInitialize';

 function lglCreateContext(DC:HDC;GLRC:HGLRC):TLGLContext; stdcall; external LeoAPIDll name 'lglCreateContext';
 procedure lglDeleteContext(Context:TLGLContext); stdcall; external LeoAPIDll name 'lglDeleteContext';
 function lglGetLastError:LongWord; stdcall; external LeoAPIDll name 'lglGetLastError';
 function lglGetRenderCount(Context:TLGLContext):LongWord; stdCall; external LeoAPIDll name 'lglGetRenderCount';
 function lglStartRender(Context:TLGLContext; Index:LongWord; Matrix:PDouble):boolean; stdCall; external LeoAPIDll name 'lglStartRender';
 procedure lglGetProjection(Context:TLGLContext; Index:LongWord; Projection:PLGLProjection); stdcall; external LeoAPIDll name 'lglGetProjection';
 procedure lglFinishRender(Context:TLGLContext); stdCall; external LeoAPIDll name 'lglFinishRender';
 procedure lglGetRenderSize(Context:TLGLContext; Index:LongWord; var Width,Height:LongWord); stdCall; external LeoAPIDll name 'lglGetRenderSize';
 procedure lglSetClippingPlane(Context:TLGLContext; CPNear,CPFar:Double); stdCall; external LeoAPIDll name 'lglSetClippingPlane';
 procedure lglGetClippingPlane(Context:TLGLContext; var CPNear,CPFar:Double); stdCall; external LeoAPIDll name 'lglGetClippingPlane';

implementation

end.
