using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.APIs.Errors;

namespace OrdersManagement.APIs.Controllers
{

	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]

	public class ErrorsController : ControllerBase
	{


		public ActionResult Error(int code) 
		{
			return NotFound(new ApiResponse( code , " End Point Is Not Found " ) );
		}


	}


}
