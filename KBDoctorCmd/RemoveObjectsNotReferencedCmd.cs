﻿using System;
using System.Diagnostics;
using System.Globalization;

using Artech.Architecture.Common.Services;
using Artech.MsBuild.Common;
using Concepto.Packages.KBDoctorCore.Sources;
using System.Collections.Generic;

namespace KBDoctorCmd
{
    public class RemoveObjectsNotReferencedCmd : ArtechTask
    {
        public override bool Execute()
        {
            bool isSuccess = true;
            Stopwatch watch = null;
            OutputSubscribe();
            IOutputService output = CommonServices.Output;
            output.StartSection("Remove attributes without table");
            try
            {
                watch = new Stopwatch();
                watch.Start();

                if (KB == null)
                {
                    output.AddErrorLine("No hay ninguna KB abierta en el contexto actual.");
                    isSuccess = false;
                }
                else
                {
                    output.AddLine("KBDoctor",string.Format(KB.Name, KB.Location));
                    List<string[]> list = new List<string[]>();
                    API.RemoveObjectsNotCalled(KB.DesignModel, output, out list);
                }

            }
            catch (Exception e)
            {
                output.AddErrorLine(e.Message);
                isSuccess = false;
            }
            finally
            {
                output.EndSection("Remove attributes without table", isSuccess);
                OutputUnsubscribe();
            }
            return isSuccess;
        }
    }
}
