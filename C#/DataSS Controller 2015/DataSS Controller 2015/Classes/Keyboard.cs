﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace DataSS_Controller_2015.Classes
{
    public class KeyboardController : Controller
    {
        //public delegate void ControllerHandler(object sender, ControllerEventArgs e);
        //public event ControllerHandler InputChanged;

        private KeyboardState OldState;
        private List<Keys> PressedKeys;
        Thread poll;

        public KeyboardController()
        {
            OldState = Keyboard.GetState();
            PressedKeys = new List<Keys>();
        }

        public override void BeginPolling()
        {
            //threading doesnt seem to work with keyboards
            poll = new Thread(new ThreadStart(polling));
            poll.Start();
            //polling();
        }

        void polling()
        {
            while (true)
            {
                //new state to update old state
                //also doesnt seem to work
                //if a key is pressed initially, it stays pressed, and vice versa (not how it should be)
                KeyboardState newState = Keyboard.GetState();
                //another way of detecting keypresses
                //PressedKeys = newState.GetPressedKeys().ToList<Keys>();
                bool flag = false;
                #region left stick equivalents
                //A uses that other way as a test
                if (newState.IsKeyDown(Keys.A))
                {
                    ls.X = -1;
                    flag = true;
                }
                if (newState.IsKeyDown(Keys.D))
                {
                    ls.X = 1;
                    flag = true;
                }
                if (newState.IsKeyDown(Keys.W))
                {
                    ls.Y = 1;
                    flag = true;
                }
                if (newState.IsKeyDown(Keys.S))
                {
                    ls.Y = -1;
                    flag = true;
                }
                if (newState.IsKeyUp(Keys.A) && newState.IsKeyUp(Keys.D) && LS.X != 0)
                {
                    ls.X = 0;
                    flag = true;
                }
                if (newState.IsKeyUp(Keys.W) && newState.IsKeyUp(Keys.S) && LS.Y != 0)
                {
                    ls.Y = 0;
                    flag = true;
                }
                #endregion
                #region right stick equivalents
                if (newState.IsKeyDown(Keys.Left))
                {
                    rs.X = -1;
                    flag = true;
                }
                if (newState.IsKeyDown(Keys.Right))
                {
                    rs.X = 1;
                    flag = true;
                }
                if (newState.IsKeyDown(Keys.Up))
                {
                    rs.Y = 1;
                    flag = true;
                }
                if (newState.IsKeyDown(Keys.Down))
                {
                    rs.Y = -1;
                    flag = true;
                }
                if (newState.IsKeyUp(Keys.Left) && newState.IsKeyUp(Keys.Right) && rs.X != 0)
                {
                    rs.X = 0;
                    flag = true;
                }
                if (newState.IsKeyUp(Keys.Down) && newState.IsKeyUp(Keys.Up) && rs.Y != 0)
                {
                    rs.Y = 0;
                    flag = true;
                }
                #endregion
                #region trigger equivalents
                if (newState.IsKeyDown(Keys.Q))
                {
                    LT = 1;
                    flag = true;
                }
                if (newState.IsKeyDown(Keys.E))
                {
                    RT = 1;
                    flag = true;
                }
                if (newState.IsKeyUp(Keys.Q) && LT != 0)
                {
                    LT = 0;
                    flag = true;
                }
                if (newState.IsKeyUp(Keys.E) && LT != 0)
                {
                    RT = 0;
                    flag = true;
                }
#endregion
                #region button equivalents
                //if (A != newState.IsKeyDown(Keys.M))
                //{
                //    A = newState.IsKeyDown(Keys.M);
                //    flag = true;
                //}
                //if (B != newState.IsKeyDown(Keys.OemPeriod))
                //{
                //    B = newState.IsKeyDown(Keys.OemPeriod);
                //    flag = true;
                //}
                //if (X != newState.IsKeyDown(Keys.OemComma))
                //{
                //    X = newState.IsKeyDown(Keys.OemComma);
                //    flag = true;
                //}
                //if (Y != newState.IsKeyDown(Keys.Divide))
                //{
                //    Y = newState.IsKeyDown(Keys.Divide);
                //    flag = true;
                //}
                //if (LB != newState.IsKeyDown(Keys.Z))
                //{
                //    LB = newState.IsKeyDown(Keys.Z);
                //    flag = true;
                //}
                //if (RB != newState.IsKeyDown(Keys.Z))
                //{
                //    RB = newState.IsKeyDown(Keys.Z);
                //    flag = true;
                //}
                #endregion
                if (flag)
                {
                    OnInputChanged();
                }
                OldState = newState;
            }
        }

        public class ControllerEventArgs : EventArgs
        {
            public ControllerEventArgs() :
                base() { }
        }
    }
}
