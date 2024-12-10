using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sensor_app.Core;


public class Page<T>
{
    public T[] Items { get; set; }
    public int Index { get; set; }
    public int Size { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}

