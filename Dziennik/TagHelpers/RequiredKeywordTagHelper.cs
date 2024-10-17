using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dziennik.TagHelpers;

[HtmlTargetElement("input", Attributes = ForAttributeName)]
public class RequiredKeywordTagHelper : TagHelper
{
	private const string ForAttributeName = "asp-for";

	[HtmlAttributeName(ForAttributeName)] public ModelExpression? For { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		if (For?.ModelExplorer?.Container?.ModelType != null)
		{
			var modelType = For.ModelExplorer.Container.ModelType;
			var property  = modelType.GetProperty(For.Name);

			var isRequired = property!.GetCustomAttributesData()
			                          .Any(ad => ad.AttributeType.Name == "IsExternalInitAttribute" &&
			                                     ad.AttributeType.Assembly.GetName().Name == "System.Runtime");

			if (isRequired) output.Attributes.Add("required", "required");
		}
	}
}