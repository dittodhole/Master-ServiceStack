using System.Collections.Generic;
using ServiceStack;

namespace HelloWorld.Models
{
    public class Hellos : List<Hello>,
                          IReturn<HelloResponse[]>
    {
    }
}
