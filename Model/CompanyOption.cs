﻿namespace BlazorSolution.Model
{
    public class CompanyOption
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
