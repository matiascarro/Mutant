using Microsoft.AspNetCore.Mvc;
using MutantCore.ServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantWebApp.Extensions
{
    public static class ResultApiExtensions
    {
        public static ActionResult CheckIfErrorApiResult<T, TE>(this Result<T> @this, Func<T, TE> mapResult)
        {
            if (@this.IsSuccessful())
                return new OkObjectResult(mapResult(@this.Value));
            //Otherwise an issue should be returned
            return new BadRequestObjectResult(new { @this.Error });
        }
    }
}
