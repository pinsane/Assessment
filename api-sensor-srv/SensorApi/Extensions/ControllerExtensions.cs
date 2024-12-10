using Microsoft.AspNetCore.Mvc;

namespace SensorApi.Extensions;

public static class ControllerExtensions
{
    public static OkObjectResult Result(this ControllerBase controller, object data, string message = null, int errorCode = 0, string exception = null)
    {
        var isSuccessful = data != null;

        var response = new
        {
            data,
            isSuccessful,
            message,
            errorCode,
            exception,
        };

        return controller.Ok(response);
    }
}
