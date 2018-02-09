using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using LeoApi.Net.OpenGL;

namespace LeoApi.Net.Sample01
{
    class LeoWindow : GameWindow
    {
        #region Members

        private LGLContext context;
        private IntPtr sphere, cylinder;

        // Init OpenGL lights
        private float[] ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
        private float[] diffuse = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
        private float[] specular = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
        private float[] position = new float[] { 1.0f, 1.0f, 1.0f, 0.0f };


        private float[] sphere_color_normal = new float[] { 0.6f, 0.8f, 1.0f, 1.0f };
        private float[] sphere_specular_color_normal = new float[] { 0.6f, 0.8f, 1.0f, 1.0f };
        private float[] sphere_color_clicked = new float[] { 0.6f, 1.0f, 0.8f, 1.0f };
        private float[] sphere_specular_color_clicked = new float[] { 0.6f, 1.0f, 0.8f, 1.0f };
        private float sphere_shininess = 20.0f;

        private float[] cylinder_color_normal = new float[] { 1.0f, 0.4f, 0.2f, 1.0f };
        private float[] cylinder_specular_color_normal = new float[] { 1.0f, 0.4f, 0.2f, 1.0f };
        private float[] cylinder_color_clicked = new float[] { 0.4f, 0.2f, 1.0f, 1.0f };
        private float[] cylinder_specular_color_clicked = new float[] { 0.4f, 0.2f, 1.0f, 1.0f };
        private float cylinder_shininess = 50.0f;


        // Bird position
        private Vector3d birdpos;

        // Bird rotation
        private Vector3d birdrot;

        // Small button state
        private bool small_button_pressed;

        // Big button state
        private double big_button_pression;

        #endregion

        public LeoWindow()
            : base(800, 600, new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(32), 24, 8, 4))
        {
            this.Title = "LeoApi.Net v1.1 - Leonar3do OpenGL Window";
            this.Keyboard.KeyDown += Keyboard_KeyDown;
        }

        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.CleanUp();
                this.Exit();
            }

            if (e.Key == Key.Space)
            {
                Leo.SetStereo(!Leo.IsStereoSwitchedOn());
            }

            if (e.Key == Key.F11)
            {
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // Retrieve current bird state
            this.UpdateLeoInfo();

            // Get the number of views to render
            uint rc = LGL.GetRenderCount(context);

            // Iterate through the views
            for (uint i = 0; i < rc; ++i)
            {
                // Start render the ith view

                double[] projmat = new double[16];
                LGL.StartRender(context, i, projmat);

                // Set the projection matrix associated with the current view
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadMatrix(projmat);

                // Render the scene
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();

                this.DrawBird();
            }
            // Indicate that all rendering is finished
            LGL.FinishRender(context);
        }

        private void DrawBird()
        {
            float[] sphere_color;
            float[] sphere_specular_color;
            float[] cylinder_color;
            float[] cylinder_specular_color;

            if (small_button_pressed)
            {
                sphere_color = sphere_color_clicked;
                sphere_specular_color = sphere_specular_color_clicked;
                cylinder_color = cylinder_color_clicked;
                cylinder_specular_color = cylinder_specular_color_clicked;
            }
            else
            {
                sphere_color = sphere_color_normal;
                sphere_specular_color = sphere_specular_color_normal;
                cylinder_color = cylinder_color_normal;
                cylinder_specular_color = cylinder_specular_color_normal;
            }

            GL.PushMatrix();
            {
                GL.Material(MaterialFace.Front, MaterialParameter.AmbientAndDiffuse, sphere_color);
                GL.Material(MaterialFace.Front, MaterialParameter.Specular, sphere_specular_color);
                GL.Material(MaterialFace.Front, MaterialParameter.Shininess, sphere_shininess);

                GL.Translate(birdpos);
                GL.Rotate(RadToDeg(birdrot.Z), 0.0, 0.0, 1.0);
                GL.Rotate(RadToDeg(birdrot.Y), 0.0, 1.0, 0.0);
                GL.Rotate(RadToDeg(birdrot.X), 1.0, 0.0, 0.0);

                OpenTK.Graphics.Glu.Sphere(sphere, 0.04, 32, 32);

                GL.Material(MaterialFace.Front, MaterialParameter.AmbientAndDiffuse, cylinder_color);
                GL.Material(MaterialFace.Front, MaterialParameter.Specular, cylinder_specular_color);
                GL.Material(MaterialFace.Front, MaterialParameter.Shininess, cylinder_shininess);

                GL.Translate(0.0, 0.0, -0.08);
                DrawSolidCylinder(cylinder, 0.01, 0.01, 0.16, 32, 32);
                GL.Translate(0.0, 0.0, +0.08);

                GL.Rotate(90.0, 0.0, 1.0, 0.0);
                GL.Translate(0.0, 0.0, -0.08);
                DrawSolidCylinder(cylinder, 0.01, 0.01, 0.16, 32, 32);
            }
            GL.PopMatrix();
        }

        private void DrawSolidCylinder(IntPtr quad, double @base, double top, double height, int slices, int stacks)
        {
            OpenTK.Graphics.Glu.Cylinder(quad, @base, top, height, slices, stacks);
            
            GL.Rotate(180, 1, 0, 0);
            OpenTK.Graphics.Glu.Disk(quad, 0.0f, @base, slices, 1);
            
            GL.Rotate(180, 1, 0, 0);
            GL.Translate(0.0f, 0.0f, height);
            OpenTK.Graphics.Glu.Disk(quad, 0.0f, top, slices, 1);
            GL.Translate(0.0f, 0.0f, -height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Retrieve current bird state
            this.UpdateLeoInfo();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!InitializeLeoSystem())
            {
                this.Exit();
            }

            // Other initializations
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            {
                GL.Light(LightName.Light0, LightParameter.Ambient, ambient);
                GL.Light(LightName.Light0, LightParameter.Diffuse, diffuse);
                GL.Light(LightName.Light0, LightParameter.Specular, specular);
                GL.Light(LightName.Light0, LightParameter.Position, position);
            }

            sphere = OpenTK.Graphics.Glu.NewQuadric();
            cylinder = OpenTK.Graphics.Glu.NewQuadric();
        }

        private bool InitializeLeoSystem()
        {
            // Initialize and connect the application to Leonar3Do System Software
            if (!Leo.Initialize())
            {
                return false;
            }

            // Initialize LGL
            LGL.Initialize();

            // Create an LGL context (with the currently active OpenGL rendering context and its device)
            this.context = LGL.CreateContext();

            if (LGL.GetLastLGLError() != LGLErrors.NoError)
            {
                return false;
            }

            return true;
        }

        private void UpdateLeoInfo()
        {
            // Get info from LeoAPI
            Leo.GetBirdPosition(out birdpos.X, out birdpos.Y, out birdpos.Z, out birdrot.X, out birdrot.Y, out birdrot.Z);

            small_button_pressed = Leo.GetSmallButtonState();
            big_button_pression = Leo.GetBigButtonState();

            GL.ClearColor((float)big_button_pression, 0.0f, 0.0f, 1.0f);
            Leo.SetBirdVibration(big_button_pression);
        }

        private void CleanUp()
        {
            Leo.SetStereo(false);
            LGL.DeleteContext(context);
            OpenTK.Graphics.Glu.DeleteQuadric(sphere);
            OpenTK.Graphics.Glu.DeleteQuadric(cylinder);
        }

        private double RadToDeg(double x)
        {
            // 180 / PI
            return x * 57.295779513082320876798154814105;
        }
    }
}