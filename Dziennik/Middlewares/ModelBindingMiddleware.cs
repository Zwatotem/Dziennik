using Dziennik.Data;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Dziennik.Middlewares;

public class ModelBindingMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = next;

	public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
	{
		if (context.Request.Method != HttpMethods.Post || !context.Request.HasFormContentType)
		{
			await _next(context);
			return;
		}

		var form = await context.Request.ReadFormAsync();
		var modelParameter = context
		                   ?.GetEndpoint()
		                   ?.Metadata
		                   ?.GetMetadata<ControllerActionDescriptor>()
		                   ?.Parameters
		                   ?.FirstOrDefault();
		var modelType = modelParameter?.ParameterType;

		if (modelType == null)
		{
			await _next(context);
			return;
		}

		var model = Activator.CreateInstance(modelType);

		var entityType  = dbContext.Model.FindEntityType(modelType);
		var foreignKeys = entityType.GetForeignKeys();

		foreach (var key in form.Keys)
		{
			var modelProperty = entityType
			                   .GetProperties()
			                   .FirstOrDefault(property => property.Name == key)
			                  ?.PropertyInfo;
			if (modelProperty is null) continue;
			var value = form[key][0];

			try
			{
				var propertyType                                           = modelProperty.PropertyType;
				if (propertyType.Name.Contains("Nullable`1")) propertyType = propertyType.GenericTypeArguments[0];

				var method = propertyType.GetMethod("Parse", [typeof(string)]);
				var parsedValue = method
				 .Invoke(null, [value]);

				modelProperty.SetValue(model, parsedValue);
			}
			catch (Exception)
			{
				// ignored
			}

			var foreignKey             = foreignKeys.FirstOrDefault(foreignKey => foreignKey.Properties.First().Name == key);
			var navigationPropertyName = foreignKey.DependentToPrincipal.Name;
			var navigationProperty     = modelType.GetProperty(navigationPropertyName);


			if (navigationProperty is not null)
			{
				var idPropertyName = foreignKey.Properties.First().Name;
				var id             = Guid.Parse(form[idPropertyName]);
				var entity         = await dbContext.FindAsync(navigationProperty.PropertyType, id);

				navigationProperty.SetValue(model, entity);
			}
		}

		context.Items["Actual" + modelParameter.Name] = model;
		await _next(context);
	}
}