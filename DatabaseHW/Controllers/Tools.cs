using DatabaseHW.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseHW.Controllers
{
    public static class Tools
    {
	    public static IActionResult Error(this Controller controller, Exception e)
	    {
		    string message = $"出错信息：{e.Message}\n出错资源：{e.Source}\n堆栈信息：{e.StackTrace}";
			return controller.View("Error", new ErrorViewModel { ErrorMessage = message });
		}
    }
}
