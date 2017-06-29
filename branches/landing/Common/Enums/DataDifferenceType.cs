﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Enums
{
    public enum DataDifferenceType
    {
        RowCountDifferent,
        ColumnCountDifferent,
        ColumnMismatch,
        WrongOrder,
        NotFound,
        SimilarMatchFound
    }
}