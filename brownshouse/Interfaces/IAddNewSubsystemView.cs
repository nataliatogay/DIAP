﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Interfaces
{
    public interface IAddNewSubsystemView : IView
    {
        void SetViewModel(IAddNewSubsystemViewModel viewModel);
    }
}
