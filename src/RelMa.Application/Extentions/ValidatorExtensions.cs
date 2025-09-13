using FluentValidation;
using System.Text.RegularExpressions;

namespace RelMa.Application.Extentions;

public static class ValidatorExtensions
{
    private static readonly Regex ValidItemNameRegex = new Regex(@"^[^\\/:*?""<>|]+$", RegexOptions.Compiled);
    public static IRuleBuilderOptions<T, string> MustBeValidItemName<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.")
            .Must(name => ValidItemNameRegex.IsMatch(name))
            .WithMessage("Tên chứa ký tự không hợp lệ.");
    }

    public static IRuleBuilderOptions<T, DefaultIdType> MustBeValidUlid<T>(this IRuleBuilder<T, DefaultIdType> ruleBuilder)
    {
        return ruleBuilder
            .NotEqual(DefaultIdType.Empty).WithMessage("{PropertyName} không được để trống.")
            .WithMessage("{PropertyName} phải là một ULID hợp lệ.");
    }

    public static IRuleBuilderOptions<T, DefaultIdType?> MustBeValidUlidOrNull<T>(this IRuleBuilder<T, DefaultIdType?> ruleBuilder)
    {
        return ruleBuilder
            .Must(id => id == null || id.Value != DefaultIdType.Empty)
            .WithMessage("{PropertyName} phải là null hoặc một ULID hợp lệ.");
    }
}

