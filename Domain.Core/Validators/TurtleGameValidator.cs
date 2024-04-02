using Domain.Entities.TurtleChallenge;
using FluentValidation;

namespace Domain.Core.Validators
{
    public class TurtleGameValidator : AbstractValidator<GameState>
    {
        public TurtleGameValidator()
        {
            RuleFor(x => x).NotNull()
                .Must(x => x.XMax > 0 && x.XMax <= x.SPEC_CONSTRAINT_MAX_SIZE)
                .WithMessage("Board X size with invalid definition");

            RuleFor(x => x).NotNull()
                .Must(x => x.YMax > 0 && x.YMax <= x.SPEC_CONSTRAINT_MAX_SIZE)
                .WithMessage("Board Y size with invalid definition");

            RuleFor(x => x.Turtle.Position).NotNull().WithMessage("Turtle Position with NULL.");
            RuleFor(x => x)
                .Must(x => x.Turtle.Position.ValidatePosition(x.XMax, x.YMax))
                .WithMessage("Turtle Position in invalid position.");

            RuleFor(x => x.Exit.Position).NotNull().WithMessage("Exit Position with NULL.");
            RuleFor(x => x)
                .Must(x => x.Exit.Position.ValidatePosition(x.XMax, x.YMax))
                .WithMessage("Exit Position in invalid position.");

            RuleFor(x => x)
                .Must(x => x.ListMine.Select(mine => mine.Position.ValidatePosition(x.XMax, x.YMax)).Count() == x.ListMine.Count())
                .WithMessage("Mine Position in invalid position.");
        }
    }
}
