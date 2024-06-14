using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookify.web.Core.Consts
{
    public static class Errors
    {
        public const string MaxLength = "Length cannot be more than {1} characters";
        public const string Duplicated = "{0} with the same name is already exists!";
    }
}