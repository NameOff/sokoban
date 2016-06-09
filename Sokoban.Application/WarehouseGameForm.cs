﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace Sokoban.Application
{
    public class WarehouseGameForm : Form
    {
        public WarehouseControl CurrentControl;
        public IGameController GameController;
        public Level CurrentLevel;
        public int TickCount;
        public IEnumerator<Level> Levels;
        

        public WarehouseGameForm(IGameController gameController, IEnumerable<Level> levels)
        {
            GameController = gameController;
            Levels = levels.GetEnumerator();
            Levels.MoveNext();
            CurrentLevel = Levels.Current;
            CurrentControl = new WarehouseControl(CurrentLevel.Warehouse);
            Controls.Add(CurrentControl);

            AutoSize = true;
            KeyPreview = true;

            var timer = new Timer();
            timer.Interval = 80;
            timer.Tick += TimerTick;
            timer.Start();
            TickCount = 0;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (GameController.IsUndo())
            {
                CurrentLevel = CurrentLevel.PreviousStep();
                CurrentControl.SetWarehouse(CurrentLevel.Warehouse);
                return;
            }
            var button = GameController.GetDirection();
            if (button == Direction.None)
                return;
            CurrentLevel = CurrentLevel.NextStep<RegularMove>(button);
            CurrentControl.SetWarehouse(CurrentLevel.Warehouse);
            /* (CurrentLevel.IsOver() && Levels.MoveNext())
            {
                CurrentLevel = 
            }*/
        }
    }
}