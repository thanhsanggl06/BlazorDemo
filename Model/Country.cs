using System.Reflection;
using System.Text.Json;

namespace BlazorSolution.Model
{
    public class Country
    {
        public override string ToString()
        {
            return Name;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        
    }
}

