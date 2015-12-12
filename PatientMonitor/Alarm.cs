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
    public partial class Alarm : Form
    {
        public Alarm()
        {
            InitializeComponent();

            // Play alarm sound and command it to loop
            MutableAlarm.PlayLooping();
            muted = false;
        }

        //add soundplayer function which will play a resource file
        SoundPlayer MutableAlarm = new SoundPlayer(ResourceAlarm.MutableAlarm);

        // Declare bool for muted state
        bool muted;

        //add int value to work as a visable counter
        int count;
        private void tmrAboveLimit_Tick(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Alarm>().Any())
            {
                // Increment counter
                count++;
                //convert int value to appear as text
                lblCounterAbove.Text = count.ToString() + " Seconds";
             }
        }

        // Instantiate  ResponseTime table adapter
        MonitorDBTableAdapters.ResponseTimesTableAdapter responseTableAdapter = new MonitorDBTableAdapters.ResponseTimesTableAdapter();

        private void btnDisableAbove_Click(object sender, EventArgs e)
        {
            //call timer recorder method to record the time taken
            TimerRecorder timesUp = new TimerRecorder();

            //call timer recorder class
            timesUp.csvWriter(lblCounterAbove.Text);

            // Write response time to database
            responseTableAdapter.Insert(Monitor.curBed, 3, DateTime.Now, count);

            //close the form once pressed
            this.Close();

            //stop the timer and alarm sound when user clicks disable
            tmrAboveLimit.Stop();
            MutableAlarm.Stop();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            if (muted == false)
            {
                // Stop alarm sound
                MutableAlarm.Stop();
                muted = true;

                // Update button label
                btnMute.Text = "Unmute Alarm";
            }
            else
            {
                // Play alarm sound and command it to loop
                MutableAlarm.PlayLooping();
                muted = false;

                // Update button label
                btnMute.Text = "Mute Alarm";
            }
        }



        private void responseTimesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.responseTimesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.monitorDB);

        }

        private void responseTimesBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.responseTimesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.monitorDB);

        }

        private void Alarm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'monitorDB.ResponseTimes' table. You can move, or remove it, as needed.
            this.responseTimesTableAdapter.Fill(this.monitorDB.ResponseTimes);

        }
    }
}
