using System;
using System.ComponentModel.DataAnnotations;

namespace Kogito.Blazor.Workflow.Process.Entities.Domain
{
    public class ExampleClass
    {
        [Required]
        public string StringExample { get; set; }

        public DateTime DateExample { get; set; }
        public int IntExample { get; set; }
        public bool BoolExample { get; set; }
        public char CharExample { get; set; }
        public decimal DecimalExample { get; set; }
        public double DoubleExample { get; set; }
        public float FloatExample { get; set; }
        public long LongExample { get; set; }
        public short ShortExample { get; set; }
    }
}