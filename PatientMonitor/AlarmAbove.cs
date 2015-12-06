﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientMonitor
{
    public partial class AlarmAbove : Form
    {
        public AlarmAbove()
        {
            InitializeComponent();
        }

        //add soundplayer function which will play a resource file
        SoundPlayer MutableAlarm = new SoundPlayer(ResourceAlarm.MutableAlarm);


        int _saveTimer;

        //create property to save timer
        public int saveTimer
        {
            get
            {
                return this._saveTimer;
            }
        }

        //add int value to work as a visable counter
        int i = 0;
        private void tmrAboveLimit_Tick(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AlarmAbove>().Any())
            {
                //play alarm sound when timer starts and command it to loop
                MutableAlarm.PlayLooping();
                i++;
                //convert int value to appear as text
                lblCounterAbove.Text = i.ToString() + " Seconds";
            }
        }

        private void btnDisableAbove_Click(object sender, EventArgs e)
        {
            //have time stored
            _saveTimer = i;

            //call timer recorder method to record the time taken
            TimerRecorder timesUp = new TimerRecorder();
            timesUp.csvWriter();

            //close the form once pressed
            this.Close();

            //stop the timer and alarm sound when user clicks disable
            tmrAboveLimit.Stop();
            MutableAlarm.Stop();


        }
    }
}
