/*
    ============================================================================

    Namespace:          GUITestStand

    Class Name:         frmMain

    File Name:          frmMain.cs

    Synopsis:           The one and only instance of this class defines the main
                        window displayed by the application on the desktop.

    Remarks:            I created this application to observe the behavior of my
                        ExceptionLogger class in a graphical environment, which
                        is devoid of standard handles.

    License:            Copyright (C) 2014-2020, David A. Gray.
                        All rights reserved.

                        Redistribution and use in source and binary forms, with
                        or without modification, are permitted provided that the
                        following conditions are met:

                        *   Redistributions of source code must retain the above
                            copyright notice, this list of conditions and the
                            following disclaimer.

                        *   Redistributions in binary form must reproduce the
                            above copyright notice, this list of conditions and
                            the following disclaimer in the documentation and/or
                            other materials provided with the distribution.

                        *   Neither the name of David A. Gray, nor the names of
                            his contributors may be used to endorse or promote
                            products derived from this software without specific
                            prior written permission.

                        THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
                        CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
                        WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
                        WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
                        PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
                        David A. Gray BE LIABLE FOR ANY DIRECT, INDIRECT,
                        INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
                        (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
                        SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
                        PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
                        ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
                        LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
                        ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
                        IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

    Created:            Monday, 14 July 2014 - Saturday, 19 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Description
    ---------- ------- --- -----------------------------------------------------
    2020/12/20 7.24    DAG Initial implementation.
    ============================================================================
*/


using System;
using System.Windows.Forms;

using WizardWrx.DLLConfigurationManager;


namespace GUITestStand
{
    public partial class frmMain : Form
    {
        public frmMain ( )
        {
            InitializeComponent ( );
        }

        private void cmdGo_Click ( object sender , EventArgs e )
        {
            StateManager stateManager = StateManager.GetTheSingleInstance ( );

            stateManager.AppExceptionLogger.OptionFlags =   stateManager.AppExceptionLogger.OptionFlags
                                                          | ExceptionLogger.OutputOptions.NBSpaceForNewlines
                                                          | ExceptionLogger.OutputOptions.ActivePropsToEventLog;


            try
            {
                throw new Exception ( @"Testing: 1, 2, 3!" );
            }
            catch ( Exception exAllKinds )
            {
                this.txtMessages.Text = stateManager.AppExceptionLogger.ReportException ( exAllKinds );
            }
        }   // private void cmdGo_Click
    }   // public partial class frmMain
}   // partial namespace GUITestStand