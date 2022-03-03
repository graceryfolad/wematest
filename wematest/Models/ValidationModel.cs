using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wematest.Models
{
    public class ValidationModel
    {
        public static List<string> getErrors(ModelStateDictionary dictionary)
        {
            var query = from state in dictionary.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
    }

    
}
