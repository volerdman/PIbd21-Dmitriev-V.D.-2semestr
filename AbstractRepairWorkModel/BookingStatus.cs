﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkModel
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public enum BookingStatus
    {
        Принят = 0,

        Выполняется = 1,

        Готов = 2,

        Оплачен = 3
    }
}
