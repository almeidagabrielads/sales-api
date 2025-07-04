namespace Ambev.DeveloperEvaluation.IoC;

using Microsoft.AspNetCore.Builder;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
