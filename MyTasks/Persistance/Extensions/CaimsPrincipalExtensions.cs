using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace MyTasks.Persistance.Extensions
{
	public static class CaimsPrincipalExtensions
	{
		public static string GetUserId(this ClaimsPrincipal model)
		{
			return model.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	
	}
}
