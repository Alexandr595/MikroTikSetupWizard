using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Application.Validation;
using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Services;

public sealed class BasicSetupWorkflow
{
    private readonly IConfigurationValidator<BasicSetupRequest> _validator;
    private readonly IConfigurationBuilder _builder;
    private readonly IConfigurationRenderer _renderer;

    public BasicSetupWorkflow(
        IConfigurationValidator<BasicSetupRequest> validator,
        IConfigurationBuilder builder,
        IConfigurationRenderer renderer)
    {
        _validator = validator;
        _builder = builder;
        _renderer = renderer;
    }

    public GeneratedConfiguration Generate(BasicSetupRequest request)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
        {
            return new GeneratedConfiguration(validation, null, string.Empty);
        }

        var plan = _builder.Build(request);
        var rscText = _renderer.Render(plan);

        return new GeneratedConfiguration(validation, plan, rscText);
    }
}
