using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Dtos
{
    public class ConfigOptionsDto
    {
        public int TotalAmount { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
    }
}
