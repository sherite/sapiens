﻿namespace GenericDataLayer.Managers
{
    using System.Collections.Generic;

    public interface IDynamicMenuSideBarManager
    {
        IList<Entities.DTOs.DynamicMenuSideBar> Select();
    }
}
